using DietPlanner.API.Controllers;
using DietPlanner.Fakes.Services;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.API.UnitTests.Controllers;

[TestClass]
public class MealTypeControllerTests
{
    private FakeMealTypeService fakemealTypeService = null;
    private MealTypeController systemUnderTest = null;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {

    }

    [TestInitialize]
    public void TestInitialize()
    {
        fakemealTypeService = new FakeMealTypeService();
        systemUnderTest = new MealTypeController(fakemealTypeService);
    }

    [TestMethod]
    public async Task IndexMealTypes_WhenCalled_ShouldReturnOk()
    {
        // Arrange
        fakemealTypeService.IsListEmpty = false;

        // Act
        var result = await systemUnderTest.Get();

        // Assert
        using var assertionScope = new AssertionScope();
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var mealTypes = okObjectResult.Value as List<MealTypeView>;
        mealTypes.Should().NotBeNull();
        mealTypes.Count.Should().Be(4);
    }

    [TestMethod]
    public async Task IndexMealTypes_WhenCalledWithEmptyList_ShouldReturnOkAndEmpty()
    {
        // Arrange
        fakemealTypeService.IsListEmpty = true;

        // Act
        var result = await systemUnderTest.Get();

        // Assert
        using var assertionScope = new AssertionScope();
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var mealTypes = okObjectResult.Value as List<MealTypeView>;
        mealTypes.Should().NotBeNull();
        mealTypes.Should().BeEmpty();
    }
}
