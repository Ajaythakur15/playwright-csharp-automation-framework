using Serilog;

namespace PlaywrightFramework.Utilities;

public static class Log
{
    static Log()
    {
        var directory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "logs");
        Directory.CreateDirectory(directory);
        Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(directory, "framework-.log"), rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    private static ILogger Logger { get; }
    public static void Information(string messageTemplate, params object?[] values) => Logger.Information(messageTemplate, values);
    public static void Error(string messageTemplate, params object?[] values) => Logger.Error(messageTemplate, values);
}
