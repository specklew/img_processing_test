using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace img_processing_test;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal static class ImageSmoothing
{
    public static Bitmap SmoothImage(Bitmap bitmap, int iterations)
    {
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            for (int i = 1; i < bitmap.Width - 1; i++)
            {
                for (int j = 1; j < bitmap.Height - 1; j++)
                {
                    bitmap.SetPixel(i, j, GetMedianColorFromNeighbouringPixels(bitmap,i,j));
                }
            }
        }

        return bitmap;
    }

    private static Color GetAverageColorFromNeighbouringPixels(Bitmap bitmap, int x, int y)
    {
        int a = 0, r = 0, g = 0, b = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y) continue;
                Color color = bitmap.GetPixel(i, j);
                a += color.A;
                r += color.R;
                g += color.G;
                b += color.B;
            }
        }

        a /= 8;
        r /= 8;
        g /= 8;
        b /= 8;
        
        return Color.FromArgb(a, r, g, b);
    }

    private static Color GetMedianColorFromNeighbouringPixels(Bitmap bitmap, int x, int y)
    {
        List<Color> colors = new();
        
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y) continue;
                Color color = bitmap.GetPixel(i, j);
                colors.Add(color);
            }
        }

        colors.Sort(new SortColorsByIntensity());

        return colors[colors.Count / 2];
    }
}