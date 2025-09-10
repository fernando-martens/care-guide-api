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

        [HttpGet("person/{personId}")]
        public IResult GetAllByPerson([FromRoute] Guid personId)
        {
            return Results.Ok(_personAnnotationService.GetAllByPerson(personId));
        }

        [HttpGet("{id}")]
        public IResult GetById([FromRoute] Guid id)
        {
            return Results.Ok(_personAnnotationService.GetById(id));
        }

        [HttpPost]
        public IResult Create([FromBody] CreatePersonAnnotationDto createPersonAnnotation)
        {
            var created = _personAnnotationService.Create(createPersonAnnotation);
            return Results.Created($"/personAnnotation/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public IResult Update([FromRoute] Guid id, [FromBody] UpdatePersonAnnotationDto updatePersonAnnotation)
        {
            return Results.Ok(_personAnnotationService.Update(id, updatePersonAnnotation));
        }

        [HttpDelete("person/{personId}")]
        public IResult DeleteAllByPerson([FromRoute] Guid personId)
        {
            _personAnnotationService.DeleteAllByPerson(personId);
            return Results.NoContent();
        }

        [HttpDelete]
        public IResult DeleteByIds([FromBody] List<Guid> ids)
        {
            _personAnnotationService.DeleteByIds(ids);
            return Results.NoContent();
        }

    }
}
