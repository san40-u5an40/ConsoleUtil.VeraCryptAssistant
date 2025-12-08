internal static partial class Startup
{
    // Получение данных для монтирования из конфигурации:
    // 
    // Если указанный в конфигурации диск уже существует, то возвращается соответствующее уведомление
    // После проверки информации из конфигурации она возвращается
    // Иначе возвращается соответствующее уведомление
    private static (MountInfo? MountInfo, string? Error) GetMountInfo(IConfigurationRoot config)
    {
        var section = config.GetSection("MountInfo");

        bool isSuccessDriveParse = char.TryParse(section["Drive"], out char drive);
        bool isVeraCryptExists = File.Exists(section["VeraCryptPath"]);
        bool isDataExists = File.Exists(section["DataPath"]);
        bool isKeyExists = File.Exists(section["KeyPath"]);

        if (isSuccessDriveParse &&
            DriveInfo
                .GetDrives()
                .Select(p => p.Name)
                .Contains(drive.ToString().ToUpper() + @":\"))
        {
            return (null, "Указанный раздел для монтирования уже существует!");
        }

        if (isSuccessDriveParse && isVeraCryptExists && isDataExists && isKeyExists)
            return (new MountInfo(drive, section["VeraCryptPath"]!, section["DataPath"]!, section["KeyPath"]!), null);
        else
            return (null, "Конфигурационные данные не были найдены!");
    }
}