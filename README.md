# PlaywrightFramework

NUnit 4 UI tests using Playwright for .NET, page objects, parallel-safe browser contexts, Serilog file logging, failure screenshots, Extent HTML reporting, and Allure result generation.

## Run locally

```powershell
dotnet restore
pwsh bin/Debug/net10.0/playwright.ps1 install chromium
$env:PW_Headless = "true"
dotnet test --settings .runsettings
```

Configuration defaults live in `Config/appsettings.json`. Use `PW_BaseUrl`, `PW_Username`, `PW_Password`, and `PW_Headless` to override them in a secure CI environment; do not put production credentials in source control.

## Reports

After a run, find `TestResults/extent/index.html`, `TestResults/logs`, failure screenshots, TRX results, and `TestResults/allure-results`. Generate an Allure report with `allure generate TestResults/allure-results --clean -o TestResults/allure-report` when the Allure CLI is installed.

## Test design

Pages contain locators and user actions; tests only express scenarios. Every test has a fresh `IBrowserContext`, so NUnit can safely run them in parallel. Add stable role, label, or test-id locators in new page objects.

GitHub Actions reads `PW_USERNAME` and `PW_PASSWORD` repository secrets. Azure Pipelines reads secret variables named `PW_USERNAME` and `PW_PASSWORD`.
