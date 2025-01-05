using Microsoft.Extensions.Configuration;
using SiberiaPets.Domain.Models;
using SiberiaPets.Infraestructure.Settings;
using System.Data.SqlClient;
using Dapper;

namespace SiberiaPets.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _connectionString;

        public AnimalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Animal>> GetAnimalsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var animales = await connection.QueryAsync<Animal>("sp_GetAnimals");
                return animales;
            }
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var animal = await connection.QueryFirstOrDefaultAsync<Animal>("sp_GetAnimalById", new { idAnimal = id });
                    return animal;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al ejecutar el stored procedure: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var id = await connection.ExecuteScalarAsync<int>("sp_CreateAnimal", new { Description = animal.Description });
                animal.IdAnimal = id;
                return animal;
            }
        }

        public async Task<Animal> UpdateAnimalAsync(int id, Animal animal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("sp_UpdateAnimal", new { IdAnimal = id, Description = animal.Description });
                return animal;
            }
        }

        public async Task DeleteAnimalAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("sp_DeleteAnimal", new { IdAnimal = id });
            }
        }
    }
}