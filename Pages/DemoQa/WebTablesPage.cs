using Microsoft.Playwright;
namespace PlaywrightFramework.Pages.DemoQa;
public sealed class WebTablesPage(IPage page) : BasePage(page)
{
    public Task OpenAsync(string baseUrl) => NavigateAsync($"{baseUrl}webtables");
    public async Task AddPersonAsync(string firstName, string lastName, string email) { await Page.Locator("#addNewRecordButton").ClickAsync(); await Page.Locator("#firstName").FillAsync(firstName); await Page.Locator("#lastName").FillAsync(lastName); await Page.Locator("#userEmail").FillAsync(email); await Page.Locator("#age").FillAsync("30"); await Page.Locator("#salary").FillAsync("10000"); await Page.Locator("#department").FillAsync("QA"); await Page.Locator("#submit").ClickAsync(); }
    public Task<bool> ContainsAsync(string email) => Page.GetByText(email, new() { Exact = true }).IsVisibleAsync();
}
