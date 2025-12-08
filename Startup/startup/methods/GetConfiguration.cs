internal static partial class Startup
{
    // Получение конфигурационных данных из файла
    private static (IConfigurationRoot? Configuration, string? Error) GetConfiguration()
    {
        string configDirectory = Path.Combine(AppContext.BaseDirectory, "config");
        string configFile = "appsettings_startup.json";

        if (!File.Exists(Path.Combine(configDirectory, configFile)))
            return (null, "Файл конфигурации не был найден!");

        var config = new ConfigurationBuilder()
            .SetBasePath(configDirectory)
            .AddJsonFile(configFile)
            .Build();

        return (config, null);
    }
}