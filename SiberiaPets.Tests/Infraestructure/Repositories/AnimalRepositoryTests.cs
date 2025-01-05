using Moq;
using SiberiaPets.Domain.Models;
using SiberiaPets.Repositories;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SiberiaPets.Tests
{
    public class AnimalRepositoryTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private AnimalRepository _animalRepository;

        [TestInitialize]
        public void SetUp()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            var configuration = new ConfigurationBuilder().Build();
            _mockConfiguration.SetupGet(c => c[It.IsAny<string>()]).Returns(() => configuration[""]);
            _animalRepository = new AnimalRepository(configuration);
        }


        [TestMethod]
        public async Task GetAnimalByIdAsync_ReturnsAnimal_WhenAnimalExists()
        {
            // Arrange
            var animalId = 1;
            var animal = new Animal { IdAnimal = animalId, Description = "Perro" };

            // Act
            var result = await _animalRepository.GetAnimalByIdAsync(animalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(animalId, result.IdAnimal);
            Assert.AreEqual("Perro", result.Description);
        }

        [TestMethod]
        public async Task GetAnimalByIdAsync_ReturnsNull_WhenAnimalDoesNotExist()
        {
            // Arrange
            var animalId = 999;

            // Act
            var result = await _animalRepository.GetAnimalByIdAsync(animalId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteAnimalAsync_DeleteAnimal_WhenAnimalExists()
        {
            // Arrange
            var animalId = 1;

            // Act
            await _animalRepository.DeleteAnimalAsync(animalId);

            // Assert
            var deletedAnimal = await _animalRepository.GetAnimalByIdAsync(animalId);
            Assert.IsNull(deletedAnimal);
        }

        [TestMethod]
        public async Task DeleteAnimalAsync_DoNothing_WhenAnimalDoesNotExist()
        {
            // Arrange
            var animalId = 999;

            // Act
            await _animalRepository.DeleteAnimalAsync(animalId);

            // Assert
            var deletedAnimal = await _animalRepository.GetAnimalByIdAsync(animalId);
            Assert.IsNull(deletedAnimal);
        }

        [TestMethod]
        public async Task UpdateAnimalAsync_UpdateAnimal_WhenAnimalExists()
        {
            // Arrange
            var animalId = 1;
            var animal = new Animal { IdAnimal = animalId, Description = "Gato" };

            // Act
            await _animalRepository.UpdateAnimalAsync(animalId, animal);

            // Assert
            var updatedAnimal = await _animalRepository.GetAnimalByIdAsync(animalId);
            Assert.IsNotNull(updatedAnimal);
            Assert.AreEqual(animalId, updatedAnimal.IdAnimal);
            Assert.AreEqual("Gato", updatedAnimal.Description);
        }

        [TestMethod]
        public async Task UpdateAnimalAsync_DoNothing_WhenAnimalDoesNotExist()
        {
            // Arrange
            var animalId = 999;
            var animal = new Animal { IdAnimal = animalId, Description = "Gato" };

            // Act
            await _animalRepository.UpdateAnimalAsync(animalId, animal);

            // Assert
            var updatedAnimal = await _animalRepository.GetAnimalByIdAsync(animalId);
            Assert.IsNull(updatedAnimal);
        }
    }
}