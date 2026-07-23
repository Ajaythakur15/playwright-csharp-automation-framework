using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class LoginPage(IPage page) : BasePage(page)
{
    private ILocator Username => Page.Locator("input[name='username']");
    private ILocator Password => Page.Locator("input[name='password']");
    private ILocator Submit => Page.Locator("button[type='submit']");

    public async Task LoginAsync(string username, string password)
    {
        await Username.FillAsync(username);
        await Password.FillAsync(password);
        await Submit.ClickAsync();
    }
}
