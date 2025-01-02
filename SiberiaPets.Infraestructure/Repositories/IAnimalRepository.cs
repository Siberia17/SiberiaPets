using SiberiaPets.Domain.Models;

namespace SiberiaPets.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync();

    }
}
