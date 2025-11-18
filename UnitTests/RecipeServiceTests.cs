using NUnit.Framework;
using Moq;
using Services;
using Services.Daos;
using Services.Dtos;
using Model;
using System.Collections.Generic;

namespace UnitTests
{
    public class RecipeServiceTests
    {
        private Mock<IRecipeDAO> _mockDAO;
        private RecipeService _service;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<IRecipeDAO>();
            _service = new RecipeService(_mockDAO.Object);
        }

        [Test]
        public void GetProductsWithRecipe_NormalFlow_ReturnsList()
        {
            var products = new List<ProductDTO> { new ProductDTO { ProductID = 1, Name = "Pizza" } };
            _mockDAO.Setup(d => d.GetProductsWithRecipe(true)).Returns(products);

            var result = _service.GetProductsWithRecipe(true);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetProductsWithRecipe_EmptyList_ReturnsEmpty()
        {
            _mockDAO.Setup(d => d.GetProductsWithRecipe(false)).Returns(new List<ProductDTO>());

            var result = _service.GetProductsWithRecipe(false);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void AddRecipe_WithStepsAndSupplies_ReturnsRecipeID()
        {
            var recipeDTO = new RecipeDTO
            {
                PreparationTime = 30,
                Steps = new List<RecipeStepDTO> { new RecipeStepDTO { StepNumber = 1, Instruction = "Step 1" } },
                Supplies = new List<RecipeSupplyDTO> { new RecipeSupplyDTO { SupplyID = 1, UseQuantity = 2 } }
            };

            var recipe = new Recipe { RecipeID = 5, PreparationTime = 30 };

            _mockDAO.Setup(d => d.AddRecipe(It.IsAny<Recipe>())).Returns(recipe);

            var result = _service.AddRecipe(recipeDTO);

            Assert.That(result, Is.EqualTo(5));
            _mockDAO.Verify(d => d.AddStep(It.IsAny<RecipeStepDTO>()), Times.Once);
            _mockDAO.Verify(d => d.AddSupply(It.IsAny<RecipeSupplyDTO>()), Times.Once);
        }

        [Test]
        public void AddRecipe_NullReturnFromDAO_ReturnsNegativeOne()
        {
            var recipeDTO = new RecipeDTO { PreparationTime = 20 };
            _mockDAO.Setup(d => d.AddRecipe(It.IsAny<Recipe>())).Returns((Recipe)null);

            var result = _service.AddRecipe(recipeDTO);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void UpdateRecipe_NormalFlow_ReturnsTrue()
        {
            var dto = new RecipeDTO { RecipeID = 1, PreparationTime = 15 };
            _mockDAO.Setup(d => d.UpdateRecipe(dto)).Returns(true);

            var result = _service.UpdateRecipe(dto);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateRecipe_Failure_ReturnsFalse()
        {
            var dto = new RecipeDTO { RecipeID = 2, PreparationTime = 20 };
            _mockDAO.Setup(d => d.UpdateRecipe(dto)).Returns(false);

            var result = _service.UpdateRecipe(dto);

            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteRecipe_NormalFlow_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.DeleteRecipe(1)).Returns(true);

            var result = _service.DeleteRecipe(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteRecipe_NotFound_ReturnsFalse()
        {
            _mockDAO.Setup(d => d.DeleteRecipe(999)).Returns(false);

            var result = _service.DeleteRecipe(999);

            Assert.IsFalse(result);
        }
    }
}
