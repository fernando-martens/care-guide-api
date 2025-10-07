using CareGuide.Core.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.PersonHealth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route(CareGuide.Models.Constants.ApiConstants.VersionPrefix + "/[controller]")]
    public class PersonHealthController
    {
        private readonly IPersonHealthService _personHealthService;

        public PersonHealthController(IPersonHealthService personHealthService)
        {
            _personHealthService = personHealthService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Person Health", Description = "Retrieves the health record associated with the logged-in person.")]
        public async Task<IResult> GetAll([FromQuery] int page = PaginationConstants.DefaultPage, [FromQuery] int pageSize = PaginationConstants.DefaultPageSize, CancellationToken cancellationToken = default)
        {
            var result = await _personHealthService.GetAllByPersonAsync(page, pageSize, cancellationToken);
            return Results.Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create Person Health", Description = "Creates a new person health for the logged-in person.")]
        public async Task<IResult> Create([FromBody] CreatePersonHealthDto createPersonHealth, CancellationToken cancellationToken)
        {
            var created = await _personHealthService.CreateAsync(createPersonHealth, cancellationToken);
            return Results.Created($"/PersonHealth/{created.Id}", created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Person Health", Description = "Updates an existing person health by its ID.")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdatePersonHealthDto updatePersonHealth, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personHealthService.UpdateAsync(id, updatePersonHealth, cancellationToken));
        }

        [HttpDelete("person")]
        [SwaggerOperation(Summary = "Delete Person Health", Description = "Deletes all person healths for the logged-in person.")]
        public async Task<IResult> DeleteAll(CancellationToken cancellationToken)
        {
            await _personHealthService.DeleteAllByPersonAsync(cancellationToken);
            return Results.NoContent();
        }
    }
}
