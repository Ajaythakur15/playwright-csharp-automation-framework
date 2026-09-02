using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightFramework.Utilities;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public abstract class BaseTest
{
    protected IPlaywright Playwright = null!;
    protected IBrowser Browser = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    [SetUp]
    public async Task SetUpAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = ConfigReader.GetBoolean("Headless", true),
            SlowMo = float.TryParse(Environment.GetEnvironmentVariable("PW_SlowMo"), out var slowMo) ? slowMo : 0
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = ConfigReader.GetBoolean("IgnoreHttpsErrors")
        });
        Page = await Context.NewPageAsync();
        ReportManager.StartTest(TestContext.CurrentContext.Test.Name);
        Log.Information("Starting {TestName}", TestContext.CurrentContext.Test.FullName);
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var file = await CaptureScreenshotAsync();
            ReportManager.Fail(TestContext.CurrentContext.Result.Message, file);
            Log.Error("Test failed: {TestName}. Screenshot: {Screenshot}", TestContext.CurrentContext.Test.FullName, file);
        }
        else
        {
            ReportManager.Pass();
        }

        await Context.CloseAsync();
        await Browser.CloseAsync();
        Playwright.Dispose();
    }

    private async Task<string> CaptureScreenshotAsync()
    {
        var directory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "screenshots");
        Directory.CreateDirectory(directory);
        var safeName = string.Concat(TestContext.CurrentContext.Test.Name.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
        var path = Path.Combine(directory, $"{safeName}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.png");
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = path, FullPage = true });
        TestContext.AddTestAttachment(path, "Failure screenshot");
        return path;
    }
}
