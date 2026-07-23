using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class DashboardPage(IPage page) : BasePage(page)
{
    private ILocator Header => Page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" });

    public Task WaitUntilVisibleAsync() => Header.WaitForAsync(new() { State = WaitForSelectorState.Visible });
}
