
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
            try
            {
                List<Person> persons = _personService.ListAll();
                List<PersonResponseDto> personDtos = _mapper.Map<List<PersonResponseDto>>(persons);
                return Ok(personDtos);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<PersonResponseDto> ListById(Guid id)
        {
            try
            {
                Person person = _personService.ListById(id);
                PersonResponseDto personDto = _mapper.Map<PersonResponseDto>(person);
                return Ok(personDto);
            }
            catch (InvalidOperationException ex)
            {
                return HandleException(ex, ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpPost]
        public ActionResult<PersonResponseDto> Insert([FromBody] PersonRequestDto person)
        {
            try
            {
                Person personCreated = _personService.Insert(person);
                PersonResponseDto personDto = _mapper.Map<PersonResponseDto>(personCreated);
                return Ok(personDto);
            }
            catch (DbUpdateException ex)
            {
                return HandleException(ex, ex.InnerException?.Message ?? ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PersonResponseDto> Update(Guid id, [FromBody] PersonRequestDto person)
        {
            try
            {
                Person personCreated = _personService.Update(id, person);
                PersonResponseDto personDto = _mapper.Map<PersonResponseDto>(personCreated);
                return Ok(personDto);
            }
            catch (InvalidOperationException ex)
            {
                return HandleException(ex, ex.Message, 400);
            }
            catch (DbUpdateException ex)
            {
                return HandleException(ex, ex.InnerException?.Message ?? ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Remove(Guid id)
        {
            try
            {
                _personService.Remove(id);
                return Ok("Person successfully deleted.");
            }
            catch (InvalidOperationException ex)
            {
                return HandleException(ex, ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }
    }
}
