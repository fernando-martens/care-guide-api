
using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.API.Controllers
{

    public class PersonController : BaseApiController
    {

        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(ILogger<BaseApiController> logger, IPersonService personService, IMapper mapper) : base(logger)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<PersonResponseDto[]> List()
        {
            List<Person> persons = _personService.ListAll();
            List<PersonResponseDto> personDtos = _mapper.Map<List<PersonResponseDto>>(persons);
            return Ok(personDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<PersonResponseDto> ListById(Guid id)
        {
            Person person = _personService.ListById(id);
            PersonResponseDto personDto = _mapper.Map<PersonResponseDto>(person);
            return Ok(personDto);
        }

        [HttpPost]
        public ActionResult<PersonResponseDto> Insert([FromBody] PersonRequestDto person)
        {
            Person personCreated = _personService.Insert(person);
            PersonResponseDto personDto = _mapper.Map<PersonResponseDto>(personCreated);
            return Ok(personDto);
        }

        [HttpPut("{id}")]
        public ActionResult<PersonResponseDto> Update(Guid id, [FromBody] PersonRequestDto person)
        {
            Person personCreated = _personService.Update(id, person);
            PersonResponseDto personDto = _mapper.Map<PersonResponseDto>(personCreated);
            return Ok(personDto);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Remove(Guid id)
        {
            _personService.Remove(id);
            return Ok("Person successfully deleted.");
        }
    }
}
