using Tesseract;

namespace MaBar.JohnyCalendar.Generator.Utils;

public class TesseractTextExtractor(
    string tessdataPath = "/app/tessdata",
    string languages = "eng+pol",
    PageSegMode pageSegMode = PageSegMode.SingleColumn)
{
    public string ExtractText(Pix img)
    {
        using var engine = new TesseractEngine(tessdataPath, languages, EngineMode.Default);
        engine.SetVariable("tessedit_char_whitelist", "0123456789,|:ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");

        using var page = engine.Process(img, pageSegMode);
        return page.GetText();
    }
}
