internal static partial class Startup
{
    // Основная логика программы:
    // 
    // Получение конфигурации, если не удачно, то вывод ошибки
    // Получение информации для монтирования, если не удачно, то вывод ошибки
    // Затем ввод пароля пользователя:
    // - Если удачно, то программа идёт дальше, и запускает список автозагрузки из конфигурации
    // - Если неверный пароль, то повторный ввод
    // - Если неизвестная ошибка, то соответствующее уведомление
    internal static async Task RunAsync()
    {
        var getConfigurationResult = GetConfiguration();
        if (getConfigurationResult.Error != null)
        {
            Console.WriteLine(getConfigurationResult.Error);
            return;
        }

        var getMountInfoResult = GetMountInfo(getConfigurationResult.Configuration!);
        if (getMountInfoResult.Error != null)
        {
            Console.WriteLine(getMountInfoResult.Error);
            return;
        }

        while (true)
        {
            string password = await PasswordInputAsync("Введите пароль для монтирования тома (данные отображаться не будут, для выхода введите значение \"q\"): ");
            if (password == "q" || password == "Q" || string.IsNullOrEmpty(password))
                return;

            Console.Write("Идёт монтирование...");
            int mountResult = await MountDriveAsync(getMountInfoResult.MountInfo!, password);
            Console.CleanLine();

            if(mountResult == 0)
            {
                Console.WriteLine("Том успешно смонтирован!");
                break;
            }
            else if(mountResult == 1)
            {
                Console.WriteLine("Неверный пароль, повторите попытку!");
                await Task.Delay(1000);
            }
            else
            {
                Console.WriteLine("Непредвиденная ошибка выполнения.");
                return;
            }
        }

        RunProcesses(getConfigurationResult.Configuration!);
    }
}