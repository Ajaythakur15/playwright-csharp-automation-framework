using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class PimPage(IPage page) : BasePage(page)
{
    private ILocator PimHeading => Page.GetByRole(AriaRole.Heading, new() { Name = "PIM" });
    private ILocator AddButton => Page.GetByRole(AriaRole.Button, new() { Name = "Add" });
    private ILocator FirstName => Page.Locator("input[name='firstName']");
    private ILocator LastName => Page.Locator("input[name='lastName']");
    private ILocator SaveButton => Page.GetByRole(AriaRole.Button, new() { Name = "Save" });
    private ILocator SuccessToast => Page.Locator(".oxd-toast--success");
    private ILocator PersonalDetailsHeading => Page.GetByRole(AriaRole.Heading, new() { Name = "Personal Details" });
    private ILocator EmployeeNameSearch => Page.GetByPlaceholder("Type for hints...").First;
    private ILocator SearchButton => Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
    private ILocator ResultRows => Page.Locator(".oxd-table-body .oxd-table-card");

    public Task WaitUntilVisibleAsync() => PimHeading.WaitForAsync(new() { State = WaitForSelectorState.Visible });

    public async Task AddEmployeeAsync(string firstName, string lastName)
    {
        await AddButton.ClickAsync();
        await FirstName.FillAsync(firstName);
        await LastName.FillAsync(lastName);
        await SaveButton.ClickAsync();
    }

    /// <summary>
    /// Returns the success toast text. The toast auto-dismisses after a few seconds, so if the
    /// Personal Details page has already rendered (i.e. the save succeeded and the toast is gone)
    /// an empty string is returned instead of timing out.
    /// </summary>
    public async Task<string> GetSuccessToastMessageAsync()
    {
        await SuccessToast.Or(PersonalDetailsHeading).First.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        return await SuccessToast.IsVisibleAsync() ? (await SuccessToast.InnerTextAsync()).Trim() : string.Empty;
    }

    public Task<bool> IsPersonalDetailsVisibleAsync() => PersonalDetailsHeading.IsVisibleAsync();

    public async Task SearchEmployeeAsync(string name)
    {
        await EmployeeNameSearch.FillAsync(name);
        await SearchAndWaitForResultsAsync(() => SearchButton.ClickAsync(), "/api/v2/pim/employees");
    }

    public Task<int> GetResultRowCountAsync() => ResultRows.CountAsync();
}
