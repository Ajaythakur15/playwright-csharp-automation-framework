using Microsoft.Playwright;
namespace PlaywrightFramework.Pages.DemoQa;
public sealed class PracticeFormPage(IPage page) : BasePage(page)
{
    public Task OpenAsync(string baseUrl) => NavigateAsync($"{baseUrl}automation-practice-form");
    public async Task SubmitAsync(string firstName, string lastName, string email) { await Page.Locator("#firstName").FillAsync(firstName); await Page.Locator("#lastName").FillAsync(lastName); await Page.Locator("#userEmail").FillAsync(email); await Page.Locator("label[for='gender-radio-1']").ClickAsync(); await Page.Locator("#userNumber").FillAsync("9876543210"); await Page.Locator("#submit").ClickAsync(); }
    public Task<string?> SubmittedNameAsync() => Page.Locator(".modal-content td").Nth(1).TextContentAsync();
}
