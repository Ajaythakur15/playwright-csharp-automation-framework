using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class OTPHelper
{
    public static async Task<string> GetOTP(IBrowserContext context)
    {
        var page = await context.NewPageAsync();

        await page.GotoAsync("https://yopmail.com/en/");
        await page.FillAsync("#login", "asingh");
        await page.ClickAsync("button:has-text('Check Inbox')");

        await page.WaitForTimeoutAsync(5000);

        var frame = page.FrameLocator("#ifmail");
        var text = await frame.Locator("body").InnerTextAsync();

        return Regex.Match(text, @"\d{6}").Value;
    }
}