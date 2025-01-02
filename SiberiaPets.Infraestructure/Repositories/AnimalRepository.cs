using Microsoft.Extensions.Configuration;
using SiberiaPets.Domain.Models;
using System.Data.SqlClient;

namespace SiberiaPets.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _connectionString;

        public AnimalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("StringSql");
        }

        public async Task<IEnumerable<Animal>> GetAnimalsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Animal", connection))
                {
                    using(var reader = await command.ExecuteReaderAsync())
                    {
                        var animals = new List<Animal>();

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
    }
}
