internal static partial class Exit
{
    // Получение из конфигурации данных для размонтирования тома
    private static (char? Drive, string? VeraCryptPath, string? Error) GetUnmountInfo(IConfigurationRoot config)
    {
        var unmountSectionConfig = config.GetSection("UnmountInfo");

        bool isSuccessDriveParse = char.TryParse(unmountSectionConfig["Drive"], out char drive);
        bool isVeraCryptExists = File.Exists(unmountSectionConfig["VeraCryptPath"]);

        if (isSuccessDriveParse && isVeraCryptExists)
            return (drive, unmountSectionConfig["VeraCryptPath"], null);
        else
            return (null, null, "Конфигурационные данные не были найдены!");
    }
}