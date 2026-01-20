using Microsoft.Playwright;
using POCTests.Extensions;

namespace POCTests.Pages;

public class UserFormPage
{
    private readonly IPage _page;
    private const string BaseUrl = "http://localhost:3000";
    
    // Locators - Información Personal
    private ILocator NameInput => _page.Locator("#name");
    private ILocator EmailInput => _page.Locator("#email");
    private ILocator AgeInput => _page.Locator("#age");
    private ILocator PhoneInput => _page.Locator("#phone");
    private ILocator AddressInput => _page.Locator("#address");
    private ILocator CountrySelect => _page.Locator("#country");
    private ILocator BirthDateInput => _page.Locator("#birthDate");
    
    // Locators - Radio buttons género
    private ILocator MaleRadio => _page.Locator("input[name='gender'][value='masculino']");
    private ILocator FemaleRadio => _page.Locator("input[name='gender'][value='femenino']");
    private ILocator OtherGenderRadio => _page.Locator("input[name='gender'][value='otro']");
    
    // Locators - Información Profesional
    private ILocator CompanyInput => _page.Locator("#company");
    private ILocator PositionInput => _page.Locator("#position");
    private ILocator ExperienceInput => _page.Locator("#experience");
    private ILocator BioTextarea => _page.Locator("#bio");
    private ILocator SkillsInput => _page.Locator("#skills");
    
    // Locators - Checkboxes idiomas
    private ILocator SpanishLanguage => _page.Locator("input[name='language'][value='español']");
    private ILocator EnglishLanguage => _page.Locator("input[name='language'][value='inglés']");
    private ILocator FrenchLanguage => _page.Locator("input[name='language'][value='francés']");
    private ILocator PortugueseLanguage => _page.Locator("input[name='language'][value='portugués']");
    private ILocator MandarinLanguage => _page.Locator("input[name='language'][value='mandarin']");
    
    // Locators - Select salario y disponibilidad
    private ILocator SalaryRange => _page.Locator("#salary");
    private ILocator AvailabilitySelect => _page.Locator("#availability");
    
    // Locators - Radio buttons tipo de contrato
    private ILocator FullTimeRadio => _page.Locator("input[name='contractType'][value='tiempo-completo']");
    private ILocator PartTimeRadio => _page.Locator("input[name='contractType'][value='medio-tiempo']");
    private ILocator FreelanceRadio => _page.Locator("input[name='contractType'][value='freelance']");
    private ILocator TemporalRadio => _page.Locator("input[name='contractType'][value='temporal']");
    
    // Locators - File y checkboxes finales
    private ILocator AvatarInput => _page.Locator("#avatar");
    private ILocator TermsCheckbox => _page.Locator("#terms");
    private ILocator NewsletterCheckbox => _page.Locator("#newsletter");
    
    // Locators - Button y mensaje
    private ILocator SubmitButton => _page.Locator("button[type='submit']");
    private ILocator MessageElement => _page.Locator("#message");

