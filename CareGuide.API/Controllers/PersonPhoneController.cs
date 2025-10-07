using CareGuide.Core.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Phone;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route(CareGuide.Models.Constants.ApiConstants.VersionPrefix + "/[controller]")]
    public class PersonPhoneController
    {
        private readonly IPersonPhoneService _personPhoneService;

        public PersonPhoneController(IPersonPhoneService personPhoneService)
        {
            _personPhoneService = personPhoneService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Person Phones", Description = "Retrieves all phones for the logged-in person, with pagination.")]
        public async Task<IResult> GetAll([FromQuery] int page = PaginationConstants.DefaultPage, [FromQuery] int pageSize = PaginationConstants.DefaultPageSize, CancellationToken cancellationToken = default)
        {
            var result = await _personPhoneService.GetAllByPersonAsync(page, pageSize, cancellationToken);
            return Results.Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Person Phone By Id", Description = "Retrieves a specific phone by its person phone ID.")]
        public async Task<IResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personPhoneService.GetAsync(id, cancellationToken));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create Person Phone", Description = "Creates a new phone for the logged-in person.")]
        public async Task<IResult> Create([FromBody] CreatePhoneDto createPhoneDto, CancellationToken cancellationToken)
        {
            var created = await _personPhoneService.CreateAsync(createPhoneDto, cancellationToken);
            return Results.Created($"/PersonPhone/", created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Person Phone", Description = "Updates an existing person phone by its ID.")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdatePhoneDto updatePhoneDto, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personPhoneService.UpdateAsync(id, updatePhoneDto, cancellationToken));
        }

        [HttpDelete("person")]
        [SwaggerOperation(Summary = "Delete All Person Phones", Description = "Deletes all phones for the logged-in person.")]
        public async Task<IResult> DeleteAll(CancellationToken cancellationToken)
        {
            await _personPhoneService.DeleteAllByPersonAsync(cancellationToken);
            return Results.NoContent();
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete Multiple Person Phones", Description = "Deletes multiple phones by PersonPhones IDs.")]
        public async Task<IResult> DeleteByIds([FromBody] List<Guid> ids, CancellationToken cancellationToken)
        {
            await _personPhoneService.DeleteByIdsAsync(ids, cancellationToken);
            return Results.NoContent();
        }
    }
}
