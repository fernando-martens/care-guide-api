using CareGuide.Core.Interfaces;
using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.PersonAnnotation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route(CareGuide.Models.Constants.ApiConstants.VersionPrefix + "/[controller]")]
    public class PersonAnnotationController : ControllerBase
    {
        private readonly IPersonAnnotationService _personAnnotationService;

        public PersonAnnotationController(IPersonAnnotationService personAnnotationService)
        {
            _personAnnotationService = personAnnotationService;
        }


        [SwaggerOperation(Summary = "Get All Annotations", Description = "Retrieves all annotations for the logged-in person, with pagination.")]
        [HttpGet]
        public async Task<IResult> GetAllByPerson([FromQuery] int page = PaginationConstants.DefaultPage, [FromQuery] int pageSize = PaginationConstants.DefaultPageSize, CancellationToken cancellationToken = default)
        {
            var result = await _personAnnotationService.GetAllByPersonAsync(page, pageSize, cancellationToken);
            return Results.Ok(result);
        }


        [SwaggerOperation(Summary = "Get Annotation By Id", Description = "Retrieves a specific annotation by its ID.")]
        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personAnnotationService.GetAsync(id, cancellationToken));
        }


        [SwaggerOperation(Summary = "Create Annotation", Description = "Creates a new annotation for the logged-in person.")]
        [HttpPost]
        public async Task<IResult> Create([FromBody] CreatePersonAnnotationDto createPersonAnnotation, CancellationToken cancellationToken)
        {
            var created = await _personAnnotationService.CreateAsync(createPersonAnnotation, cancellationToken);
            return Results.Created($"/personAnnotation/{created.Id}", created);
        }


        [SwaggerOperation(Summary = "Update Annotation", Description = "Updates an existing annotation by its ID.")]
        [HttpPut("{id}")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdatePersonAnnotationDto updatePersonAnnotation, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personAnnotationService.UpdateAsync(id, updatePersonAnnotation, cancellationToken));
        }


        [SwaggerOperation(Summary = "Delete All Annotations", Description = "Deletes all annotations for the logged-in person.")]
        [HttpDelete("person")]
        public async Task<IResult> DeleteAllByPerson(CancellationToken cancellationToken)
        {
            await _personAnnotationService.DeleteAllByPersonAsync(cancellationToken);
            return Results.NoContent();
        }


        [SwaggerOperation(Summary = "Delete Multiple Annotations", Description = "Deletes multiple annotations by their IDs.")]
        [HttpDelete]
        public async Task<IResult> DeleteByIds([FromBody] List<Guid> ids, CancellationToken cancellationToken)
        {
            await _personAnnotationService.DeleteByIdsAsync(ids, cancellationToken);
            return Results.NoContent();
        }

    }
}
