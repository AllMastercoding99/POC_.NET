using Microsoft.Playwright;

namespace POCTests.Extensions;

/// <summary>
/// Métodos de extensión para ILocator que centralizan delays y acciones comunes.
/// Permite reutilizar la lógica de interacción con elementos en cualquier Page Object Model.
/// 
/// Ventajas:
/// - DRY: Centraliza la lógica de delays en un único lugar
/// - Reutilizable: Disponible en cualquier clase que importe este namespace
/// - Configurable: Permite pasar un delay personalizado si es necesario
/// - Limpio: Simplifica el código de los Page Objects
/// </summary>
public static class LocatorExtensions
{
    private const int DefaultDelay = 500;

    /// <summary>
    /// Llena un campo de texto con delay.
    /// Útil para simular acciones humanas y permitir que el navegador actualice el DOM.
    /// </summary>
    /// <param name="locator">El locator del campo a llenar</param>
    /// <param name="value">El valor a escribir</param>
    /// <param name="delay">Tiempo en milisegundos a esperar (por defecto 500ms)</param>
    public static async Task FillWithDelayAsync(this ILocator locator, string value, int delay = DefaultDelay)
    {
        await locator.FillAsync(value);
        await Task.Delay(delay);
    }

    /// <summary>
    /// Selecciona una opción en un dropdown con delay.
    /// Útil para simular acciones humanas y permitir que JavaScript procese el cambio.
    /// </summary>
    /// <param name="locator">El locator del select</param>
    /// <param name="value">El valor de la opción a seleccionar</param>
    /// <param name="delay">Tiempo en milisegundos a esperar (por defecto 500ms)</param>
    public static async Task SelectWithDelayAsync(this ILocator locator, string value, int delay = DefaultDelay)
    {
        await locator.SelectOptionAsync(value);
        await Task.Delay(delay);
    }

    /// <summary>
    /// Marca un checkbox o radio button con delay.
    /// Útil para simular acciones humanas y permitir que JavaScript procese el evento.
    /// </summary>
    /// <param name="locator">El locator del checkbox o radio button</param>
    /// <param name="delay">Tiempo en milisegundos a esperar (por defecto 500ms)</param>
    public static async Task CheckWithDelayAsync(this ILocator locator, int delay = DefaultDelay)
    {
        await locator.CheckAsync();
        await Task.Delay(delay);
    }

    /// <summary>
    /// Hace click en un elemento con delay.
    /// Útil para simular acciones humanas y permitir que el navegador procese el evento.
    /// </summary>
    /// <param name="locator">El locator del elemento a clickear</param>
    /// <param name="delay">Tiempo en milisegundos a esperar (por defecto 500ms)</param>
    public static async Task ClickWithDelayAsync(this ILocator locator, int delay = DefaultDelay)
    {
        await locator.ClickAsync();
        await Task.Delay(delay);
    }
}
