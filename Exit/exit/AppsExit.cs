internal static partial class Exit
{
    // Завершение программ из конфигурационного перечня:
    // 
    // Получение перечня программ из конфигурационного файла с его преобразование для более удобной работы
    // Получение перечня запущенных процессов с его преобразованием для более удобной работы
    // Пересечение полученных списков по имени процесса
    // И перебор получившегося перечня в цикле с завершение получившихся процессов
    // Если возникают ошибки, то вывод соответствующего уведомления
    private static void AppsExit(IConfigurationRoot config)
    {
        var appList = config
            .GetSection("AppsExit")
            .GetChildren()
            .Select(p => p.Value);

        var processList = Process
            .GetProcesses()
            .Select(p => new
            {
                Name = p.ProcessName,
                Process = p
            });

        var processToExitList = processList
            .IntersectBy(appList, p => p.Name);

        foreach (var processToExit in processToExitList)
            try
            {
                processToExit.Process.Kill(true);
                processToExit.Process.WaitForExit();
                Console.WriteLine("Процесс \"" + processToExit.Name + "\" успешно завершён!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("При завершении процесса \"" + processToExit.Name + "\" произошла ошибка:\n" + ex.Message);
            }
    }
}