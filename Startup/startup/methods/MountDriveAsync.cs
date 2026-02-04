internal static partial class Startup
{
    // Монтирование тома:
    // 
    // Создаётся объект с информацией о запускаемом процессе
    // Который затем соответственно и запускается с ожиданием кода завершения
    // Из метода возвращается код завершения процесса
    private static async Task<int> MountDriveAsync(MountInfo mountInfo, string password)
    {
        ProcessStartInfo processInfo = new()
        {
            FileName = mountInfo.VeraCryptPath,
            Arguments = $"/q /s /l {mountInfo.Drive} /v {mountInfo.DataPath} /k {mountInfo.KeyPath} /p {password}",
            CreateNoWindow = true,
            UseShellExecute = true,
        };

        var process = Process.Start(processInfo);
        await process!.WaitForExitAsync();
        return process!.ExitCode;
    }
}