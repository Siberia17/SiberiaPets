using Microsoft.AspNetCore.Mvc;
using SiberiaPets.Domain.Models;
using SiberiaPets.Services;

namespace SiberiaPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
