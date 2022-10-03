using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using CommandLine;

namespace img_processing_test;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal class Program
{
    static async Task<int> Main(String[] args)
    {
        return await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .MapResult(async opts =>
            {
                try
                {
                    return ProcessImage(opts.SourcePath, opts.SavePath);
                }
                catch
                {
                    Console.WriteLine("Error!");
                    return -3;
                }
            }, errs => Task.FromResult(-1));
    }

    private static int ProcessImage(String source, String save)
    {
        if (source == null || save == null) throw new Exception("No path was passed!");

        using (var image = new Bitmap(source))
        {
            Bitmap bitmap = new Bitmap(image);
            bitmap = SmoothImage(bitmap, 10);
            bitmap.Save(save);
        }
        return 0;
    }

    private static Bitmap SmoothImage(Bitmap bitmap, int iterations)
    {
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            for (int i = 1; i < bitmap.Height - 1; i++)
            {
                for (int j = 1; j < bitmap.Height - 1; j++)
                {
                    bitmap.SetPixel(i, j, GetAverageColorFromNeighbouringPixels(bitmap,i,j));
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
}