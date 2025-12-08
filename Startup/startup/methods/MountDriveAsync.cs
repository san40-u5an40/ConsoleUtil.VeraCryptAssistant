internal static partial class Startup
{
    // Монтирование тома:
    // 
    // Создаётся объект с информацией о запускаемом процессе
    // Который затем соответственно и запускается с ожиданием кода завершения
    // Из метода возвращается код завершения процесса
    private static async Task<int> MountDriveAsync(MountInfo mountInfo, string password)
    {
        var processInfo = new ProcessStartInfo()
        {
            FileName = mountInfo.VeraCryptPath,
            Arguments = $"/l {mountInfo.Drive} /v {mountInfo.DataPath} /k {mountInfo.KeyPath} /p {password} /q /s",
            CreateNoWindow = true
        };

        var process = Process.Start(processInfo);
        await process!.WaitForExitAsync();
        return process!.ExitCode;
    }
}