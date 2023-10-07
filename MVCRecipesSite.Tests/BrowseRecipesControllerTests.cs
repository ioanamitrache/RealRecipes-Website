using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCRecipesSite.Controllers;
using MVCRecipesSite.Data;
using MVCRecipesSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MVCRecipesSite.Tests
{
    public class BrowseRecipesControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m=>m.Recipes).Returns(
                (new Recipe[]
                {
                    new Recipe(){RecipeId=1, Name="R1"},
                    new Recipe(){RecipeId=2, Name="R2"}
                }).AsQueryable());

            BrowseRecipesController controller = new BrowseRecipesController(mock.Object);
            controller.PageSize = 2;

            //Act
            IEnumerable<Recipe>? result = ((ViewResult)controller.Index()).ViewData.Model as IEnumerable<Recipe>;

            //Assert
            Recipe[] recipeArray = result?.ToArray() ?? Array.Empty<Recipe>();
            Assert.True(recipeArray.Length == 2);
            Assert.Equal("R1", recipeArray[0].Name);
            Assert.Equal("R2", recipeArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipes).Returns(
                (new Recipe[]
                {
                    new Recipe(){RecipeId=1, Name="R1"},
                    new Recipe(){RecipeId=2, Name="R2"},
                    new Recipe(){RecipeId=3, Name="R3"},
                    new Recipe(){RecipeId=4, Name="R4"},
                    new Recipe(){RecipeId=5, Name="R5"}
                }).AsQueryable());

            BrowseRecipesController controller = new BrowseRecipesController(mock.Object);
            controller.PageSize = 3;

            //Act
            IEnumerable<Recipe> result = (controller.Index(2) as ViewResult)?.ViewData.Model as IEnumerable<Recipe> ?? Enumerable.Empty<Recipe>();

            //Assert
            Recipe[] recipeArray = result.ToArray();
            Assert.True(recipeArray.Length == 2);
            Assert.Equal("R4", recipeArray[0].Name);
            Assert.Equal("R5", recipeArray[1].Name);
        }
    }
}