using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class DashboardTests : BaseTest
{
    [Test]
    [Category("Regression")]
    public async Task Dashboard_loads_after_login()
    {
        var login = new LoginPage(Page);
        var dashboard = new DashboardPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), ConfigReader.GetRequired("Password"));
        await dashboard.WaitUntilVisibleAsync();
    }
}
