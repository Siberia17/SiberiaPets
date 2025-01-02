using SiberiaPets.Domain.Models;

namespace SiberiaPets.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync();
    }
}
