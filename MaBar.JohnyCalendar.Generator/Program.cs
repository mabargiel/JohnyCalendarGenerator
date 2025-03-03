namespace MaBar.JohnyCalendar.Generator;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var imagePath = GetArgument(args, "--imagePath") ??
                            Environment.GetEnvironmentVariable("IMAGE_PATH");

            var icsFilePath = GetArgument(args, "--output") ??
                              Environment.GetEnvironmentVariable("ICS_PATH");

            var year = int.Parse(GetArgument(args, "--year") ??
                                 Environment.GetEnvironmentVariable("YEAR")
                                 ?? throw new ArgumentException("Year is required."));

            var month = int.Parse(GetArgument(args, "--month") ??
                                  Environment.GetEnvironmentVariable("MONTH")
                                  ?? throw new ArgumentException("Month is required."));

            var eventName = GetOptionalArgument(args, "--eventName") ??
                            Environment.GetEnvironmentVariable("EVENT_NAME") ??
                            "Trening";

            var description = GetOptionalArgument(args, "--description") ??
                              Environment.GetEnvironmentVariable("DESCRIPTION") ??
                              "Trening at Rusznikarska 14, 31-261 Kraków";

            var alertsArg = GetOptionalArgument(args, "--alerts") ??
                            Environment.GetEnvironmentVariable("ALERTS");
            var alerts = alertsArg != null
                ? ParseAlerts(alertsArg)
                : [TimeSpan.FromHours(1), TimeSpan.FromMinutes(30)]; // Default alerts

            var generator = new IcsFileGenerator(imagePath, icsFilePath, year, month, eventName, description, alerts);
            generator.CreateIcsFile();

            Console.WriteLine("ICS file generated successfully at: " + icsFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
        }
    }

    private static string? GetArgument(string[] args, string key)
    {
        var index = Array.IndexOf(args, key);
        if (index == -1 || index + 1 >= args.Length)
            return null;
        return args[index + 1];
    }

    private static string? GetOptionalArgument(string[] args, string key)
    {
        var index = Array.IndexOf(args, key);
        if (index == -1 || index + 1 >= args.Length)
            return null;
        return args[index + 1];
    }

    private static List<TimeSpan> ParseAlerts(string alertStr)
    {
        return alertStr.Split(',')
            .Select(alert =>
                alert.EndsWith('h') ? TimeSpan.FromHours(int.Parse(alert[..^1])) :
                alert.EndsWith('m') ? TimeSpan.FromMinutes(int.Parse(alert[..^1])) :
                throw new ArgumentException($"Invalid alert format: {alert}")
            ).ToList();
    }
}
