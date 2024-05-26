namespace DietPlanner.Services.Tests.Validators;

[TestClass]
public class UpdateDietEntryValidatorTests
{   
    private UpdateDietEntryValidator updateDietEntryValidator;


    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {

    }

    [TestInitialize]
    public void TestInitialize()
    {
        updateDietEntryValidator = new UpdateDietEntryValidator();
    }

    [TestMethod]
    public void UpdateDietEntryValidator_WhenValidObject_ShouldNotHaveAnyErrors()
    {
        // Arrange
        var model = new UpdateDietEntry
        {
            ID = 1,
            Date = DateTime.Now,
            FoodName = "Apple",
            MealTypeID = 1,
            Calories = 100
        };

        // Act
        var result = updateDietEntryValidator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [TestMethod]
    public void UpdateDietEntryValidator_WhenInvalidObject_ShouldHaveErrors()
    {
        // Arrange
        var model = new UpdateDietEntry
        {
            ID = 1,
            Date = DateTime.Now,
            FoodName = "Apple",
            MealTypeID = 1,
            Calories = -100
        };

        // Act
        var result = updateDietEntryValidator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
    }
}
