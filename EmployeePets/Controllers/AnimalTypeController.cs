using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeePets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeePets.Controllers
{
    [ApiController]
    [Route("api/v1/animaltypes")]
    public class AnimalTypeController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<AnimalTypeController> _logger;

        public AnimalTypeController(ApplicationContext context, ILogger<AnimalTypeController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<AnimalType>> Get()
        {
            _logger.LogDebug(
                $"{nameof(AnimalTypeController)}.{nameof(Get)} method called.");
            
            return await _db.AnimalTypes.ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<AnimalType> Get(long id)
        {
            _logger.LogDebug(
                $"{nameof(AnimalTypeController)}.{nameof(Get)} method called. Parameters: {nameof(id)} = {id}");
            return await _db.AnimalTypes.FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AnimalType animalType)
        {
            _logger.LogDebug(
                $"{nameof(AnimalTypeController)}.{nameof(Post)} method called. Parameters: {nameof(animalType)} = {animalType}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _db.AnimalTypes.AddAsync(animalType)
                .ConfigureAwait(false);
            var affected = await _db.SaveChangesAsync().ConfigureAwait(false);
            if (affected == 0) NoContent();
            return Ok(result.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put(AnimalType animalType)
        {
            _logger.LogDebug(
                $"{nameof(AnimalTypeController)}.{nameof(Put)} method called. Parameters: {nameof(animalType)} = {animalType}");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _db.AnimalTypes.Update(animalType);
            var affected = await _db.SaveChangesAsync().ConfigureAwait(false);
            if (affected == 0) return NoContent();
            return Ok(result.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogDebug(
                $"{nameof(AnimalTypeController)}.{nameof(Delete)} method called. Parameters: {nameof(id)} = {id}");
            var animalType = await _db.AnimalTypes.FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (animalType == null) return NotFound(id);
            _db.AnimalTypes.Remove(animalType);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(animalType);
        }
    }
}