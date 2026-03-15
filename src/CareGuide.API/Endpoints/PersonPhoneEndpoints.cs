using CareGuide.API.Endpoints.Shared;
using CareGuide.API.Extensions;
using CareGuide.Core.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.PersonPhone;
using CareGuide.Models.DTOs.Phone;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Endpoints;

public class PersonPhoneEndpoints() : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/person-phones")
            .WithTags("Person Phones")
            .WithDefaultProblemResponses();

        group.MapGet("/", GetAll)
             .WithName("GetAllPersonPhones")
             .WithSummary("Get all person phones")
             .WithDescription("Retrieves all phone records associated with the authenticated person using pagination parameters.")
             .Produces<PersonPhoneDto>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapGet("/{id:guid}", GetById)
             .WithName("GetPersonPhoneById")
             .WithSummary("Get person phone by id")
             .WithDescription("Retrieves a specific phone record for the authenticated person by its identifier.")
             .Produces<PersonPhoneDto>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", Create)
             .WithName("CreatePersonPhone")
             .WithSummary("Create person phone")
             .WithDescription("Creates a new phone record for the authenticated person.")
             .Accepts<CreatePhoneDto>("application/json")
             .Produces<PersonPhoneDto>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:guid}", Update)
             .WithName("UpdatePersonPhone")
             .WithSummary("Update person phone")
             .WithDescription("Updates an existing phone record for the authenticated person by its identifier.")
             .Accepts<UpdatePhoneDto>("application/json")
             .Produces<PersonPhoneDto>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/person", DeleteAll)
             .WithName("DeleteAllPersonPhones")
             .WithSummary("Delete all person phones")
             .WithDescription("Deletes all phone records associated with the authenticated person.")
             .Produces(StatusCodes.Status204NoContent);

        group.MapDelete("/", DeleteByIds)
             .WithName("DeletePersonPhonesByIds")
             .WithSummary("Delete multiple person phones")
             .WithDescription("Deletes multiple phone records for the authenticated person using a list of phone identifiers provided in the request body.")
             .Accepts<List<Guid>>("application/json")
             .Produces(StatusCodes.Status204NoContent)
             .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> GetAll(int page, int pageSize, IPersonPhoneService personPhoneService, CancellationToken cancellationToken)
    {
        page = page == 0 ? PaginationConstants.DefaultPage : page;
        pageSize = pageSize == 0 ? PaginationConstants.DefaultPageSize : pageSize;

        var result = await personPhoneService.GetAllByPersonAsync(page, pageSize, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetById(Guid id, IPersonPhoneService personPhoneService, CancellationToken cancellationToken)
    {
        var result = await personPhoneService.GetAsync(id, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Create(CreatePhoneDto createPhoneDto, IPersonPhoneService personPhoneService, CancellationToken cancellationToken)
    {
        var created = await personPhoneService.CreateAsync(createPhoneDto, cancellationToken);
        return Results.Created($"/person-phones/{created.PersonId}", created);
    }

    private static async Task<IResult> Update(Guid id, UpdatePhoneDto updatePhoneDto, IPersonPhoneService personPhoneService, CancellationToken cancellationToken)
    {
        var result = await personPhoneService.UpdateAsync(id, updatePhoneDto, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteAll(IPersonPhoneService personPhoneService, CancellationToken cancellationToken)
    {
        await personPhoneService.DeleteAllByPersonAsync(cancellationToken);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteByIds([FromBody] List<Guid> ids, IPersonPhoneService personPhoneService, CancellationToken cancellationToken)
    {
        await personPhoneService.DeleteByIdsAsync(ids, cancellationToken);
        return Results.NoContent();
    }
}