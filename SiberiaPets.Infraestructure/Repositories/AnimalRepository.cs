using Microsoft.Extensions.Configuration;
using SiberiaPets.Domain.Models;
using SiberiaPets.Infraestructure.Settings;
using System.Data.SqlClient;

namespace SiberiaPets.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public AnimalRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<IEnumerable<Animal>> GetAnimalsAsync()
        {
            using (SqlConnection connection = new SqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM Animal", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<Animal> animals = new List<Animal>();

                while (await reader.ReadAsync())
                {
                    var animal = new Animal
                    {
                        IdAnimal = reader.GetInt32(0),
                        Description = reader.GetString(1)
                    };

                    animals.Add(animal);
                }
                return animals;
            }
        }
    }
}
