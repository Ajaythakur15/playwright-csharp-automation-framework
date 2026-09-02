using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class AdminPage(IPage page) : BasePage(page)
{
    private ILocator SystemUsersHeading => Page.GetByRole(AriaRole.Heading, new() { Name = "System Users" });
    private ILocator UsernameField => Page.Locator(".oxd-form .oxd-input-group").Filter(new() { HasText = "Username" }).Locator("input");
    private ILocator SearchButton => Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
    private ILocator ResultsTable => Page.Locator(".oxd-table");
    private ILocator ResultRows => Page.Locator(".oxd-table-body .oxd-table-card");
    private ILocator RecordsFoundText => Page.Locator(".orangehrm-horizontal-padding .oxd-text--span");

    public Task WaitUntilVisibleAsync() => SystemUsersHeading.WaitForAsync(new() { State = WaitForSelectorState.Visible });

    public async Task SearchUserAsync(string username)
    {
        await UsernameField.FillAsync(username);
        await SearchButton.ClickAsync();
        await ResultsTable.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        await RecordsFoundText.WaitForAsync(new() { State = WaitForSelectorState.Visible });
    }

    public Task<int> GetResultRowCountAsync() => ResultRows.CountAsync();

    public async Task<IReadOnlyList<string>> GetResultUsernamesAsync()
    {
        var usernames = new List<string>();
        for (var i = 0; i < await ResultRows.CountAsync(); i++)
        {
            var cell = ResultRows.Nth(i).GetByRole(AriaRole.Cell).Nth(1);
            usernames.Add((await cell.InnerTextAsync()).Trim());
        }

        return usernames;
    }
}
