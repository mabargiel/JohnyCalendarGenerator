using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace MaBar.JohnyCalendar.Generator.Utils;

public class IcsEventParser
{
    public Calendar ToIcsCalendar(string text, int year, int month, string eventName, string description, List<TimeSpan> alerts)
    {
        var events = GetEvents(text, year, month, eventName, description, alerts);

        var calendar = new Calendar();
        foreach (var calendarEvent in events)
        {
            calendar.Events.Add(calendarEvent);
        }

        return calendar;
    }

    private CalendarEvent[] GetEvents(string text, int year, int month, string eventName, string description, List<TimeSpan> alerts)
    {
        var rows = text
            .Replace("\n\n", "\n")
            .Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        if (rows.Length >= 6)
        {
            rows = rows.Skip(1).ToArray(); // Remove headers if needed
        }

        return (from row in rows
                let content = row.Split(' ')
                where content.Length == 2
                let timeParts = content[0].Split(":")
                let days = content[1].Split(",").Select(int.Parse)
                from dayNumber in days
                let hour = int.Parse(timeParts[0])
                let minute = int.Parse(timeParts[1])
                select new CalendarEvent
                {
                    Summary = eventName,
                    Start = new CalDateTime(year, month, dayNumber, hour, minute, 0),
                    End = new CalDateTime(year, month, dayNumber, hour + 1, minute, 0),
                    Location = "Rusznikarska 14, 31-261 Kraków, Poland",
                    Description = description,
                    Uid = $"{year}-{month}-{dayNumber}-{hour}-{minute}"
                }
                into calendarEvent
                select AddAlerts(calendarEvent, alerts)
            ).ToArray();
    }

    private static CalendarEvent AddAlerts(CalendarEvent calendarEvent, List<TimeSpan> alerts)
    {
        foreach (var alert in alerts)
        {
            calendarEvent.Alarms.Add(new Alarm
            {
                Trigger = new Trigger(-alert),
                Action = AlarmAction.Display,
                Description = alert.TotalMinutes >= 60
                    ? $"{alert.TotalHours:F0} hour{(alert.TotalHours > 1 ? "s" : "")} before"
                    : $"{alert.TotalMinutes:F0} minutes before"
            });
        }

        return calendarEvent;
    }
}
