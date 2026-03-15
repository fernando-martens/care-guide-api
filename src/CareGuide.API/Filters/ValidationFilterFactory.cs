using FluentValidation;
using FluentValidation.Results;
using System.Reflection;

namespace CareGuide.API.Filters;

public static class ValidationFilterFactory
{
    private static readonly MethodInfo ValidateMethod = typeof(ValidationFilterFactory).GetMethod(nameof(ValidateModelAsync), BindingFlags.NonPublic | BindingFlags.Static)!;

    public static EndpointFilterDelegate Create(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        var targets = context.MethodInfo
            .GetParameters()
            .Select((parameter, index) => new ValidationTarget(index, parameter.ParameterType))
            .Where(x => CanBeValidated(x.ModelType))
            .ToArray();

        if (targets.Length == 0)
        {
            return next;
        }

        return async invocationContext =>
        {
            Dictionary<string, string[]>? errors = null;

            foreach (var target in targets)
            {
                var argument = invocationContext.Arguments[target.ArgumentIndex];

                if (argument is null)
                {
                    continue;
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(target.ModelType);
                var validator = invocationContext.HttpContext.RequestServices.GetService(validatorType);

                if (validator is null)
                {
                    continue;
                }

                var validationResult = await InvokeValidationAsync(
                    target.ModelType,
                    validator,
                    argument,
                    invocationContext.HttpContext.RequestAborted);

                if (!validationResult.IsValid)
                {
                    errors ??= new Dictionary<string, string[]>(StringComparer.Ordinal);
                    MergeErrors(errors, validationResult);
                }
            }

            if (errors is not null)
            {
                return Results.ValidationProblem(errors);
            }

            return await next(invocationContext);
        };
    }

    private static bool CanBeValidated(Type type)
    {
        if (type == typeof(string) || type == typeof(HttpContext) || type == typeof(HttpRequest) || type == typeof(HttpResponse) || type == typeof(CancellationToken))
        {
            return false;
        }

        if (type.IsPrimitive || type.IsEnum || type.IsInterface || type.IsAbstract)
        {
            return false;
        }

        var underlyingNullable = Nullable.GetUnderlyingType(type);
        if (underlyingNullable is not null && (underlyingNullable.IsPrimitive || underlyingNullable.IsEnum))
        {
            return false;
        }

        return true;
    }

    private static async Task<ValidationResult> InvokeValidationAsync(Type modelType, object validator, object model, CancellationToken cancellationToken)
    {
        var genericMethod = ValidateMethod.MakeGenericMethod(modelType);
        var task = (Task<ValidationResult>)genericMethod.Invoke(null, new object[] { validator, model, cancellationToken })!;

        return await task;
    }

    private static Task<ValidationResult> ValidateModelAsync<T>(object validator, object model, CancellationToken cancellationToken)
    {
        return ((IValidator<T>)validator).ValidateAsync((T)model, cancellationToken);
    }

    private static void MergeErrors(IDictionary<string, string[]> errors, ValidationResult validationResult)
    {
        var groupedErrors = validationResult.Errors
            .Where(x => x is not null)
            .GroupBy(x => string.IsNullOrWhiteSpace(x.PropertyName) ? string.Empty : x.PropertyName, x => x.ErrorMessage);

        foreach (var group in groupedErrors)
        {
            if (errors.TryGetValue(group.Key, out var existing))
            {
                errors[group.Key] = existing.Concat(group).Distinct().ToArray();
            }
            else
            {
                errors[group.Key] = group.Distinct().ToArray();
            }
        }
    }

    private sealed record ValidationTarget(int ArgumentIndex, Type ModelType);
}