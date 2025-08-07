using CatppuccinCs;
using System.Drawing;
// Catppuccin class is an access point to the themes.
var latte = Catppuccin.Latte;
Console.WriteLine($"{latte.Name} {latte.Emoji}"); // Latte 🌻

// So far colors are stored in System.Drawing.Color structure.
// Color [A=255, R=221, G=120, B=120]
Console.WriteLine($"{latte.Flamingo.Color}");
Console.WriteLine($"{latte.AnsiBlueBright.Name}");

// Implicit cast to System.Drawing.Color is available.
Color c = latte.Base;

PrintColorInfo(Catppuccin.Frappe, CatppuccinColorId.Green);
PrintColorInfo(Catppuccin.Macchiato, CatppuccinColorId.AnsiCyanNormal);
PrintColorInfo(Catppuccin.Mocha, CatppuccinColorId.Overlay1);

void PrintColorInfo(CatppuccinFlavor theme, CatppuccinColorId col)
{
    Console.Write($"In '{theme.Name}{theme.Emoji}' flavor ");
    CatppuccinColor color = theme.GetColorById(col)!;
    Console.WriteLine($"'{color.Name}' color is {color.Color}");
}