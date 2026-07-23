# Framework review

## Completed improvements

- Retained NUnit 4 rather than mixing test frameworks; enabled fixture and test parallelism with isolated Playwright contexts.
- Formalized Page Object Model classes under `Pages` and added namespaces, async method names, and stable role-based locators.
- Replaced nullable configuration access with validated settings loaded from the test output directory. Environment variables prefixed `PW_` override the JSON defaults.
- Added lifecycle-safe teardown, failure screenshots attached to NUnit results, daily Serilog logs, and an Extent HTML report.
- Kept Allure.NUnit and supplied its result-directory configuration.
- Added a `.runsettings` file, source-control hygiene, and Azure DevOps/GitHub Actions pipelines.

## Validation

`dotnet build --no-restore` succeeds. `dotnet test --no-restore --settings .runsettings` passed both tests against OrangeHRM on 2026-07-23.

## Remaining recommendation

`ExtentReports` 5.0.4 brings `System.Drawing.Common` 5.0.0 transitively, for which NuGet reports NU1904. The report works, but treat this as a dependency risk: monitor ExtentReports for a supported release, or replace it with a maintained reporting implementation if policy prohibits known-vulnerable transitive dependencies. Do not suppress NU1904 without a recorded risk acceptance.
