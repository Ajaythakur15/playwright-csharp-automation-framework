using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public sealed class MfaPage(IPage page) : BasePage(page)
{
    private ILocator OtpField => Page.GetByPlaceholder("Enter the code");
    private ILocator Continue => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public async Task EnterOtpAsync(string otp)
    {
        await OtpField.FillAsync(otp);
        await Continue.ClickAsync();
    }
}
