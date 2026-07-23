using NUnit.Framework;
using PlaywrightFramework.Pages.DemoQa;
using PlaywrightFramework.Utilities;
namespace PlaywrightFramework.Tests;
[TestFixture]
[NonParallelizable]
[Category("DemoQA")]
public sealed class DemoQaPracticeTests : BaseTest
{
    private static string BaseUrl => ConfigReader.GetRequired("DemoQaBaseUrl");
    [Test] public async Task Practice_form_can_be_submitted() { var form = new PracticeFormPage(Page); await form.OpenAsync(BaseUrl); await form.SubmitAsync("Ajay", "Tester", "ajay.tester@example.com"); Assert.That(await form.SubmittedNameAsync(), Does.Contain("Ajay Tester")); }
    [Test] public async Task Web_table_can_add_a_record() { var table = new WebTablesPage(Page); const string email = "ajay.table@example.com"; await table.OpenAsync(BaseUrl); await table.AddPersonAsync("Ajay", "Table", email); Assert.That(await table.ContainsAsync(email), Is.True); }
    [Test] public async Task Alert_can_be_accepted() { var alerts = new AlertsPage(Page); string? message = null; Page.Dialog += async (_, dialog) => { message = dialog.Message; await dialog.AcceptAsync(); }; await alerts.OpenAsync(BaseUrl); await alerts.OpenAlertAsync(); await Task.Delay(100); Assert.That(message, Is.EqualTo("You clicked a button")); }
    [Test] public async Task File_can_be_uploaded() { var upload = new UploadPage(Page); var file = Path.Combine(TestContext.CurrentContext.WorkDirectory, "demoqa-upload.txt"); await File.WriteAllTextAsync(file, "DemoQA Playwright upload"); await upload.OpenAsync(BaseUrl); await upload.UploadAsync(file); Assert.That(await upload.UploadedFileNameAsync(), Does.Contain("demoqa-upload.txt")); }
    [Test] public async Task Select_menu_can_choose_a_widget_option() { var menu = new SelectMenuPage(Page); await menu.OpenAsync(BaseUrl); await menu.SelectTitleAsync("Group 1, option 1"); Assert.That(await menu.SelectedTitleAsync(), Does.Contain("Group 1, option 1")); }
}
