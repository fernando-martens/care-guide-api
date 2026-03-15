using CareGuide.Models.DTOs.Account;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.DTOs.PersonHealth;
using CareGuide.Models.DTOs.Phone;
using CareGuide.Models.Mappers.Person;
using CareGuide.Models.Mappers.PersonAnnotation;
using CareGuide.Models.Mappers.PersonHealth;
using CareGuide.Models.Mappers.PersonPhone;
using CareGuide.Models.Mappers.Phone;
using CareGuide.Models.Mappers.User;
using CareGuide.Models.Validators.Account;
using CareGuide.Models.Validators.PersonAnnotation;
using CareGuide.Models.Validators.PersonHealth;
using CareGuide.Models.Validators.Phone;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Models
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModelMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AccountToPersonProfileMapper>();
                cfg.AddProfile<AccountToUserProfileMapper>();
                cfg.AddProfile<PersonAnnotationProfileMapper>();
                cfg.AddProfile<PersonHealthProfileMapper>();
                cfg.AddProfile<PersonProfileMapper>();
                cfg.AddProfile<UserProfileMapper>();
                cfg.AddProfile<PhoneProfileMapper>();
                cfg.AddProfile<PersonPhoneProfileMapper>();
            });

            return services;
        }

        public static IServiceCollection AddModelValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateAccountDto>, CreateAccountDtoValidator>();
            services.AddTransient<IValidator<UpdatePasswordAccountDto>, UpdatePasswordAccountDtoValidator>();
            services.AddTransient<IValidator<CreatePersonAnnotationDto>, CreatePersonAnnotationDtoValidator>();
            services.AddTransient<IValidator<UpdatePersonAnnotationDto>, UpdatePersonAnnotationDtoValidator>();
            services.AddTransient<IValidator<CreatePersonHealthDto>, CreatePersonHealthDtoValidator>();
            services.AddTransient<IValidator<UpdatePersonHealthDto>, UpdatePersonHealthDtoValidator>();
            services.AddTransient<IValidator<CreatePhoneDto>, CreatePhoneDtoValidator>();
            services.AddTransient<IValidator<UpdatePhoneDto>, UpdatePhoneDtoValidator>();

            return services;
        }
    }
}
