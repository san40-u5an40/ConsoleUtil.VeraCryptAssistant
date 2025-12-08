internal static partial class Exit
{
    // Основная логика программы:
    // 
    // Получение конфигурации, если не удачно, то вывод ошибки
    // Получение информации для размонтирования, если не удачно, то вывод ошибки
    // Завершение перечня программ из конфигурационного файла (если они работают на базе смонтированного тома)
    // И размонтирование самого тома, если не удачно, то вывод ошибки
    internal static void Run()
    {
        var getConfigurationResult = GetConfiguration();
        if (getConfigurationResult.Error != null)
        {
            Console.WriteLine(getConfigurationResult.Error);
            return;
        }

        var getUnmountInfoResult = GetUnmountInfo(getConfigurationResult.Configuration!);
        if (getUnmountInfoResult.Error != null)
        {
            Console.WriteLine(getUnmountInfoResult.Error);
            return;
        }

        AppsExit(getConfigurationResult.Configuration!);

        int exitCodeUnmount = Unmount((char)getUnmountInfoResult.Drive!, getUnmountInfoResult.VeraCryptPath!);
        if (exitCodeUnmount == 0)
            Console.WriteLine("Том успешно размонтирован!");
        else
        {
            Console.WriteLine("Ошибка размонтирования!");
            return;
        }
    }
}