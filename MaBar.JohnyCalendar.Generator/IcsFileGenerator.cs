using Ical.Net.Serialization;
using MaBar.JohnyCalendar.Generator.Utils;
using Tesseract;

namespace MaBar.JohnyCalendar.Generator;

internal class IcsFileGenerator(
    string imagePath,
    string icsFilePath,
    int year,
    int month,
    string eventName,
    string description,
    List<TimeSpan> alerts)
{
    public void CreateIcsFile()
    {
        using var img = Pix.LoadFromFile(imagePath);
        var preProcessedImage = new ImagePreProcessor(img).Process();
        var extractedText = new TesseractTextExtractor().ExtractText(preProcessedImage);
        var calendar = new IcsEventParser().ToIcsCalendar(extractedText, year, month, eventName, description, alerts);

        var directory = Path.GetDirectoryName(icsFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        var serializer = new CalendarSerializer();
        var serializedCalendar = serializer.SerializeToString(calendar);

        File.WriteAllText(icsFilePath, serializedCalendar);
    }
}
