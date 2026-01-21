using Microsoft.Playwright;
namespace POCTests.Config;

public enum BrowserTypeOption
{
    Chromium,
    Firefox,
    WebKit
}

public class BrowserConfig
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    public IPage? Page { get; private set; }

    public async Task InitializeAsync()
    {
        var browserEnv = Environment.GetEnvironmentVariable("BROWSER") ?? "Chromium";
        var browserType = Enum.TryParse<BrowserTypeOption>(browserEnv, true, out var type) ? type : BrowserTypeOption.Chromium;

        _playwright = await Playwright.CreateAsync();

        _browser = browserType switch
        {
            BrowserTypeOption.Chromium => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true }),
            BrowserTypeOption.Firefox => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true }),
            BrowserTypeOption.WebKit => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true }),
            _ => throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null)
        };

        var context = await _browser.NewContextAsync();
        Page = await context.NewPageAsync();
    }

    public async Task CloseAsync()
    {
        if (Page != null)
            await Page.CloseAsync();
        if (_browser != null)
            await _browser.CloseAsync();
        _playwright?.Dispose();
    }
}