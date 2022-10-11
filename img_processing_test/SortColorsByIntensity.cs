using System.Drawing;

namespace img_processing_test;

public class SortColorsByIntensity : IComparer<Color>
{
    public int Compare(Color x, Color y)
    {
        return x.GetSaturation() - y.GetSaturation() > 0 ? 1 : -1;
    }
}