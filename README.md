# POC - Automated Testing Framework with .NET and Playwright

## Table of Contents
1. [General Architecture](#general-architecture)
2. [Main Components](#main-components)
3. [ValidationTests.cs - Automated Tests](#validationtestscs---automated-tests)
4. [Full Test Lifecycle](#full-test-lifecycle)
5. [Design Patterns Used](#design-patterns-used)
6. [How to Add a New Test?](#how-to-add-a-new-test)
7. [Starting the Project Locally](#starting-the-project-locally)
8. [Running and Viewing Results](#running-and-viewing-results)

---

## General Architecture

```
┌──────────────────────────────────────────────────────────────┐
│ Web frontend application (HTML / JS)                        │
│ The interface under test                                    │
└──────────────────────────────────────────────────────────────┘
───────────────────────────────────────────────────────────────
┌───────────────┬───────────────┬───────────────┬─────────────┐
│ Tests         │ PO            │ Utils         │ TR          │
│ (Tests)       │ (PageObjects) │ (Utilities)   │ (Runner/Rep.)│
└───────┬───────┴───────┬───────┴───────┬───────┘
        │               │               │
        └───────────────PW──────────────┘
                        (Playwright Wrapper)
```

---

## Main Components

- **BrowserConfig.cs:** Browser management and configuration (Playwright, headless mode by default).
- **UserFormPage.cs:** Page Object Model for the user form.
- **ValidationTests.cs:** Form validation tests.
- **UserCreationTests.cs:** User creation tests.

---

## ValidationTests.cs - Automated Tests

**AAA Pattern (Arrange-Act-Assert):**
```
Arrange: Prepare the environment
Act:     Execute the action
Assert:  Verify the result
```

---

## Full Test Lifecycle

```
Start
  │
  ├─ new ValidationTests()              ← Create instance
  │
  ├─ InitializeAsync()                  ← SETUP
  │   ├─ new BrowserConfig()
  │   ├─ _browserConfig.InitializeAsync()
  │   │   ├─ Create Playwright
  │   │   ├─ Launch Chromium (headless)
  │   │   └─ Open http://localhost:3000
  │   └─ _userFormPage = new UserFormPage(_browserConfig.Page)
  │
  ├─ ShouldNotAllowSubmittingEmptyForm() ← TEST
  │   ├─ await _userFormPage.NavigateAsync()
  │   ├─ await _userFormPage.SubmitFormAsync()
  │   ├─ var message = await _userFormPage.GetMessageTextAsync()
  │   └─ Assert.Equal("All required fields must be filled", message)
  │
  ├─ DisposeAsync()                     ← TEARDOWN
  │   ├─ await _browserConfig.CloseAsync()
  │   │   ├─ Close page
  │   │   ├─ Close browser
  │   │   └─ Cleanup Playwright
  │   └─ Dispose resources
  │
  └─ End (PASSED or FAILED)
```

---

## Design Patterns Used

### 1. **IAsyncLifetime** (xUnit)

```csharp
public class ValidationTests : IAsyncLifetime
{
    // IAsyncLifetime means:
    // - InitializeAsync(): called BEFORE
    // - DisposeAsync(): called AFTER
    // - Automatic for each [Fact] or [Theory]
}
```

### 2. **Page Object Model (POM)**

```csharp
// ❌ BAD - Scattered selectors
[Fact]
public async Task MyTest()
{
    await page.Locator("#name").FillAsync("Juan");
    await page.Locator("#email").FillAsync("juan@test.com");
}

// ✅ GOOD - Centralized in POM
[Fact]
public async Task MyTest()
{
    await _userFormPage.FillNameAsync("Juan");
    await _userFormPage.FillEmailAsync("juan@test.com");
}
```

### 3. **Async/Await** (Non-blocking operations)

```csharp
// JavaScript in the browser is asynchronous
// We wait for it to fully execute before continuing

await _userFormPage.SubmitFormAsync();  // Waits for click
var message = await _userFormPage.GetMessageTextAsync(); // Get result
```

---

## How to Add a New Test?

```csharp
[Fact]
public async Task ShouldShowErrorIfPhoneInvalid()
{
    // 1. ARRANGE - Prepare
    await _userFormPage.NavigateAsync();
    
    // 2. ACT - Act
    await _userFormPage.FillCompleteFormAsync(
        name: "Juan",
        email: "juan@test.com",
        age: "25",
        phone: "123",  // Invalid phone (less than 7 digits)
        // ... other fields
    );
    await _userFormPage.SubmitFormAsync();
    
    // 3. ASSERT - Verify
    var message = await _userFormPage.GetMessageTextAsync();
    Assert.Equal("Phone number must have at least 7 digits", message);
}
```

---

## Starting the Project Locally

### 1. Start the Web Page (Frontend)

```bash
cd frontend
npm install
npm start
```

Open your browser at: [http://localhost:3000](http://localhost:3000)

### 2. Run Automated Tests (.NET)

```bash
cd dotnet-tests/POCTests
dotnet restore
dotnet test --logger "trx;LogFileName=test-results.trx"
```

**Browser and Headless Mode Configuration**

- Place your `.env` file in the `dotnet-tests/POCTests` folder.
- The test framework will always load `.env` from this location, regardless of your working directory.
- Example `.env`:
  ```
  BROWSER=Chromium
  HEADLESS=false
  ```
- To run tests in a visible browser window, set `HEADLESS=false`.
- To change the browser, set `BROWSER=Chromium`, `BROWSER=Firefox`, or `BROWSER=WebKit`.

### Recommended Workflow

1. **Terminal 1 - Frontend:**
   ```bash
   cd frontend
   npm start
   ```

2. **Terminal 2 - Backend Tests:**
   ```bash
   cd dotnet-tests/POCTests
   dotnet test
   ```

**Note:** It is important to keep both processes running. The frontend provides the interface and the tests validate it automatically.

---

## Running and Viewing Results

- Test results are shown in the terminal after running `dotnet test`.
- For a results file, `TestResults/test-results.trx` (XML) is generated.
- For a user-friendly visualization, the **.NET Test Explorer** extension in Visual Studio Code is recommended.
- Python and external HTML report tools are not required.

---