#pragma warning disable CS8618
#pragma warning disable IDE1006

namespace CatppuccinGenerate;
public record CatppuccinColor(
    string name,
    int order,
    bool accent,
    string hex
) {
    public static string GetCsColorName(string s) => GenerateCatppuccinBindings.Capitalize(s);
}
public record CatppuccinAnsiColor(
    CatppuccinColor normal,
    CatppuccinColor bright
) {
    public static string GetBrightName(string c) => $"Ansi{GenerateCatppuccinBindings.Capitalize(c)}Bright";
    public static string GetNormalName(string c) => $"Ansi{GenerateCatppuccinBindings.Capitalize(c)}Normal";
}

public record CatppuccinFlavor(
    string name,
    string emoji,
    int order,
    bool dark,
    Dictionary<string, CatppuccinColor> colors,
    Dictionary<string, CatppuccinColor> ansiColors
)
{
    public IEnumerable<string> CsColorNames { get => _csColorNames; }
    private readonly string[] _csColorNames = colors.Keys.Select(CatppuccinColor.GetCsColorName)
                .Concat(
                 (IEnumerable<string>)ansiColors.Keys.Select(CatppuccinAnsiColor.GetNormalName)
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