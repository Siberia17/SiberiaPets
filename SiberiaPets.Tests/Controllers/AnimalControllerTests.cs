
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiberiaPets.Application.Controllers;
using SiberiaPets.Domain.Models;
using SiberiaPets.Services;
using System.Security.Cryptography.X509Certificates;

namespace SiberiaPets.Tests.Controllers
{
    [TestClass]
    public class AnimalControllerTests
    {
        private Mock<IAnimalService> _animalServiceMock;
        private SiberiaPets.Application.Controllers.AnimalController _controller;

        public AnimalControllerTests()
        {
            _animalServiceMock = new Mock<IAnimalService>();
            _controller = new AnimalController(_animalServiceMock.Object);
        }

        [TestMethod]
        public async Task GetAnimal_ReturnsAnimalAsync()
        {
            //configurar el servicio mock
            var animals = new List<Animal>
            {
                new Animal { IdAnimal = 1, Description = "Perro" },
                new Animal { IdAnimal = 2, Description = "Gato" }
            };
            _animalServiceMock.Setup(s => s.GetAnimalsAsync()).ReturnsAsync(animals);

            //llamar al metodo
            var result = await _controller.GetAnimals();

            //verificar el resultado

            var okResult = (OkObjectResult)result.Result;
            var animalList = (IEnumerable<Animal>)okResult.Value;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(animalList);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, animalList.Count());



        }

    }
}
