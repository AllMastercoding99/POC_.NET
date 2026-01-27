# TEST_GUIDE.md

## 1. Test Structure

- Tests are located in:  
  `dotnet-tests/POCTests/Tests/`
- Page objects and helpers:  
  `dotnet-tests/POCTests/Pages/`  
  `dotnet-tests/POCTests/Utils/`

## 2. Environment Configuration

- The `.env` file must be in `dotnet-tests/POCTests/`
- Example:
  ```
  BROWSER=Chromium
  HEADLESS=false
  BASE_URL=http://localhost:3000
  ```

## 3. How to Create a New Test

1. **Create a test file** in `dotnet-tests/POCTests/Tests/`  
   Example: `UserFormEmailValidationTests.cs`

2. **Inherit from the base class**  
   ```csharp
   public class UserFormEmailValidationTests : TestBase
   {
       // Test methods here
   }
   ```

3. **Add test methods using `[Fact]` or `[Theory]`**  
   Example:
   ```csharp
   [Fact]
   public async Task Should_Show_Error_When_Email_Is_Invalid()
   {
       await _userFormPage.NavigateAsync();
       await _userFormPage.FillEmailAsync("invalid-email");
       await _userFormPage.SubmitFormAsync();
       var message = await _userFormPage.GetMessageTextAsync();
       Assert.Equal("Invalid email address", message);
   }
   ```

4. **Use `UserFormPage` methods to interact with the form.**

## 4. Running the Tests

```bash
cd dotnet-tests/POCTests
dotnet test
```

## 5. Best Practices

- Use descriptive names for test files and methods.
- Document each test with brief comments.
- Use `[Theory]` and `MemberData` for varied test data.
- Keep code clean and reuse helpers.

---

## Prompting with GitHub Copilot

This section is dedicated to using **GitHub Copilot** to accelerate and facilitate the creation of new tests in this framework.

### Example Prompts for GitHub Copilot

- **Field validation:**
  > Copilot, generate an xUnit test using the `TestBase` base class that verifies the user form shows an error message when the email field is invalid. Use the methods from `UserFormPage`.

- **Successful creation test:**
  > Copilot, create an xUnit test that fills all fields in the user form with valid data and verifies that the message "Usuario creado correctamente" is shown. Use the project's infrastructure and helpers.

- **Parameterized test with Theory:**
  > Copilot, generate a `[Theory]` xUnit test for the user form, using `MemberData` to test several invalid data scenarios and verify the corresponding validation messages.

- **Extending the user page:**
  > Copilot, add a method in `UserFormPage` to select an additional language and use it in a new validation test.

- **Integration test with BASE_URL:**
  > Copilot, create a test that navigates to the main page using the URL defined in `.env` and verifies that the user form is visible.

### Tips for Using Copilot in This Project

- Always specify the use of the `TestBase` base class and the methods from `UserFormPage` so Copilot generates code compatible with the project architecture.
- If you need helpers or utilities, ask Copilot to place them in the corresponding folder (`Utils` or `Pages`).
- You can ask Copilot to adapt existing examples to new scenarios by simply changing the data or expected message.

---

**If you have a useful prompt, add it here to share with the team.**