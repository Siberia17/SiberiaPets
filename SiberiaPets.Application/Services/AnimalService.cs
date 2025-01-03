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

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await _animalRepository.GetAnimalByIdAsync(id);
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            return await _animalRepository.CreateAnimalAsync(animal);
        }


        public async Task DeleteAnimalAsync(int id)
        {
            await _animalRepository.DeleteAnimalAsync(id);
        }

        public Task<Animal> UpdateAnimalAsync(Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}
