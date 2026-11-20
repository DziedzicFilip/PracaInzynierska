namespace Api.Data;

public static class EnvironmentFileLoader
{
    public static void Load(string applicationDirectory)
    {
        var envFile = FindEnvironmentFile(applicationDirectory);

        if (envFile is null)
        {
            return;
        }

        foreach (var rawLine in File.ReadAllLines(envFile))
        {
            var line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith('#'))
            {
                continue;
            }

            var separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = line[..separatorIndex].Trim();
            var value = line[(separatorIndex + 1)..].Trim();

            if (value.Length >= 2 &&
                ((value.StartsWith('"') && value.EndsWith('"')) ||
                 (value.StartsWith('\'') && value.EndsWith('\''))))
            {
                value = value[1..^1];
            }

            if (!string.IsNullOrWhiteSpace(key))
            {
                if (Environment.GetEnvironmentVariable(key) is null)
                {
                    Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);
                }
            }
        }
    }

    private static string? FindEnvironmentFile(string applicationDirectory)
    {
        var startingDirectories = new[]
        {
            applicationDirectory,
            Directory.GetCurrentDirectory()
        };

        foreach (var startingDirectory in startingDirectories.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var directory = new DirectoryInfo(startingDirectory);
            while (directory is not null)
            {
                var candidate = Path.Combine(directory.FullName, ".env");
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                directory = directory.Parent;
            }
        }

        return null;
    }
}
