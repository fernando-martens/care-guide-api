using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.PersonAnnotation;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("person/{personId}")]
        public async Task<IResult> GetAllByPerson([FromRoute] Guid personId, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personAnnotationService.GetAllByPersonAsync(personId, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personAnnotationService.GetAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreatePersonAnnotationDto createPersonAnnotation, CancellationToken cancellationToken)
        {
            var created = await _personAnnotationService.CreateAsync(createPersonAnnotation, cancellationToken);
            return Results.Created($"/personAnnotation/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdatePersonAnnotationDto updatePersonAnnotation, CancellationToken cancellationToken)
        {
            return Results.Ok(await _personAnnotationService.UpdateAsync(id, updatePersonAnnotation, cancellationToken));
        }

        [HttpDelete("person/{personId}")]
        public async Task<IResult> DeleteAllByPerson([FromRoute] Guid personId, CancellationToken cancellationToken)
        {
            await _personAnnotationService.DeleteAllByPersonAsync(personId, cancellationToken);
            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> DeleteByIds([FromBody] List<Guid> ids, CancellationToken cancellationToken)
        {
            await _personAnnotationService.DeleteByIdsAsync(ids, cancellationToken);
            return Results.NoContent();
        }

    }
}
