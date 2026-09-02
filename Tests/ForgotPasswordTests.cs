using NUnit.Framework;
using PlaywrightFramework.Pages;
using PlaywrightFramework.Utilities;

namespace PlaywrightFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class ForgotPasswordTests : BaseTest
{
    [Test]
    [Category("Regression")]
    [Explicit("The public OrangeHRM demo never completes the reset-password request (mail sending hangs), so this only passes against an instance with mail configured.")]
    public async Task Reset_password_request_shows_confirmation()
    {
        var login = new LoginPage(Page);

        await login.NavigateAsync(ConfigReader.GetRequired("OrangeHrmBaseUrl"));
        var forgot = await login.OpenForgotPasswordAsync();
        await forgot.RequestResetAsync(ConfigReader.GetRequired("Username"));

        Assert.That(await forgot.GetConfirmationMessageAsync(), Is.EqualTo("Reset Password link sent successfully"));
    }
}
