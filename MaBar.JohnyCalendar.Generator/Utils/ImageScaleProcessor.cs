using Tesseract;

namespace MaBar.JohnyCalendar.Generator.Utils;

internal class ImageScaleProcessor(Pix image)
{
    public Pix Process()
    {
        var scale = GetOptimalScale(image);
        return image.Scale(scale, scale);
    }

    private static float GetOptimalScale(Pix img)
    {
        var sharpness = CalculateSharpness(img);
        const float threshold = 2500.0f; // error trials chosen number
        var scale = Math.Max(0.5f, threshold / sharpness);
        return scale;
    }

    private static float CalculateSharpness(Pix img)
    {
        var width = img.Width;
        var height = img.Height;
        var totalPixels = width * height;

        var dataPtr = img.GetData().Data;

        float sum = 0;
        float sumSquared = 0;

        unsafe
        {
            var pixelData = (byte*)dataPtr;

            for (var i = 0; i < totalPixels; i++)
            {
                int pixelValue = pixelData[i];
                sum += pixelValue;
                sumSquared += pixelValue * pixelValue;
            }
        }

        var mean = sum / totalPixels;
        var variance = sumSquared / totalPixels - mean * mean;

        return variance;
    }
}
