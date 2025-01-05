using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SiberiaPets.Services;
using SiberiaPets.Application.Controllers;
using SiberiaPets.Repositories;
using SiberiaPets.Domain.Models;

namespace SiberiaPets.Tests
{
    public class AnimalControllerTests
    {
        private readonly Mock<IAnimalRepository> _mockAnimalRepository;
        private readonly AnimalController _animalController;

        public AnimalControllerTests()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _animalController = new AnimalController(_mockAnimalRepository.Object);
        }

        [Fact]
        public async Task GetAnimalById_ReturnsOkResult_WithAValidId()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(s => s.GetAnimalByIdAsync(It.IsAny<int>())).ReturnsAsync(animal);

            // Act
            var result = await _mockAnimalRepository.Object.GetAnimalByIdAsync(1);

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdAnimal);
            Assert.Equal("Perro", result.Description);
        }

        [Fact]
        public async Task GetAnimalById_ReturnsNotFoundResult_WithAnInvalidId()
        {
            // Arrange
            Animal animal = null;
            _mockAnimalRepository.Setup(s => s.GetAnimalByIdAsync(It.IsAny<int>())).ReturnsAsync(animal);

            // Act
            var result = await _mockAnimalRepository.Object.GetAnimalByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAnimal_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(s => s.CreateAnimalAsync(It.IsAny<Animal>())).ReturnsAsync(animal);

            // Act
            var result = await _mockAnimalRepository.Object.CreateAnimalAsync(animal);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAnimal_ValidData_ReturnsOkResult()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };
            _mockAnimalRepository.Setup(s => s.UpdateAnimalAsync(animal.IdAnimal, It.IsAny<Animal>())).ReturnsAsync(animal);

            // Act
            var result = await _mockAnimalRepository.Object.UpdateAnimalAsync(animal.IdAnimal, animal);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAnimal_ValidData_ReturnsOkResult()
        {
            // Arrange
            var animal = new Animal { IdAnimal = 1, Description = "Perro" };


            _mockAnimalRepository.Setup(r => r.DeleteAnimalAsync(It.IsAny<int>())).Returns(() => Task.FromResult(animal));

            // Act
            await _mockAnimalRepository.Object.DeleteAnimalAsync(1);

            // Assert
            var deletedAnimal = await _mockAnimalRepository.Object.GetAnimalByIdAsync(1);

            Assert.Equal(1, deletedAnimal.IdAnimal);
            Assert.Equal("Perro", deletedAnimal.Description);
        }

    }
}