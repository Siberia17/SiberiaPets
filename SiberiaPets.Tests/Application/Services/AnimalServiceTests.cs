using Moq;
using SiberiaPets.Domain.Models;
using SiberiaPets.Repositories;
using SiberiaPets.Services;

namespace SiberiaPets.Tests
{
    public class AnimalServiceTests
    {
        private readonly Mock<IAnimalRepository> _mockAnimalRepository;
        private readonly AnimalService _animalService;

        public AnimalServiceTests()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _animalService = new AnimalService(_mockAnimalRepository.Object);
        }

        [Fact]
        public async Task GetAnimalById_ReturnsAnimal_WithAValidId()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(r => r.GetAnimalByIdAsync(It.IsAny<int>())).ReturnsAsync(animal);

            // Act
            var result = await _animalService.GetAnimalByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdAnimal);
            Assert.Equal("Perro", result.Description);
        }

        [Fact]
        public async Task GetAnimalById_ReturnsNull_WithAnInvalidId()
        {
            // Arrange
            Animal animal = null;
            _mockAnimalRepository.Setup(r => r.GetAnimalByIdAsync(It.IsAny<int>())).ReturnsAsync(animal);

            // Act
            var result = await _animalService.GetAnimalByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAnimal_ValidData_AnimalCreated()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(r => r.CreateAnimalAsync(It.IsAny<Animal>())).ReturnsAsync(animal);

            // Act
            var result = await _animalService.CreateAnimalAsync(animal);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdAnimal);
            Assert.Equal("Perro", result.Description);
        }

        [Fact]
        public async Task UpdateAnimal_ValidData_AnimalUpdated()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(r => r.UpdateAnimalAsync(animal.IdAnimal, It.IsAny<Animal>())).ReturnsAsync(animal);

            // Act
            var result = await _animalService.UpdateAnimalAsync(animal.IdAnimal, animal);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdAnimal);
            Assert.Equal("Perro", result.Description);
        }

        [Fact]
        public async Task DeleteAnimal_ValidData_AnimalDeleted()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(r => r.DeleteAnimalAsync(It.IsAny<int>())).Returns(() => Task.FromResult(animal));

            // Act
            await _animalService.DeleteAnimalAsync(1);

            // Assert
            var deletedAnimal = await _animalService.GetAnimalByIdAsync(1);
            Assert.Equal(1, deletedAnimal.IdAnimal);
            Assert.Equal("Perro", deletedAnimal.Description);
        }

    }
}