using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page) => Page = page;

    public Task NavigateAsync(string url) =>
        Page.GotoAsync(url, new() { WaitUntil = WaitUntilState.DOMContentLoaded });

    /// <summary>
    /// Runs a search action, waits for the matching OrangeHRM list API response and then waits until the
    /// "(N) Record(s) Found" / "No Records Found" label reflects that response's <c>meta.total</c>,
    /// so callers never observe the rows rendered before the search.
    /// </summary>
    protected async Task SearchAndWaitForResultsAsync(Func<Task> search, string apiPathFragment)
    {
        var response = await Page.RunAndWaitForResponseAsync(
            search,
            r => r.Url.Contains(apiPathFragment) && r.Request.Method == "GET" && r.Ok);

        var total = (await response.JsonAsync())?.GetProperty("meta").GetProperty("total").GetInt32() ?? 0;
        var expectedLabel = total == 0 ? "No Records Found" : $"({total}) Record";

        await Page.Locator(".orangehrm-horizontal-padding .oxd-text--span")
            .Filter(new() { HasText = expectedLabel })
            .WaitForAsync(new() { State = WaitForSelectorState.Visible });
    }
}
