using Microsoft.Playwright;
using POCTests.Config;
using POCTests.Pages;
using Xunit;
using POCTests.Utils;

namespace POCTests.Tests;

public class UserCreationTests : IAsyncLifetime
{
    private BrowserConfig _browserConfig = null!;
    private UserFormPage _userFormPage = null!;

    public async Task InitializeAsync()
    {
        _browserConfig = new BrowserConfig();
        await _browserConfig.InitializeAsync();
        _userFormPage = new UserFormPage(_browserConfig.Page!);
    }

    public async Task DisposeAsync()
    {
        await _browserConfig.CloseAsync();
    }

    public static IEnumerable<object[]> UserCreationTestCases()
    {
        // Caso exitoso
        var user = UserDataGenerator.GenerateUser();
        yield return new object[]
        {
            user.name, user.email, user.age, user.phone, user.address, user.country, user.gender, user.birthDate,
            user.company, user.position, user.experience, user.languages, user.salary, user.availability,
            user.contractType, user.bio, user.skills, user.acceptTerms, user.subscribeNewsletter,
            "Usuario creado correctamente"
        };
    }

    [Theory]
    [MemberData(nameof(UserCreationTestCases))]
    public async Task ShouldCreateUserSuccessfully(
        string name, string email, string age, string phone, string address, string country, string gender, string birthDate,
        string company, string position, string experience, List<string> languages, string salary, string availability,
        string contractType, string bio, string skills, bool acceptTerms, bool subscribeNewsletter, string expectedMessage)
    {
        // Mock de la API para simular respuesta exitosa
        await _browserConfig.Page!.RouteAsync("/api/users", async route =>
        {
            await route.FulfillAsync(new RouteFulfillOptions
            {
                Status = 201,
                Body = "{\"id\": 1}",
                ContentType = "application/json"
            });
        });

        await _userFormPage.NavigateAsync();

        await _userFormPage.FillCompleteFormAsync(
            name, email, age, phone, address, country, gender, birthDate, company, position, experience, languages,
            salary, availability, contractType, bio, skills, acceptTerms, subscribeNewsletter
        );
        await _userFormPage.SubmitFormAsync();

        var message = await _userFormPage.GetMessageTextAsync();
        Assert.Equal(expectedMessage, message);
    }
}