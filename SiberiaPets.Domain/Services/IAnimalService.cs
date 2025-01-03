using SiberiaPets.Domain.Models;

namespace SiberiaPets.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync();
        Task<Animal> GetAnimalByIdAsync(int id);

        Task<Animal> CreateAnimalAsync(Animal animal);
        Task<Animal> UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(int id);
    }
}
