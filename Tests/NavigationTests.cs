using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class NavigationTests : BaseTest
{
    [TestCase("Admin")]
    [TestCase("PIM")]
    [TestCase("Leave")]
    [TestCase("Time")]
    [TestCase("Recruitment")]
    [TestCase("My Info")]
    [TestCase("Performance")]
    [TestCase("Dashboard")]
    [TestCase("Directory")]
    [TestCase("Maintenance")]
    [TestCase("Buzz")]
    [Category("Regression")]
    public async Task Sidebar_menu_opens_expected_module(string menu)
    {
        var login = new LoginPage(Page);
        var dashboard = new DashboardPage(Page);
        var sidebar = new SidebarPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), ConfigReader.GetRequired("Password"));
        await dashboard.WaitUntilVisibleAsync();

        await sidebar.NavigateToAsync(menu);

        Assert.That(await sidebar.GetPageHeadingAsync(menu), Is.EqualTo(SidebarPage.ExpectedHeadingFor(menu)));
    }
}
