
namespace DietPlanner.Services.Tests.Validators;

[TestClass]
public class AddDietEntryValidatorTests
{
    private AddDietEntryValidator addDietEntryValidator;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {

    }

    [TestInitialize]
    public void TestInitialize()
    {
        addDietEntryValidator = new AddDietEntryValidator();
    }

    [TestMethod]
    public void AddDietEntryValidator_WhenValidObject_ShouldNotHaveAnyErrors()
    {
        // Arrange
        var model = new AddDietEntry
        {
            Date = DateTime.Now,
            FoodName = "Apple",
            MealTypeID = 1,
            UserID = Guid.NewGuid().ToString(),
            Calories = 100
        };

        // Act
        var result = addDietEntryValidator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [TestMethod]
    public void AddDietEntryValidator_WhenInvalidObject_ShouldHaveErrors()
    {
        // Arrange
        var model = new AddDietEntry
        {
            Date = DateTime.Now,
            FoodName = "Apple",
            MealTypeID = 1,
            UserID = Guid.NewGuid().ToString(),
            Calories = -100
        };

        // Act
        var result = addDietEntryValidator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
    }
}
