using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class PimTests : BaseTest
{
    private async Task<PimPage> OpenPimAsync()
    {
        var login = new LoginPage(Page);
        var dashboard = new DashboardPage(Page);
        var sidebar = new SidebarPage(Page);
        var pim = new PimPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        await login.LoginAsync(ConfigReader.GetRequired("Username"), ConfigReader.GetRequired("Password"));
        await dashboard.WaitUntilVisibleAsync();
        await sidebar.NavigateToAsync("PIM");
        await pim.WaitUntilVisibleAsync();
        return pim;
    }

    [Test]
    [Category("Regression")]
    public async Task Add_employee_shows_success_toast()
    {
        var pim = await OpenPimAsync();
        var suffix = Guid.NewGuid().ToString("N")[..6];

        await pim.AddEmployeeAsync($"Auto{suffix}", "Tester");

        var toast = await pim.GetSuccessToastMessageAsync();

        Assert.That(toast.Contains("Success") || await pim.IsPersonalDetailsVisibleAsync(), Is.True,
            $"Expected a success toast or the Personal Details page after saving; toast was '{toast}'.");
    }

    [Test]
    [Category("Regression")]
    public async Task Search_existing_employee_returns_results()
    {
        var pim = await OpenPimAsync();

        await pim.SearchEmployeeAsync("a");

        Assert.That(await pim.GetResultRowCountAsync(), Is.GreaterThan(0));
    }
}
