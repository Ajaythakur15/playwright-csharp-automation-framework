using Microsoft.Playwright;
namespace PlaywrightFramework.Pages.DemoQa;
public sealed class SelectMenuPage(IPage page) : BasePage(page)
{
    public Task OpenAsync(string baseUrl) => NavigateAsync($"{baseUrl}select-menu");
    public async Task SelectTitleAsync(string title) { await Page.Locator("#withOptGroup").ClickAsync(); await Page.GetByText(title, new() { Exact = true }).ClickAsync(); }
    public Task<string?> SelectedTitleAsync() => Page.Locator("#withOptGroup").TextContentAsync();
}
