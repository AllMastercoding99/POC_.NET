using Microsoft.Playwright;
using POCTests.Config;
using POCTests.Pages;
using Xunit;
using System.IO;

namespace POCTests.Tests;

public abstract class TestBase : IAsyncLifetime
{
    protected BrowserConfig _browserConfig = null!;
    protected UserFormPage _userFormPage = null!;
    protected string BaseUrl { get; private set; } = null!;

    static TestBase()
    {
        var envPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env");
        DotNetEnv.Env.Load(envPath);
    }

    public virtual async Task InitializeAsync()
    {
        _browserConfig = new BrowserConfig();
        await _browserConfig.InitializeAsync();
        BaseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost:3000";
        _userFormPage = new UserFormPage(_browserConfig.Page!, BaseUrl);
    }

    public virtual async Task DisposeAsync()
    {
        if (_browserConfig != null)
            await _browserConfig.CloseAsync();
    }
}