    public UserFormPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync(BaseUrl);
    }

    // ============ Métodos de Información Personal ============
    public async Task FillNameAsync(string name)
    {
        await NameInput.FillWithDelayAsync(name);
    }

    public async Task FillEmailAsync(string email)
    {
        await EmailInput.FillWithDelayAsync(email);
    }

    public async Task FillAgeAsync(string age)
    {
        await AgeInput.FillWithDelayAsync(age);
    }

    public async Task FillPhoneAsync(string phone)
    {
        await PhoneInput.FillWithDelayAsync(phone);
    }

    public async Task FillAddressAsync(string address)
    {
        await AddressInput.FillWithDelayAsync(address);
    }

    public async Task SelectCountryAsync(string country)
    {
        await CountrySelect.SelectWithDelayAsync(country);
    }

    public async Task FillBirthDateAsync(string date)
    {
        await BirthDateInput.FillWithDelayAsync(date);
    }

    // ============ Métodos de Radio Buttons Género ============
    public async Task SelectGenderAsync(string gender)
    {
        switch (gender.ToLower())
        {
            case "masculino":
            case "male":
                await MaleRadio.CheckWithDelayAsync();
                break;
            case "femenino":
            case "female":
                await FemaleRadio.CheckWithDelayAsync();
                break;
            case "otro":
            case "other":
                await OtherGenderRadio.CheckWithDelayAsync();
                break;
        }
    }

    // ============ Métodos de Información Profesional ============
    public async Task FillCompanyAsync(string company)
    {
        await CompanyInput.FillWithDelayAsync(company);
    }

    public async Task FillPositionAsync(string position)
    {
        await PositionInput.FillWithDelayAsync(position);
    }

    public async Task FillExperienceAsync(string years)
    {
        await ExperienceInput.FillWithDelayAsync(years);
    }

    public async Task FillBioAsync(string bio)
    {
        await BioTextarea.FillWithDelayAsync(bio);
    }

    public async Task FillSkillsAsync(string skills)
    {
        await SkillsInput.FillWithDelayAsync(skills);
    }

    // ============ Métodos de Idiomas ============
    public async Task SelectLanguageAsync(string language)
    {
        switch (language.ToLower())
        {
            case "español":
            case "spanish":
                await SpanishLanguage.CheckWithDelayAsync();
                break;
            case "inglés":
            case "english":
                await EnglishLanguage.CheckWithDelayAsync();
                break;
            case "francés":
            case "french":
                await FrenchLanguage.CheckWithDelayAsync();
                break;
            case "portugués":
            case "portuguese":
                await PortugueseLanguage.CheckWithDelayAsync();
                break;
            case "mandarin":
            case "chino":
                await MandarinLanguage.CheckWithDelayAsync();
                break;
        }
    }

    // ============ Métodos de Salario y Disponibilidad ============
    public async Task SetSalaryAsync(string salary)
    {
        await SalaryRange.FillWithDelayAsync(salary);
    }

    public async Task SelectAvailabilityAsync(string availability)
    {
        await AvailabilitySelect.SelectWithDelayAsync(availability);
    }

    // ============ Métodos de Tipo de Contrato ============
    public async Task SelectContractTypeAsync(string contractType)
    {
        switch (contractType.ToLower())
        {
            case "tiempo-completo":
            case "full-time":
                await FullTimeRadio.CheckWithDelayAsync();
                break;
            case "medio-tiempo":
            case "part-time":
                await PartTimeRadio.CheckWithDelayAsync();
                break;
            case "freelance":
                await FreelanceRadio.CheckWithDelayAsync();
                break;
            case "temporal":
            case "temporary":
                await TemporalRadio.CheckWithDelayAsync();
                break;
        }
    }

    // ============ Métodos de Consentimientos ============
    public async Task CheckTermsAsync()
    {
        await TermsCheckbox.CheckWithDelayAsync();
    }

    public async Task CheckNewsletterAsync()
    {
        await NewsletterCheckbox.CheckWithDelayAsync();
    }

    // ============ Métodos de Submit y Mensaje ============
    public async Task SubmitFormAsync()
    {
        await SubmitButton.ClickWithDelayAsync();
    }

    public async Task<string> GetMessageTextAsync()
    {
        var text = await MessageElement.TextContentAsync();
        return text ?? string.Empty;
    }

    // ============ Método Helper para Formulario Completo ============
    public async Task FillCompleteFormAsync(
        string name, string email, string age, string phone, string address, 
        string country, string gender, string birthDate,
        string company, string position, string experience,
        List<string> languages, string salary, string availability, 
        string contractType, string bio, string skills,
        bool acceptTerms = true, bool subscribeNewsletter = false)
    {
        // Información Personal
        await FillNameAsync(name);
        await FillEmailAsync(email);
        await FillAgeAsync(age);
        await FillPhoneAsync(phone);
        await FillAddressAsync(address);
        await SelectCountryAsync(country);
        await SelectGenderAsync(gender);
        await FillBirthDateAsync(birthDate);
        
        // Información Profesional
        await FillCompanyAsync(company);
        await FillPositionAsync(position);
        await FillExperienceAsync(experience);
        
        // Idiomas
        foreach (var lang in languages)
        {
            await SelectLanguageAsync(lang);
        }
        
        // Salario, Disponibilidad y Contrato
        await SetSalaryAsync(salary);
        await SelectAvailabilityAsync(availability);
        await SelectContractTypeAsync(contractType);
        
        // Descripción y Habilidades
        await FillBioAsync(bio);
        await FillSkillsAsync(skills);
        
        // Consentimientos
        if (acceptTerms)
        {
            await CheckTermsAsync();
        }
        
        if (subscribeNewsletter)
        {
            await CheckNewsletterAsync();
        }
    }

    // ============ Métodos Legacy para Compatibilidad ============
    public async Task FillFormAsync(string name, string email, string age)
    {
        await FillNameAsync(name);
        await FillEmailAsync(email);
        await FillAgeAsync(age);
    }
}
