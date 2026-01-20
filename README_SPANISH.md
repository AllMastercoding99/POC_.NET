# POC - Framework de Pruebas Automatizadas con .NET y Playwright

## Índice
1. [Arquitectura General](#arquitectura-general)
2. [Componentes Principales](#componentes-principales)
3. [ValidationTests.cs - Pruebas Automatizadas](#validationtestscs---pruebas-automatizadas)
4. [Ciclo de Vida Completo de una Prueba](#ciclo-de-vida-completo-de-una-prueba)
5. [Patrones Utilizados](#patrones-utilizados)
6. [¿Cómo agregar una nueva prueba?](#cómo-agregar-una-nueva-prueba)
7. [Iniciando el Proyecto Localmente](#iniciando-el-proyecto-localmente)
8. [Ejecución y Visualización de Resultados](#ejecución-y-visualización-de-resultados)

---

## Arquitectura General

```
┌──────────────────────────────────────────────────────────────┐
│ Aplicación web frontend (HTML / JS)                         │
│ La interfaz siendo probada                                  │
└──────────────────────────────────────────────────────────────┘
───────────────────────────────────────────────────────────────
┌───────────────┬───────────────┬───────────────┬─────────────┐
│ Pruebas       │ PO            │ Utils         │ TR          │
│ (Tests)       │ (PageObjects) │ (Utilidades)  │ (Runner/Rep.)│
└───────┬───────┴───────┬───────┴───────┬───────┘
        │               │               │
        └───────────────PW──────────────┘
                        (Playwright Wrapper)
```

---

## Componentes Principales

- **BrowserConfig.cs:** Gestión y configuración del navegador (Playwright, modo headless por defecto).
- **UserFormPage.cs:** Page Object Model para el formulario de usuario.
- **ValidationTests.cs:** Pruebas de validación de formulario.
- **UserCreationTests.cs:** Pruebas de creación de usuario.

---

## ValidationTests.cs - Pruebas Automatizadas

**Patrón AAA (Arrange-Act-Assert):**
```
Arrange: Preparar el ambiente
Act:     Ejecutar la acción
Assert:  Verificar el resultado
```

---

## Ciclo de Vida Completo de una Prueba

```
Inicio
  │
  ├─ new ValidationTests()              ← Crear instancia
  │
  ├─ InitializeAsync()                  ← SETUP
  │   ├─ new BrowserConfig()
  │   ├─ _browserConfig.InitializeAsync()
  │   │   ├─ Crear Playwright
  │   │   ├─ Lanzar Chromium (headless)
  │   │   └─ Abrir http://localhost:3000
  │   └─ _userFormPage = new UserFormPage(_browserConfig.Page)
  │
  ├─ ShouldNotAllowSubmittingEmptyForm() ← TEST
  │   ├─ await _userFormPage.NavigateAsync()
  │   ├─ await _userFormPage.SubmitFormAsync()
  │   ├─ var message = await _userFormPage.GetMessageTextAsync()
  │   └─ Assert.Equal("Todos los campos...", message)
  │
  ├─ DisposeAsync()                     ← TEARDOWN
  │   ├─ await _browserConfig.CloseAsync()
  │   │   ├─ Cerrar página
  │   │   ├─ Cerrar navegador
  │   │   └─ Limpiar Playwright
  │   └─ Dispose recursos
  │
  └─ Fin (PASSED o FAILED)
```

---

## Patrones Utilizados

### 1. **IAsyncLifetime** (xUnit)

```csharp
public class ValidationTests : IAsyncLifetime
{
    // IAsyncLifetime significa:
    // - InitializeAsync(): se llama ANTES
    // - DisposeAsync(): se llama DESPUÉS
    // - Automático para cada [Fact] o [Theory]
}
```

### 2. **Page Object Model (POM)**

```csharp
// ❌ MAL - Selectores esparcidos
[Fact]
public async Task MyTest()
{
    await page.Locator("#name").FillAsync("Juan");
    await page.Locator("#email").FillAsync("juan@test.com");
}

// ✅ BIEN - Centralizado en POM
[Fact]
public async Task MyTest()
{
    await _userFormPage.FillNameAsync("Juan");
    await _userFormPage.FillEmailAsync("juan@test.com");
}
```

### 3. **Async/Await** (Operaciones no-bloqueantes)

```csharp
// JavaScript en el navegador es asincrónico
// Esperamos a que se ejecute completamente antes de continuar

await _userFormPage.SubmitFormAsync();  // Espera el click
await Task.Delay(500);                  // Espera que JS valide
var message = await _userFormPage.GetMessageTextAsync(); // Obtén resultado
```

---

## ¿Cómo agregar una nueva prueba?

```csharp
[Fact]
public async Task ShouldShowErrorIfPhoneInvalid()
{
    // 1. ARRANGE - Preparar
    await _userFormPage.NavigateAsync();
    
    // 2. ACT - Actuar
    await _userFormPage.FillCompleteFormAsync(
        name: "Juan",
        email: "juan@test.com",
        age: "25",
        phone: "123",  // Teléfono inválido (menos de 7 dígitos)
        // ... otros campos
    );
    await _userFormPage.SubmitFormAsync();
    
    // 3. ASSERT - Verificar
    var message = await _userFormPage.GetMessageTextAsync();
    Assert.Equal("El teléfono debe contener al menos 7 dígitos", message);
}
```

---

## Iniciando el Proyecto Localmente

### 1. Iniciar la Página Web (Frontend)

```bash
cd frontend
npm install
npm start
```

Abre tu navegador en: [http://localhost:3000](http://localhost:3000)

### 2. Ejecutar las Pruebas Automatizadas (.NET)

```bash
cd dotnet-tests/POCTests
dotnet restore
dotnet test --logger "trx;LogFileName=test-results.trx"
```

---

### Flujo Recomendado

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

**Nota:** Es importante mantener ambos procesos ejecutándose. El frontend proporciona la interfaz y los tests la validan automáticamente.

---

## Ejecución y Visualización de Resultados

- Los resultados de las pruebas se muestran en la terminal tras ejecutar `dotnet test`.
- Para un archivo de resultados, se genera `TestResults/test-results.trx` (XML).
- Para una visualización amigable, se recomienda la extensión **.NET Test Explorer** en Visual Studio Code.
- No se requiere Python ni herramientas de reporte HTML externas.

---