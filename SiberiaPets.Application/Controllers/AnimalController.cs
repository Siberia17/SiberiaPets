using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiberiaPets.Domain.Models;
using SiberiaPets.Repositories;

namespace SiberiaPets.Application.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;
        
        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            var animals = await _animalRepository.GetAnimalsAsync();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimalById(int id)
        {
            var animal = await _animalRepository.GetAnimalByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }


        [HttpPost]
        public async Task<ActionResult<Animal>> CreateAnimal(Animal animal)
        {
            var createdAnimal = await _animalRepository.CreateAnimalAsync(animal);

            return CreatedAtAction(nameof(GetAnimalById), new { id = createdAnimal.IdAnimal }, createdAnimal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Animal>> UpdateAnimal(int id, Animal animal)
        {
            var updatedAnimal = await _animalRepository.UpdateAnimalAsync(id, animal);
            return Ok(updatedAnimal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await _animalRepository.GetAnimalByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            await _animalRepository.DeleteAnimalAsync(id);
            return NoContent();
        }
    }
}
