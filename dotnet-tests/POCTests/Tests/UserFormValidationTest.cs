using Microsoft.Playwright;
using POCTests.Config;
using POCTests.Pages;
using Xunit;
using POCTests.Utils;

namespace POCTests.Tests;

/// <summary>
/// Tests that verify validation messages for invalid user form data.
/// </summary>
public class UserFormValidationTests : TestBase
{
    // Validation messages as constants
    private const string AllFieldsRequiredMessage = "Todos los campos obligatorios deben estar completos";
    private const string AgeTooYoungMessage = "La edad debe ser mayor o igual a 18";
    private const string CountryRequiredMessage = "Debe seleccionar un país/ciudad";
    private const string GenderRequiredMessage = "Debe seleccionar un género";
    private const string TermsRequiredMessage = "Debe aceptar los términos y condiciones";

    /// <summary>
    /// Provides invalid user data and the expected validation message for each scenario.
    /// </summary>
    public static IEnumerable<object[]> InvalidUserFormTestCases()
    {
        // All fields empty
        yield return new object[]
        {
            "", "", "", "", "", "", "", "", "", "", "", new List<string>(), "0", "", "", "", "", false, false,
            AllFieldsRequiredMessage
        };

        // Age less than 18
        var user1 = UserDataGenerator.GenerateUser();
        yield return new object[]
        {
            user1.name, user1.email, "16", user1.phone, user1.address, user1.country, user1.gender, user1.birthDate,
            user1.company, user1.position, user1.experience, user1.languages, user1.salary, user1.availability,
            user1.contractType, user1.bio, user1.skills, user1.acceptTerms, user1.subscribeNewsletter,
            AgeTooYoungMessage
        };

        // Country not selected
        var user2 = UserDataGenerator.GenerateUser();
        yield return new object[]
        {
            user2.name, user2.email, user2.age, user2.phone, user2.address, "", user2.gender, user2.birthDate,
            user2.company, user2.position, user2.experience, user2.languages, user2.salary, user2.availability,
            user2.contractType, user2.bio, user2.skills, user2.acceptTerms, user2.subscribeNewsletter,
            CountryRequiredMessage
        };

        // Gender not selected
        var user3 = UserDataGenerator.GenerateUser();
        yield return new object[]
        {
            user3.name, user3.email, user3.age, user3.phone, user3.address, user3.country, "", user3.birthDate,
            user3.company, user3.position, user3.experience, user3.languages, user3.salary, user3.availability,
            user3.contractType, user3.bio, user3.skills, user3.acceptTerms, user3.subscribeNewsletter,
            GenderRequiredMessage
        };

        // Terms not accepted
        var user4 = UserDataGenerator.GenerateUser();
        yield return new object[]
        {
            user4.name, user4.email, user4.age, user4.phone, user4.address, user4.country, user4.gender, user4.birthDate,
            user4.company, user4.position, user4.experience, user4.languages, user4.salary, user4.availability,
            user4.contractType, user4.bio, user4.skills, false, user4.subscribeNewsletter,
            TermsRequiredMessage
        };
    }

    /// <summary>
    /// Should display the correct validation message for each invalid form scenario.
    /// </summary>
    [Theory]
    [MemberData(nameof(InvalidUserFormTestCases))]
    public async Task Should_Display_Validation_Message_For_Invalid_User_Form_Data(
        string name, string email, string age, string phone, string address, string country, string gender, string birthDate,
        string company, string position, string experience, List<string> languages, string salary, string availability,
        string contractType, string bio, string skills, bool acceptTerms, bool subscribeNewsletter, string expectedMessage)
    {
        // Arrange: Navigate to the user form page using BASE_URL from .env
        await _userFormPage.NavigateAsync();

        // Act: Fill the form with invalid data and submit
        await _userFormPage.FillCompleteFormAsync(
            name, email, age, phone, address, country, gender, birthDate, company, position, experience, languages,
            salary, availability, contractType, bio, skills, acceptTerms, subscribeNewsletter
        );
        await _userFormPage.SubmitFormAsync();

        // Assert: The expected validation message is shown
        var message = await _userFormPage.GetMessageTextAsync();
        Assert.Equal(expectedMessage, message);
    }
}