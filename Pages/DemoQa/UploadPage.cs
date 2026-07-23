using Microsoft.Playwright;
namespace PlaywrightFramework.Pages.DemoQa;
public sealed class UploadPage(IPage page) : BasePage(page)
{
    public Task OpenAsync(string baseUrl) => NavigateAsync($"{baseUrl}upload-download");
    public Task UploadAsync(string filePath) => Page.Locator("#uploadFile").SetInputFilesAsync(filePath);
    public Task<string?> UploadedFileNameAsync() => Page.Locator("#uploadedFilePath").TextContentAsync();
}
