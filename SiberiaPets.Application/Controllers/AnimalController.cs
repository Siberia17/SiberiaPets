using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiberiaPets.Domain.Models;
using SiberiaPets.Services;

namespace SiberiaPets.Application.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            var animals = await _animalService.GetAnimalsAsync();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _animalService.GetAnimalByIdAsync(id);
            if(animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }


        [HttpPost]
        public async Task<ActionResult<Animal>> CreateAnimal(Animal animal)
        {
            var newanimal = await _animalService.CreateAnimalAsync(animal);
           
            return CreatedAtAction(nameof(GetAnimal), new { idAnimal = newanimal.IdAnimal }, newanimal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Animal>> UpdateAnimal(int id, Animal animal)
        {
            if (id != animal.IdAnimal)
            {
                return BadRequest();
            }
            await _animalService.UpdateAnimalAsync(animal);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await _animalService.GetAnimalByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            await _animalService.DeleteAnimalAsync(id);
            return NoContent();
        }

    }
}
