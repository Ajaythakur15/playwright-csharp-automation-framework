using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class SidebarPage(IPage page) : BasePage(page)
{
    /// <summary>
    /// Sidebar menu item -> heading shown once the module has loaded. Most modules echo the
    /// menu name in the topbar; "My Info" opens the employee's PIM record and "Maintenance"
    /// first asks for administrator re-authentication.
    /// </summary>
    public static readonly IReadOnlyDictionary<string, string> ExpectedHeadings = new Dictionary<string, string>
    {
        ["Admin"] = "Admin",
        ["PIM"] = "PIM",
        ["Leave"] = "Leave",
        ["Time"] = "Time",
        ["Recruitment"] = "Recruitment",
        ["My Info"] = "Personal Details",
        ["Performance"] = "Performance",
        ["Dashboard"] = "Dashboard",
        ["Directory"] = "Directory",
        ["Maintenance"] = "Administrator Access",
        ["Buzz"] = "Buzz"
    };

    private ILocator Menu => Page.Locator(".oxd-sidepanel");
    private ILocator MenuLink(string name) => Menu.GetByRole(AriaRole.Link, new() { Name = name, Exact = true });
    private ILocator Heading(string text) => Page.GetByRole(AriaRole.Heading, new() { Name = text, Exact = true }).First;

    public static string ExpectedHeadingFor(string menu) =>
        ExpectedHeadings.TryGetValue(menu, out var heading) ? heading : menu;

    public async Task NavigateToAsync(string menu)
    {
        await MenuLink(menu).ClickAsync();
        await Heading(ExpectedHeadingFor(menu)).WaitForAsync(new() { State = WaitForSelectorState.Visible });
    }

    public async Task<string> GetPageHeadingAsync(string menu)
    {
        var heading = Heading(ExpectedHeadingFor(menu));
        await heading.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        return (await heading.InnerTextAsync()).Trim();
    }
}
