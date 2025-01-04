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

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Animal WHERE IdAnimal = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Animal
                            {
                                IdAnimal = reader.GetInt32(0),
                                Description = reader.GetString(1)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            using (var connection = new SqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Animal (Description) VALUES (@Description); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@Description", animal.Description);

                    var id = await command.ExecuteScalarAsync();

                    animal.IdAnimal = Convert.ToInt32(id);

                    return animal;
                }
            }
        }

        public async Task<Animal> UpdateAnimalAsync(Animal animal)
        {
            using (var connection = new SqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Animal SET Description = @Description WHERE IdAnimal = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Description", animal.Description);
                    command.Parameters.AddWithValue("@Id", animal.IdAnimal);

                    await command.ExecuteNonQueryAsync();

                    return animal;
                }
            }
        }

        public async Task DeleteAnimalAsync(int id)
        {
            using (var connection = new SqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
