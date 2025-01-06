using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiberiaPets.Domain.Models;
using SiberiaPets.Repositories;
using SiberiaPets.Domain.Constants;
using Microsoft.AspNetCore.Http;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return StatusCode(401, ErrorMessages.ErrorAuthentication);
            }
            try
            {
                var animals = await _animalRepository.GetAnimalsAsync();
                return Ok(animals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorMessages.ErrorAuthentication);
            }
           
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimalById(int id)
        {
            try
            {
                var animal = await _animalRepository.GetAnimalByIdAsync(id);
                if (animal == null)
                {
                    return StatusCode(404, ErrorMessages.ErrorRegistryNotFound);  
                }
                return animal;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorMessages.ErrorAuthentication);
            }
           
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Animal>> CreateAnimal(Animal animal)
        {
            if (await _animalRepository.ExistAnimal(animal.Description))
            {
                return Conflict(new { error = ErrorMessages.ErrorRegistryAlreadyExist });
            }

            try
            {
                var createdAnimal = await _animalRepository.CreateAnimalAsync(animal);

                return CreatedAtAction(nameof(GetAnimalById), new { id = createdAnimal.IdAnimal }, createdAnimal);
            }
            catch (Exception ex)
            {
               
                    return StatusCode(500, ErrorMessages.ErrorAuthentication);
              
            }
            
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Animal>> UpdateAnimal(int id, Animal animal)
        {
            
            try
            {
                var updatedAnimal = await _animalRepository.UpdateAnimalAsync(id, animal);
                return Ok(updatedAnimal);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorMessages.ErrorDatabaseConnection);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
           
            try
            {
                var animal = await _animalRepository.GetAnimalByIdAsync(id);
                if (animal == null)
                {
                    return NotFound();
                }
                await _animalRepository.DeleteAnimalAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ErrorMessages.ErrorDatabaseConnection);
            }
        }
    }
}
