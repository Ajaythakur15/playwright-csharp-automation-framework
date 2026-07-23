using Microsoft.Extensions.Configuration;

namespace PlaywrightFramework.Utilities;

public static class ConfigReader
{
    private static readonly IConfigurationRoot Config;

    static ConfigReader()
    {
        Config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false)
            .AddEnvironmentVariables(prefix: "PW_")
            .Build();
    }

    public static string GetRequired(string key)
    {
        return Config[key] ?? throw new InvalidOperationException($"Required configuration value '{key}' is missing.");
    }

    public static bool GetBoolean(string key, bool defaultValue = false) =>
        bool.TryParse(Config[key], out var value) ? value : defaultValue;
}
