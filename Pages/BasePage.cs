using Microsoft.Playwright;

namespace PlaywrightFramework.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page) => Page = page;

    public Task NavigateAsync(string url) =>
        Page.GotoAsync(url, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
}
