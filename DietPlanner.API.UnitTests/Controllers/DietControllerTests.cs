using DietPlanner.API.Controllers;
using DietPlanner.Fakes.Services;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.API.UnitTests.Controllers;

[TestClass]
public class DietControllerTests
{
    private FakeDietService fakeDietService = null;
    private DietController systemUnderTest = null;
    private string userId = Guid.NewGuid().ToString();

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {

    }

    [TestInitialize]
    public void TestInitialize()
    {
        fakeDietService = new FakeDietService();
        systemUnderTest = new DietController(fakeDietService);
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalled_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = false;

        // Act
        var result = await systemUnderTest.Get(1);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietEntry = okObjectResult.Value as ViewDietEntry;
        dietEntry.Should().NotBeNull();
        dietEntry.ID.Should().Be(1);
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithEmptyList_ShouldReturnOkAndEmpty()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = true;

        // Act
        var result = await systemUnderTest.Get(1);

        // Assert
        result.Should().NotBeNull();
        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietEntry = okObjectResult.Value as ViewDietEntry;
        dietEntry.Should().NotBeNull();
        dietEntry.ID.Should().Be(0);
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithValidUserAndDate_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = false;

        // Act
        var result = await systemUnderTest.Index(userId, DateTime.Now);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietEntries = okObjectResult.Value as List<ViewDietEntry>;
        dietEntries.Should().NotBeNull();
        dietEntries.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithInvalidUserAndValidDate_ShouldReturnEmpty()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = true;

        // Act
        var result = await systemUnderTest.Index("invalid", DateTime.Now);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietEntries = okObjectResult.Value as List<ViewDietEntry>;
        dietEntries.Should().NotBeNull();
        dietEntries.Count.Should().Be(0);
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithValidUser_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = false;

        // Act
        var result = await systemUnderTest.GetSummary(userId);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietSummary = okObjectResult.Value as List<ViewDietSummary>;
        dietSummary.Should().NotBeNull();
        dietSummary.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithInvalidUser_ShouldReturnEmpty()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = true;

        // Act
        var result = await systemUnderTest.GetSummary("invalid");

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietSummary = okObjectResult.Value as List<ViewDietSummary>;
        dietSummary.Should().NotBeNull();
        dietSummary.Count.Should().Be(0);
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithValidUserAndDate_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = false;

        // Act
        var result = await systemUnderTest.GetSummary(userId, DateTime.Now);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietSummary = okObjectResult.Value as List<ViewDietSummary>;
        dietSummary.Should().NotBeNull();
        dietSummary.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithInvalidUserAndValidDate_ShouldReturnEmpty()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = true;

        // Act
        var result = await systemUnderTest.GetSummary("invalid", DateTime.Now);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietSummary = okObjectResult.Value as List<ViewDietSummary>;
        dietSummary.Should().NotBeNull();
        dietSummary.Count.Should().Be(0);
    }

    [TestMethod]
    public async Task GetDietEntry_WhenCalledWithValidId_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = false;

        // Act
        var result = await systemUnderTest.Get(1);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietEntry = okObjectResult.Value as ViewDietEntry;
        dietEntry.Should().NotBeNull();
        dietEntry.ID.Should().Be(1);
    }

    [TestMethod]
    public async Task GetDietEntry_WhenCalledWithInvalidId_ShouldReturnEmpty()
    {
        // Arrange
        fakeDietService.IsEntryListEmpty = true;

        // Act
        var result = await systemUnderTest.Get(0);

        // Assert
        result.Should().NotBeNull();

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();

        var dietEntry = okObjectResult.Value as ViewDietEntry;
        dietEntry.Should().NotBeNull();
        dietEntry.ID.Should().Be(0);
    }

    [TestMethod]
    public async Task UpdateDietEntry_WhenCalledWithValidEntry_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsUpdateFail = false;
        var entry = new UpdateDietEntry
        {
            ID = 1,
            FoodName = "Test Food",
            Calories = 100,
            MealTypeID = 1
        };

        // Act
        var result = await systemUnderTest.Patch(entry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [TestMethod]
    public async Task UpdateDietEntry_WhenCalledWithInvalidEntry_ShouldReturnBadRequest()
    {
        // Arrange
        fakeDietService.IsUpdateFail = true;
        var entry = new UpdateDietEntry
        {
            ID = 0,
            FoodName = "Test Food",
            Calories = 0,
            MealTypeID = 0
        };

        // Act
        var result = await systemUnderTest.Patch(entry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestResult>();
    }

    [TestMethod]
    public async Task DeleteDietEntry_WhenCalledWithValidEntry_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsDeleteFail = false;
        var deleteEntry = new DeleteEntry
        {
            Id = 1,
            UserID = userId
        };

        // Act
        var result = await systemUnderTest.Delete(deleteEntry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [TestMethod]
    public async Task DeleteDietEntry_WhenCalledWithInvalidEntry_ShouldReturnBadRequest()
    {
        // Arrange
        fakeDietService.IsDeleteFail = true;
        var deleteEntry = new DeleteEntry
        {
            Id = 0,
            UserID = userId
        };

        // Act
        var result = await systemUnderTest.Delete(deleteEntry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestResult>();
    }

    [TestMethod]
    public async Task AddDietEntry_WhenCalledWithValidEntry_ShouldReturnOk()
    {
        // Arrange
        fakeDietService.IsAddFail = false;
        var entry = new AddDietEntry
        {
            Date = DateTime.Now,
            FoodName = "Test Food",
            Calories = 100,
            MealTypeID = 1,
            UserID = userId
        };

        // Act
        var result = await systemUnderTest.Post(entry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [TestMethod]
    public async Task AddDietEntry_WhenCalledWithInvalidEntry_ShouldReturnBadRequest()
    {
        // Arrange
        fakeDietService.IsAddFail = true;
        var entry = new AddDietEntry
        {
            Date = DateTime.Now,
            FoodName = "Test Food",
            Calories = 0,
            MealTypeID = 0,
            UserID = userId
        };

        // Act
        var result = await systemUnderTest.Post(entry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestResult>();
    }

    [TestMethod]
    public async Task AddDietEntry_WhenCalledWithInvalidUser_ShouldReturnBadRequest()
    {
        // Arrange
        fakeDietService.IsAddFail = true;
        var entry = new AddDietEntry
        {
            Date = DateTime.Now,
            FoodName = "Test Food",
            Calories = 100,
            MealTypeID = 1,
            UserID = "invalid"
        };

        // Act
        var result = await systemUnderTest.Post(entry);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestResult>();
    }

}
