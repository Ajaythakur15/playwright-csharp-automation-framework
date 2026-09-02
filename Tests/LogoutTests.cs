using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class LogoutTests : BaseTest
{
    [Test]
    [Category("Regression")]
    public async Task Logout_returns_to_login_page()
    {
        var login = new LoginPage(Page);
        var dashboard = new DashboardPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), ConfigReader.GetRequired("Password"));
        await dashboard.WaitUntilVisibleAsync();

        await dashboard.LogoutAsync();

        Assert.That(Page.Url, Does.Contain("/auth/login"));
    }
}
