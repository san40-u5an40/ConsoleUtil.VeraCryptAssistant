internal static partial class Startup
{
    // В данном методе используется контроллер ввода пароля из фреймворка san40_u5an40.ConsoleDisplayFramework
    // Подробнее: https://www.nuget.org/packages/san40_u5an40.ConsoleDisplayFramework#readme-body-tab
    // Данный метод возвращает введённый пользователем пароль
    private static async Task<string> PasswordInputAsync(string inputMessage)
    {
        var input = new ControllerReadLine(inputMessage);
        var printer = new Printer<string>(new UpperDisplay(), input, false);
        var password = await printer.ShowAsync();
        Console.Clear();
        return password;
    }
}