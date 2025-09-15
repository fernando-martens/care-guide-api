using CareGuide.Core.Interfaces;
using CareGuide.Models.Constants;
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

        [HttpGet]
        public async Task<IResult> GetAllByPerson([FromQuery] int page = PaginationConstants.DefaultPage, [FromQuery] int pageSize = PaginationConstants.DefaultPageSize, CancellationToken cancellationToken = default)
        {
            var result = await _personAnnotationService.GetAllByPersonAsync(page, pageSize, cancellationToken);
            return Results.Ok(result);
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

        [HttpDelete("person")]
        public async Task<IResult> DeleteAllByPerson(CancellationToken cancellationToken)
        {
            await _personAnnotationService.DeleteAllByPersonAsync(cancellationToken);
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
