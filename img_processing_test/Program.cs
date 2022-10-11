using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using CommandLine;

namespace img_processing_test;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal static class Program
{
    static async Task<int> Main(String[] args)
    {
        
        //return ProcessImage(@"C:\Studia\2022_Winter\Image Processing\Tinkering\TestImages\small.bmp", @"C:\Studia\2022_Winter\Image Processing\Tinkering\result.bmp");
        return await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .MapResult(async opts =>
            {
                //try
                //{
                    return ProcessImage(opts.SourcePath, opts.SavePath);
                //}
                //catch
                // {
                //     Console.WriteLine("Error!");
                //     return -3;
                // }
            }, errs => Task.FromResult(-1));
    }

    private static int ProcessImage(String source, String save)
    {
        if (source == null || save == null) throw new Exception("No path was passed!");

        using Bitmap image = new(source);
        Bitmap bitmap = new(image);

        Console.Write("Number of operation: ");
        int input = Convert.ToInt32(Console.ReadLine());
        
        Console.Write("Number of iterations: ");
        int iterations = Convert.ToInt32(Console.ReadLine());
        
        switch (input)
        {
            case 1:
                bitmap = ImageSmoothing.SmoothImage(bitmap, iterations);
                break;
            case 2:
                bitmap = ImageConvolution.VerticalEdge(bitmap, iterations);
                break;
            case 3:
                bitmap = ImageSmoothing.SmoothImage(bitmap, iterations);
                break;
            case 4:
                bitmap = ImageSmoothing.SmoothImage(bitmap, iterations);
                break;
            case 5:
                bitmap = ImageSmoothing.SmoothImage(bitmap, iterations);
                break;
        }
        
        bitmap.Save(save);
        return 0;
    }
}