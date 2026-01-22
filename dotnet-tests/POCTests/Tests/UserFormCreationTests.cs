using Microsoft.Playwright;
using POCTests.Config;
using POCTests.Pages;
using Xunit;
using POCTests.Utils;

namespace POCTests.Tests;

/// <summary>
/// Tests that verify successful user creation through the user form.
/// </summary>
public class UserFormCreationTests : TestBase
{
    /// <summary>
    /// Provides valid user data for successful creation scenarios.
    /// </summary>
    private const string SuccessMessage = "Usuario creado correctamente";

    public static IEnumerable<object[]> ValidUserFormTestCases()
    {
        var user = UserDataGenerator.GenerateUser();
        yield return new object[]
        {
            user.name, user.email, user.age, user.phone, user.address, user.country, user.gender, user.birthDate,
            user.company, user.position, user.experience, user.languages, user.salary, user.availability,
            user.contractType, user.bio, user.skills, user.acceptTerms, user.subscribeNewsletter,
            SuccessMessage
        };
    }

    /// <summary>
    /// Should create a user when all form data is valid.
    /// </summary>
    [Theory]
    [MemberData(nameof(ValidUserFormTestCases))]
    public async Task Should_Create_User_When_Form_Data_Is_Valid(
        string name, string email, string age, string phone, string address, string country, string gender, string birthDate,
        string company, string position, string experience, List<string> languages, string salary, string availability,
        string contractType, string bio, string skills, bool acceptTerms, bool subscribeNewsletter, string expectedMessage)
    {
        // Arrange: Navigate to the user form page using BASE_URL from .env
        await _userFormPage.NavigateAsync();

        // Act: Fill the form with valid data and submit
        await _userFormPage.FillCompleteFormAsync(
            name, email, age, phone, address, country, gender, birthDate, company, position, experience, languages,
            salary, availability, contractType, bio, skills, acceptTerms, subscribeNewsletter
        );
        await _userFormPage.SubmitFormAsync();

        // Assert: The expected success message is shown
        var message = await _userFormPage.GetMessageTextAsync();
        Assert.Equal(expectedMessage, message);
    }
}