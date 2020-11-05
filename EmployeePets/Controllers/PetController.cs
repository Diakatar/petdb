using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeePets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeePets.Controllers
{
    [ApiController]
    [Route("api/v1/pets")]
    public class PetController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<PetController> _logger;

        public PetController(ApplicationContext context, ILogger<PetController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Pet>> Get()
        {
            _logger.LogDebug(
                $"{nameof(PetController)}.{nameof(Get)} method called.");
            return await _db.Pets.ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<Pet> Get(long id)
        {
            _logger.LogDebug(
                $"{nameof(PetController)}.{nameof(Get)} method called. Parameters: {nameof(id)} = {id}");
            return await _db.Pets.FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pet pet)
        {
            _logger.LogDebug(
                $"{nameof(PetController)}.{nameof(Post)} method called. Parameters: {nameof(pet)} = {pet}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.AttachRange(pet.Type, pet.Owner);
            //_db.Entry(pet.Type).State = EntityState.Unchanged;
            //_db.Entry(pet.Owner).State = EntityState.Unchanged;
            var result = await _db.Pets.AddAsync(pet)
                .ConfigureAwait(false);
            var affected = await _db.SaveChangesAsync(true).ConfigureAwait(false);
            if (affected == 0) NoContent();
            return Ok(result.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Pet pet)
        {
            _logger.LogDebug(
                $"{nameof(PetController)}.{nameof(Put)} method called. Parameters: {nameof(pet)} = {pet}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _db.Pets.Update(pet);
            var affected = await _db.SaveChangesAsync().ConfigureAwait(false);
            if (affected == 0) return NoContent();
            return Ok(result.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogDebug(
                $"{nameof(PetController)}.{nameof(Delete)} method called. Parameters: {nameof(id)} = {id}");
            var pet = await _db.Pets.FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (pet == null) return NotFound(id);
            _db.Pets.Remove(pet);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(pet);
        }
    }
}