namespace CatppuccinCs;

public record CatppuccinColor(
    string Name,
    System.Drawing.Color Color,
    CatppuccinColorId ColorId,
    bool Accent
);