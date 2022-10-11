using System.Drawing;

namespace img_processing_test;

internal struct RGB
{
    private int R { get; }
    private int G {get; }
    private int B {get; }

    public RGB(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
    }
    public static RGB operator *(RGB rgb, int scalar)
    {
        return new RGB(rgb.R * scalar, rgb.G * scalar, rgb.B * scalar);
    }
    
    public static RGB operator *(RGB rgb, RGB right)
    {
        return new RGB(rgb.R * right.R, rgb.G * right.G, rgb.B * right.B);
    }
    
    public static RGB operator +(RGB left, RGB right)
    {
        return new RGB(left.R + right.R, left.G + right.G, left.B + right.B);
    }
    
    public static RGB operator /(RGB left, RGB right)
    {
        return new RGB(left.R / right.R, left.G / right.G, left.B / right.B);
    }
    
    public static RGB operator -(RGB left, RGB right)
    {
        return new RGB(left.R - right.R, left.G - right.G, left.B - right.B);
    }

    public Color ToColor()
    {
        int r = Math.Clamp(R, 0, 255);
        int g = Math.Clamp(G, 0, 255);
        int b = Math.Clamp(B, 0, 255);
        
        return Color.FromArgb(255, r, g, b);
    }

    public static RGB ToRGB(Color color)
    {
        return new RGB(color.R, color.G, color.B);
    }
}