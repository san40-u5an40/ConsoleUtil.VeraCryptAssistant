internal static partial class Startup
{
    // Метод запуска программ из автозагрузки, указанной в конфигурационном файле:
    // 
    // Получение перечня программ из конфигурационного файла
    // И их перебор с запуском
    // Если программа не будет найдена, или если при её запуске возникнет ошибка, появится соответствующее уведомление
    private static void RunProcesses(IConfigurationRoot config)
    {
        var section = config.GetSection("AppsStartup");

        var processes = section.GetChildren();
        foreach (var process in processes)
        {
            if (!File.Exists(process.Value))
                Console.WriteLine($"Файл \"{process.Value}\" не найден!");
            else
                try
                {
                    Process.Start(process.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("При запуске \"" + process.Value + "\" произошла ошибка:\n" + ex.Message);
                }
        }
    }
}