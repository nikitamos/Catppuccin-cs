namespace CatppuccinCs;

public record CatppuccinColor(
    string Name,
    System.Drawing.Color Color,
    CatppuccinColorId ColorId,
    bool Accent
)
{
    public static implicit operator System.Drawing.Color(CatppuccinColor c) => c.Color;
}