// Запись для хранения данных, необходимых для монтирования тома VeraCrypt
internal record MountInfo(char Drive, string VeraCryptPath, string DataPath, string KeyPath);