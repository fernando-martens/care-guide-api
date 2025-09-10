using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.PersonAnnotation;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonAnnotationController : ControllerBase
    {
        private readonly IPersonAnnotationService _personAnnotationService;

        public PersonAnnotationController(IPersonAnnotationService personAnnotationService)
        {
            _personAnnotationService = personAnnotationService;
        }

        [HttpGet("SelectAllByPerson/{personId}")]
        public IResult SelectAllByPerson([FromRoute] Guid personId)
        {
            return Results.Ok(_personAnnotationService.SelectAllByPerson(personId));
        }

        [HttpGet("{id}")]
        public IResult SelectById([FromRoute] Guid id)
        {
            return Results.Ok(_personAnnotationService.SelectById(id));
        }

        [HttpPost]
        public IResult Create([FromBody] CreatePersonAnnotationDto createPersonAnnotation)
        {
            var created = _personAnnotationService.Create(createPersonAnnotation);
            return Results.Created($"/PersonAnnotations/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public IResult Update([FromRoute] Guid id, [FromBody] UpdatePersonAnnotationDto updatePersonAnnotation)
        {
            return Results.Ok(_personAnnotationService.Update(id, updatePersonAnnotation));
        }

        [HttpDelete("DeleteAllByPerson/{personId}")]
        public IResult DeleteAllByPerson([FromRoute] Guid personId)
        {
            _personAnnotationService.DeleteAllByPerson(personId);
            return Results.NoContent();
        }

        [HttpDelete("DeleteByIds")]
        public IResult DeleteByIds([FromBody] List<Guid> ids)
        {
            _personAnnotationService.DeleteByIds(ids);
            return Results.NoContent();
        }

    }
}
