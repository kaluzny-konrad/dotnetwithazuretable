namespace dotnetwithazuretable.Console;

public static class DotEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(new[] { '=' }, 2, 
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) continue;

            var key = parts[0];
            var value = parts[1];
            if (value.Length < 2) continue;
            value = value.Remove(value.Length - 1).Remove(0, 1);

            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
