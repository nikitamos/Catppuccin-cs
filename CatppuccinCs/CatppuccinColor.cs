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
    public uint AsRgba() => (((uint)Rgb.R) << 0x18) | (((uint)Rgb.G) << 0x10) | (((uint)Rgb.B) << 0x08) | 0xFF;
    public uint AsArgb() => (((uint)Rgb.R) << 0x10) | (((uint)Rgb.G) << 0x08) | ((uint)Rgb.B) | (((uint)0xFF) << 0x18);
}