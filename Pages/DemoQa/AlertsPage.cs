using Microsoft.Playwright;
namespace PlaywrightFramework.Pages.DemoQa;
public sealed class AlertsPage(IPage page) : BasePage(page)
{
    public Task OpenAsync(string baseUrl) => NavigateAsync($"{baseUrl}alerts");
    public Task OpenAlertAsync() => Page.Locator("#alertButton").ClickAsync();
}
