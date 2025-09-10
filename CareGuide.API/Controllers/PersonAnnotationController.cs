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
        public async Task<IResult> GetAllByPerson([FromRoute] Guid personId)
        {
            return Results.Ok(await _personAnnotationService.GetAllByPersonAsync(personId));
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] Guid id)
        {
            return Results.Ok(await _personAnnotationService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreatePersonAnnotationDto createPersonAnnotation)
        {
            var created = await _personAnnotationService.CreateAsync(createPersonAnnotation);
            return Results.Created($"/personAnnotation/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdatePersonAnnotationDto updatePersonAnnotation)
        {
            return Results.Ok(await _personAnnotationService.UpdateAsync(id, updatePersonAnnotation));
        }

        [HttpDelete("person/{personId}")]
        public async Task<IResult> DeleteAllByPerson([FromRoute] Guid personId)
        {
            await _personAnnotationService.DeleteAllByPersonAsync(personId);
            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> DeleteByIds([FromBody] List<Guid> ids)
        {
            await _personAnnotationService.DeleteByIdsAsync(ids);
            return Results.NoContent();
        }

    }
}
