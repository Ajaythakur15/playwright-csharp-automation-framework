using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class LoginTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    public async Task Valid_credentials_open_dashboard()
    {
        var login = new LoginPage(Page);
        var dashboard = new DashboardPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), ConfigReader.GetRequired("Password"));
        await dashboard.WaitUntilVisibleAsync();
    }

    [Test]
    [Category("Regression")]
    public async Task Invalid_credentials_show_error()
    {
        var login = new LoginPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync("InvalidUser", "wrongPassword");

        Assert.That(await login.GetErrorMessageAsync(), Is.EqualTo("Invalid credentials"));
    }

    [Test]
    [Category("Regression")]
    public async Task Empty_username_and_password_show_required_messages()
    {
        var login = new LoginPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(string.Empty, string.Empty);

        Assert.That(await login.GetRequiredValidationCountAsync(), Is.EqualTo(2));
    }

    [Test]
    [Category("Regression")]
    public async Task Empty_password_shows_single_required_message()
    {
        var login = new LoginPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), string.Empty);

        Assert.That(await login.GetRequiredValidationCountAsync(), Is.EqualTo(1));
    }

    [Test]
    [Category("Regression")]
    public async Task Empty_username_shows_single_required_message()
    {
        var login = new LoginPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(string.Empty, ConfigReader.GetRequired("Password"));

        Assert.That(await login.GetRequiredValidationCountAsync(), Is.EqualTo(1));
    }
}
