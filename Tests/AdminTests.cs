using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class AdminTests : BaseTest
{
    [Test]
    [Category("Regression")]
    public async Task Search_admin_user_returns_result_row()
    {
        var login = new LoginPage(Page);
        var dashboard = new DashboardPage(Page);
        var sidebar = new SidebarPage(Page);
        var admin = new AdminPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), ConfigReader.GetRequired("Password"));
        await dashboard.WaitUntilVisibleAsync();
        await sidebar.NavigateToAsync("Admin");
        await admin.WaitUntilVisibleAsync();

        await admin.SearchUserAsync("Admin");

        Assert.That(await admin.GetResultRowCountAsync(), Is.GreaterThan(0));
        Assert.That(await admin.GetResultUsernamesAsync(), Has.Some.Contains("Admin").IgnoreCase);
    }
}
