using Microsoft.Playwright;

namespace POCTests.Config;

public class BrowserConfig
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    public IPage? Page { get; private set; }

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var context = await _browser.NewContextAsync();
        Page = await context.NewPageAsync();
    }

    public async Task CloseAsync()
    {
        if (Page != null)
        {
            await Page.CloseAsync();
        }

        if (_browser != null)
        {
            await _browser.CloseAsync();
        }

        _playwright?.Dispose();
    }
}
