using SiberiaPets.Domain.Models;
using SiberiaPets.Repositories;

namespace SiberiaPets.Services
{
    public class AnimalService : IAnimalService
    {

        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<IEnumerable<Animal>> GetAnimalsAsync()
        {
            return await _animalRepository.GetAnimalsAsync();
        }
    }
}
