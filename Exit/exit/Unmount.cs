internal static partial class Exit
{
    // Размонтирование тома
    private static int Unmount(char drive, string veraCryptPath)
    {
        var exitProcessInfo = new ProcessStartInfo()
        {
            FileName = veraCryptPath,
            Arguments = $"/d {drive} /f /q /s",
            CreateNoWindow = true
        };

        var process = Process.Start(exitProcessInfo);
        process!.WaitForExit();
        return process.ExitCode;
    }
}