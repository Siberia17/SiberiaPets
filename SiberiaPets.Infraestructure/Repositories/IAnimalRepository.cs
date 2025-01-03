using SiberiaPets.Domain.Models;

namespace SiberiaPets.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync();

        Task<Animal> GetAnimalByIdAsync(int id);

        Task<Animal> CreateAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(int id);

    }
}
