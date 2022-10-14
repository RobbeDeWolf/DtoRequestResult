using Microsoft.AspNetCore.Mvc;
using PeopleManager.Services.Abstractions;
using PeopleManager.Services.Model.Requests;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var people = await _personService.FindAsync();
            return Ok(people);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var person = await _personService.GetAsync(id);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PersonRequest person)
        {
            var createdPerson = await _personService.CreateAsync(person);
            return Ok(createdPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]PersonRequest person)
        {
            var updatedPerson = await _personService.UpdateAsync(id, person);
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var isDeleted = await _personService.DeleteAsync(id);
            return Ok(isDeleted);
        }
    }
}
