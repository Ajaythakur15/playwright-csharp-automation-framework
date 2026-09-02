using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class LoginPage(IPage page) : BasePage(page)
{
    private ILocator Username => Page.Locator("input[name='username']");
    private ILocator Password => Page.Locator("input[name='password']");
    private ILocator Submit => Page.Locator("button[type='submit']");
    private ILocator ErrorAlert => Page.Locator(".oxd-alert-content-text");
    private ILocator RequiredMessages => Page.Locator(".oxd-input-field-error-message");
    private ILocator ForgotPasswordLink => Page.GetByText("Forgot your password?");
    private ILocator LoginHeading => Page.GetByRole(AriaRole.Heading, new() { Name = "Login" });

    public async Task LoginAsync(string username, string password)
    {
        await Username.FillAsync(username);
        await Password.FillAsync(password);
        await Submit.ClickAsync();
    }

    public Task WaitUntilVisibleAsync() => LoginHeading.WaitForAsync(new() { State = WaitForSelectorState.Visible });

    public async Task<string> GetErrorMessageAsync()
    {
        await ErrorAlert.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        return (await ErrorAlert.InnerTextAsync()).Trim();
    }

    public async Task<int> GetRequiredValidationCountAsync()
    {
        await RequiredMessages.First.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        return await RequiredMessages.CountAsync();
    }

    public async Task<ForgotPasswordPage> OpenForgotPasswordAsync()
    {
        await ForgotPasswordLink.ClickAsync();
        var forgot = new ForgotPasswordPage(Page);
        await forgot.WaitUntilVisibleAsync();
        return forgot;
    }
}
