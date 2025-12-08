internal static partial class Exit
{
    // Получение конфигурационных данных
    private static (IConfigurationRoot? Configuration, string? Error) GetConfiguration()
    {
        string configDirectory = Path.Combine(AppContext.BaseDirectory, "config");
        string configFile = "appsettings_exit.json";

        if (!File.Exists(Path.Combine(configDirectory, configFile)))
            return (null, "Файл конфигурации не был найден!");

        var config = new ConfigurationBuilder()
            .SetBasePath(configDirectory)
            .AddJsonFile(configFile)
            .Build();

        return (config, null);
    }
}