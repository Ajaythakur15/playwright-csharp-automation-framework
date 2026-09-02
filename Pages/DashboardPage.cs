using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class DashboardPage(IPage page) : BasePage(page)
{
    private ILocator Header => Page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" });
    private ILocator UserDropdown => Page.Locator(".oxd-userdropdown-tab");
    private ILocator LogoutMenuItem => Page.GetByRole(AriaRole.Menuitem, new() { Name = "Logout" });

    public Task WaitUntilVisibleAsync() => Header.WaitForAsync(new() { State = WaitForSelectorState.Visible });

    public async Task<LoginPage> LogoutAsync()
    {
        await UserDropdown.ClickAsync();
        await LogoutMenuItem.ClickAsync();
        var login = new LoginPage(Page);
        await login.WaitUntilVisibleAsync();
        return login;
    }
}
