namespace CatppuccinCs;

public record CatppuccinColor(
    string Name,
    CatppuccinColorId ColorId,
    bool Accent,
    (byte R, byte G, byte B) Rgb,
    (float H, float S, float L) Hsl,
    string Hex
)
{
    public static explicit operator System.Drawing.Color(CatppuccinColor c) => System.Drawing.Color.FromArgb(c.Rgb.R, c.Rgb.G, c.Rgb.B);
}