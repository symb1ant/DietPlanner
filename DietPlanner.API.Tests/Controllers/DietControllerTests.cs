namespace DietPlanner.API.Tests.Controllers;

[TestClass]
public class DietControllerTests
{
    private HttpClient _httpClient = null!;
    private string userId = Constants.TEST_USER_ID;
    private string todaysDate = DateTime.Now.ToString("yyyy-MM-dd");

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {

    }

    [TestInitialize]
    public void TestInitialize()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(o =>
            {
                o.ConfigureAppConfiguration((c, p) =>
                {
                    p.AddConfiguration(new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
                        {
                                { "ConnectionStrings:DefaultConnection", Constants.CONNECTION_STRING }
                        }).Build());
                });
            });

        _httpClient = application.CreateDefaultClient(new Uri("http://localhost:5068"));
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithValidUser_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet/index/{userId}";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().NotBeNull();
        response.Should().NotBeEmpty();
        response.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithInvalidUser_ShouldReturnEmpty()
    {
        // Arrange
        var testurl = $"/api/diet/index/invalid";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().BeEmpty();
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithValidUserAndDate_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet/index/{userId}/{todaysDate}";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().NotBeNull();
        response.Should().NotBeEmpty();
        response.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task IndexDietEntries_WhenCalledWithInvalidUserAndValidDate_ShouldReturnEmpty()
    {
        // Arrange
        var testurl = $"/api/diet/index/invalid/{todaysDate}";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().BeEmpty();
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithValidUser_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet/summary/{userId}";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().NotBeNull();
        response.Should().NotBeEmpty();
        response.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithInvalidUser_ShouldReturnEmpty()
    {
        // Arrange
        var testurl = $"/api/diet/summary/invalid";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().BeEmpty();
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithValidUserAndDate_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet/summary/{userId}/{todaysDate}";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().NotBeNull();
        response.Should().NotBeEmpty();
        response.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [TestMethod]
    public async Task SummaryDietEntries_WhenCalledWithInvalidUserAndValidDate_ShouldReturnEmpty()
    {
        // Arrange
        var testurl = $"/api/diet/summary/invalid/{todaysDate}";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<ViewDietEntry>>(testurl);

        // Assert
        response.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetDietEntry_WhenCalledWithValidID_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet/get/1";

        // Act
        var response = await _httpClient.GetFromJsonAsync<ViewDietEntry>(testurl);

        // Assert
        response.Should().NotBeNull();        
        response.ID.Should().Be(1);
        response.FoodName.Should().Be("Test Food");
    }

    [TestMethod]
    public async Task PostDietEntry_WhenCalledWithValidEntry_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet";
        var entry = new AddDietEntry
        {
            UserID = userId,
            Date = DateTime.Now,
            FoodName = "Test Food",
            MealTypeID = 1,
            Calories = 100
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(testurl, entry);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task PostDietEntry_WhenCalledWithInvalidEntry_ShouldReturnBadRequest()
    {
        // Arrange
        var testurl = $"/api/diet";
        var entry = new AddDietEntry
        {
            UserID = userId,
            Date = DateTime.Now,
            FoodName = "Test Food",
            MealTypeID = 1,
            Calories = 0
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(testurl, entry);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task UpdateDietEntry_WhenCalledWithValidEntry_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet";
        var entry = new UpdateDietEntry
        {
            ID = 1,
            Date = DateTime.Now,
            FoodName = "Test Food",
            MealTypeID = 2,
            Calories = 102
        };

        // Act
        var response = await _httpClient.PatchAsJsonAsync(testurl, entry);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task UpdateDietEntry_WhenCalledWithInvalidEntry_ShouldReturnBadRequest()
    {
        // Arrange
        var testurl = $"/api/diet";
        var entry = new UpdateDietEntry
        {
            ID = 11,
            Date = DateTime.Now,
            FoodName = "Test Food Invalid",
            MealTypeID = 1,
            Calories = 0
        };

        // Act
        var response = await _httpClient.PatchAsJsonAsync(testurl, entry);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task DeleteDietEntry_WhenCalledWithValidEntry_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/diet/delete";
        var entry = new DeleteEntry
        {
            Id = 2,
            UserID = userId
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(testurl, entry);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task DeleteDietEntry_WhenCalledWithInvalidEntry_ShouldReturnBadRequest()
    {
        // Arrange
        var testurl = $"/api/diet/delete";
        var entry = new DeleteEntry
        {
            Id = 22,
            UserID = userId
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(testurl, entry);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
