using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace img_processing_test;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal static class ImageConvolution
{
    private static readonly int[,] VerticalEdgeKernel =
    {
        { 1, 0,-1}, 
        { 1, 0,-1}, 
        { 1, 0,-1}
    };
    
    private static readonly int[,] Outline =
    {
        {-1,-1,-1}, 
        {-1, 8,-1}, 
        {-1,-1,-1}
    };
    
    private static readonly int[,] Sharpen =
    {
        { 0,-1, 0}, 
        {-1, 5,-1}, 
        { 0,-1, 0}
    };
    
    private static readonly int[,] Kernel =
    {
        { 1, 0, 0}, 
        { 0, 0, 0}, 
        { 0, 0,-1}
    };
    
    public static Bitmap VerticalEdge(Bitmap bitmap, int iterations)
    {
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap.SetPixel(i, j, GetColorByMultiplyingKernel(bitmap, Kernel, i, j));
                }
            }
        }
        
        return bitmap;
    }

    private static Bitmap ApplyKernelToPosition(Bitmap bitmap, int[,] kernel, int x, int y)
    {
        int offset = kernel.GetLength(0) / 2;

        for (int i = 0; i < kernel.GetLength(0); i++)
        {
            for (int j = 0; j < kernel.GetLength(1); j++)
            {
                int m = x - offset + i;
                int n = y - offset + j;

                if (m < 0 || m >= bitmap.Width || n < 0 || n >= bitmap.Height) continue;

                bitmap.SetPixel(m, n, MultiplyColorRgbValues(bitmap.GetPixel(m, n), kernel[i,j]));
            }
        }

        return bitmap;
    }

    private static Color GetColorByMultiplyingKernel(Bitmap bitmap, int[,] kernel, int x, int y)
    {
        int offset = kernel.GetLength(0) / 2;
        
        RGB initial = RGB.ToRGB(bitmap.GetPixel(x, y));
        
        for (int i = 0; i < kernel.GetLength(0); i++)
        {
            for (int j = 0; j < kernel.GetLength(1); j++)
            {
                int m = x - offset + i;
                int n = y - offset + j;

                if (m < 0 || m >= bitmap.Width || n < 0 || n >= bitmap.Height) continue;

                RGB current = RGB.ToRGB(bitmap.GetPixel(m,n));
                current *= kernel[i, j];
                initial += current;
            }
        }

        return initial.ToColor();
    }

    private static Color MultiplyColorRgbValues(Color color, int scalar)
    {
        int r = color.R * scalar;
        int b = color.G * scalar;
        int g = color.B * scalar;

        r = Math.Clamp(r, 0, 255);
        g = Math.Clamp(g, 0, 255);
        b = Math.Clamp(b, 0, 255);
        
        return Color.FromArgb(255, r, g, b);
    }
}