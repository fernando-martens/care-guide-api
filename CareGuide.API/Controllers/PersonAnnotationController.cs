using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.PersonAnnotation;
using Microsoft.AspNetCore.Mvc;

namespace CareGuide.API.Controllers
{
    public class PersonAnnotationController : BaseApiController
    {

        private readonly IPersonAnnotationService _personAnnotationService;

        public PersonAnnotationController(ILogger<BaseApiController> logger, IPersonAnnotationService personAnnotationService) : base(logger)
        {
            _personAnnotationService = personAnnotationService;
        }

        [HttpGet("SelectAllByPerson/{personId}")]
        public ActionResult<List<PersonAnnotationDto>> SelectAllByPerson([FromRoute] Guid personId)
        {
            return Ok(_personAnnotationService.SelectAllByPerson(personId));
        }

        [HttpGet("{id}")]
        public ActionResult<List<PersonAnnotationDto>> SelectById([FromRoute] Guid id)
        {
            return Ok(_personAnnotationService.SelectById(id));
        }

        [HttpPost]
        public ActionResult<PersonAnnotationDto> Create([FromBody] CreatePersonAnnotationDto createPersonAnnotation)
        {
            return Ok(_personAnnotationService.Create(createPersonAnnotation));
        }

        [HttpPut("{id}")]
        public ActionResult<PersonAnnotationDto> Update([FromRoute] Guid id, [FromBody] UpdatePersonAnnotationDto updatePersonAnnotation)
        {
            return Ok(_personAnnotationService.Update(id, updatePersonAnnotation));
        }

        [HttpDelete("DeleteAllByPerson/{personId}")]
        public ActionResult<PersonAnnotationDto> DeleteAllByPerson([FromRoute] Guid personId)
        {
            _personAnnotationService.DeleteAllByPerson(personId);
            return Ok("Person Annotations successfully deleted.");
        }

        [HttpDelete("DeleteByIds")]
        public ActionResult<PersonAnnotationDto> DeleteByIds([FromBody] List<Guid> ids)
        {
            _personAnnotationService.DeleteByIds(ids);
            return Ok("Person Annotation successfully deleted.");
        }

    }
}
