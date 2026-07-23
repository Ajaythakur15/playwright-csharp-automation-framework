using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace PlaywrightFramework.Utilities;

[SetUpFixture]
public sealed class ReportManager
{
    private static readonly object Sync = new();
    private static ExtentReports? _extent;
    private static readonly AsyncLocal<ExtentTest?> CurrentTest = new();

    [OneTimeSetUp]
    public void CreateReport()
    {
        var directory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "extent");
        Directory.CreateDirectory(directory);
        var reporter = new ExtentSparkReporter(Path.Combine(directory, "index.html"));
        _extent = new ExtentReports();
        _extent.AttachReporter(reporter);
        _extent.AddSystemInfo("Framework", "Playwright .NET / NUnit");
    }

    [OneTimeTearDown]
    public void FlushReport() => _extent?.Flush();

    public static void StartTest(string name)
    {
        lock (Sync) CurrentTest.Value = _extent?.CreateTest(name);
    }

    public static void Pass() => CurrentTest.Value?.Pass("Passed");

    public static void Fail(string? message, string screenshot)
    {
        CurrentTest.Value?.Fail(message ?? "Test failed").AddScreenCaptureFromPath(screenshot);
    }
}
