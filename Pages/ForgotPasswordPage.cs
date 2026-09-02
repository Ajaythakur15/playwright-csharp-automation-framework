using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class ForgotPasswordPage(IPage page) : BasePage(page)
{
    private ILocator Heading => Page.GetByRole(AriaRole.Heading, new() { Name = "Reset Password" });
    private ILocator Username => Page.Locator("input[name='username']");
    private ILocator ResetButton => Page.GetByRole(AriaRole.Button, new() { Name = "Reset Password" });
    private ILocator Confirmation => Page.GetByRole(AriaRole.Heading, new() { Name = "Reset Password link sent successfully" });

    public Task WaitUntilVisibleAsync() => Heading.WaitForAsync(new() { State = WaitForSelectorState.Visible });

    public async Task RequestResetAsync(string username)
    {
        await Username.FillAsync(username);
        await ResetButton.ClickAsync();
    }

    public async Task<string> GetConfirmationMessageAsync()
    {
        // The public demo dispatches a reset e-mail before rendering the confirmation, which can take well over the default 30s.
        await Confirmation.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 120_000 });
        return (await Confirmation.InnerTextAsync()).Trim();
    }
}
