namespace DietPlanner.API.Tests.Controllers;

[TestClass]
public class MealTypeControllerTests
{
    private HttpClient _httpClient = null!;
    
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
    public async Task IndexMealTypes_WhenCalled_ShouldReturnOk()
    {
        // Arrange
        var testurl = $"/api/mealtype";

        // Act
        var response = await _httpClient.GetFromJsonAsync<List<MealTypeView>>(testurl);

        // Assert
        response.Should().NotBeNull();
        response.Should().NotBeEmpty();
        response.Count.Should().Be(4);
    }
}
