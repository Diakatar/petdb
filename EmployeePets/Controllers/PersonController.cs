using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeePets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace EmployeePets.Controllers
{
    [ApiController]
    [Route("api/v1/persons")]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ApplicationContext context, ILogger<PersonController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            _logger.LogDebug(
                $"{nameof(PersonController)}.{nameof(Get)} method called.");

            var result = await _db.Persons.ToListAsync().ConfigureAwait(false);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<Person> Get(long id)
        {
            _logger.LogDebug(
                $"{nameof(PersonController)}.{nameof(Get)} method called. Parameters: {nameof(id)} = {id}");
            return await _db.Persons.FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);
        }
        
        [HttpGet("{id}/pets")]
        public async Task<IEnumerable<Pet>> GetPets(long id)
        {
            _logger.LogDebug(
                $"{nameof(PersonController)}.{nameof(GetPets)} method called. Parameters: {nameof(id)} = {id}");
            var result = await _db.Persons.Where(i => i.Id == id)
                .Include(p => p.Pets).SelectMany(p => p.Pets)
                .Include(o => o.Owner)
                .Include(t => t.Type)
                .ToListAsync().ConfigureAwait(false);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            _logger.LogDebug(
                $"{nameof(PersonController)}.{nameof(Post)} method called. Parameters: {nameof(person)} = {person}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _db.Persons.AddAsync(person)
                .ConfigureAwait(false);
            var affected = await _db.SaveChangesAsync().ConfigureAwait(false);
            if (affected == 0) NoContent();
            return Ok(result.Entity);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(Person person)
        {
            _logger.LogDebug(
                $"{nameof(PersonController)}.{nameof(Put)} method called. Parameters: {nameof(person)} = {person}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _db.Persons.Update(person);
            var affected = await _db.SaveChangesAsync().ConfigureAwait(false);
            if (affected == 0) return NoContent();
            return Ok(result.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogDebug(
                $"{nameof(PersonController)}.{nameof(Delete)} method called. Parameters: {nameof(id)} = {id}");
            var person = await _db.Persons.FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (person == null) return NotFound(id);
            _db.Persons.Remove(person);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(person);
        }
    }
}