using Tesseract;

namespace MaBar.JohnyCalendar.Generator.Utils;

public class ImagePreProcessor(Pix image)
{
    public Pix Process()
    {
        return Binarize(AdaptiveScale(ConvertToGrey(image)));
    }

    private static Pix ConvertToGrey(Pix pix)
    {
        return pix.ConvertRGBToGray();
    }

    private static Pix AdaptiveScale(Pix img)
    {
        return new ImageScaleProcessor(img).Process();
    }

    private static Pix Binarize(Pix img)
    {
        var binarized = img.BinarizeOtsuAdaptiveThreshold(32, 32, 1, 1, 0.1f);
        return binarized;
    }
}
