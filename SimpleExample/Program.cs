using System.Globalization;
using CatppuccinCs;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

// Catppuccin class is an access point to the flavors.
CatppuccinFlavor latte = Catppuccin.Latte;
Console.WriteLine($"{latte.Name} {latte.Emoji}"); // Latte 🌻

// The CatppuccinFlavor class contains colors as its fields
CatppuccinColor teal = latte.Teal;

// Representation of the color in various formats
Console.WriteLine($"{teal.Hex}"); // #179299
Console.WriteLine($"{teal.Rgb}"); // (23, 146, 153)
Console.WriteLine($"{teal.Hsl}"); // (183.23077, 0.7386364, 0.34509805)

// Rgb and Hsl are stored as tuples with named fields
var mochaText = Catppuccin.Mocha.Text;
var (r, g, b) = mochaText.Rgb;
Console.WriteLine($"Mocha.Text = rgb({r}, {g}, {b})"); // Mocha.Text = rgb(205, 214, 244)
Console.WriteLine($"Mocha.Text = hsl({mochaText.Hsl.H}, {mochaText.Hsl.S}, {mochaText.Hsl.L})"); // Mocha.Text = hsl(226.15384, 0.6393443, 0.88039213)

// Human-readable color name
Console.WriteLine($"{teal.Name}"); // Teal

// An id to get the same color from another flavor
CatppuccinColorId tealId = teal.ColorId;
var frappeTeal = Catppuccin.Frappe.GetColorById(tealId)!;
Console.WriteLine($"{frappeTeal.Name} = {frappeTeal.Hex}"); // Teal = #81c8be


PrintColorInfo(Catppuccin.Frappe, CatppuccinColorId.Green);
PrintColorInfo(Catppuccin.Macchiato, CatppuccinColorId.AnsiCyanNormal);
PrintColorInfo(Catppuccin.Mocha, CatppuccinColorId.Overlay1);

// The function prints the name of color and its representation in hex, RGB and HSL formats in the color theme
void PrintColorInfo(CatppuccinFlavor theme, CatppuccinColorId col)
{
    Console.Write($"In '{theme.Name}{theme.Emoji}' flavor ");
    CatppuccinColor color = theme.GetColorById(col)!;
    Console.WriteLine($"'{color.Name}' color is {color.Hex} (a.k.a. {color.Rgb} a.k.a. {color.Hsl})");
}