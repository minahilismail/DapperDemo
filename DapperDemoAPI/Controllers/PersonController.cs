using DapperDemoData.Models;
using DapperDemoData.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/<PersonController>
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository person)
        {
            _personRepository = person;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _personRepository.GetPeople();
            return Ok(people);
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (person == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _personRepository.AddPerson(person);
            if (!result)
            {
                return BadRequest("Cannot Save data");
            }
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            if (person == null || person.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPerson = await _personRepository.GetPersonById(id);
            if (existingPerson == null)
            {
                return NotFound();
            }

            var result = await _personRepository.UpdatePerson(person);
            if (!result)
            {
                return BadRequest("Cannot update person");
            }
            return NoContent();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personRepository.DeletePerson(id);
            return NoContent();
        }
    }
}
