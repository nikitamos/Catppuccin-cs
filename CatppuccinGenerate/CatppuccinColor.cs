#pragma warning disable CS8618
#pragma warning disable IDE1006

using System.Globalization;

namespace CatppuccinGenerate;
public record struct RgbColor(
    byte r, byte g, byte b
)
{
    public override readonly string ToString() => $"({r}, {g}, {b})";
}
public record struct HslColor(
    float h, float s, float l
)
{
    public override readonly string ToString() => String.Format(CultureInfo.InvariantCulture, "({0}f, {1}f, {2}f)", h, s, l);
}
public record CatppuccinColor(
    string name,
    int order,
    bool accent,
    string hex,
    RgbColor rgb,
    HslColor hsl
)
{
    public static string GetCsColorName(string s) => GenerateCatppuccinBindings.Capitalize(s);
}
public record CatppuccinAnsiColor(
    CatppuccinColor normal,
    CatppuccinColor bright
)
{
    public static string GetBrightName(string c) => $"Ansi{GenerateCatppuccinBindings.Capitalize(c)}Bright";
    public static string GetNormalName(string c) => $"Ansi{GenerateCatppuccinBindings.Capitalize(c)}Normal";
}

public record CatppuccinFlavor(
    string name,
    string emoji,
    int order,
    bool dark,
    Dictionary<string, CatppuccinColor> colors,
    Dictionary<string, CatppuccinAnsiColor> ansiColors
)
{
    public IEnumerable<string> CsColorNames { get => _csColorNames; }
    public int ColorCount { get => _csColorNames.Length; }
    private readonly string[] _csColorNames = colors.Keys.Select(CatppuccinColor.GetCsColorName)
                .Concat(
                  ansiColors.Keys.Select(CatppuccinAnsiColor.GetNormalName)
               ).Concat(
                  ansiColors.Keys.Select(CatppuccinAnsiColor.GetBrightName)
               ).ToArray();
}

public record CatppuccinPalettes(
    string version,
    CatppuccinFlavor latte,
    CatppuccinFlavor frappe,
    CatppuccinFlavor macchiato,
    CatppuccinFlavor mocha
);