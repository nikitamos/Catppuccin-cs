# Experimental C# Bindings of [Catppuccin Color Theme](https://github.com/catppuccin/catppuccin)
The aim of this project is to provide minimalistic yet convenient bindings of Catppuccin color themes to C# programming languages.

The color themes are automatically parsed and appropriate bindings are auto-generated.

## Work-in-progress notice
The work on the project is still in progress. Public API may change. No backward compatibility is guaranteed.

### Jobs to be done
* [ ] Write tests.
* [ ] Write docs.
* [x] Fix first build failure.
* [x] Stabilize `CatppuccinColor` API.
* [ ] Release on [NuGet](nuget.org).

## Installation
To use the library in your MSBuild project, add this repo as a submodule to your git
repository and *ProjectReference* the `CatppuccinCs`.

## Example

```csharp
using CatppuccinCs;

// Catppuccin class is an access point to the flavors.
CatppuccinFlavor latte = Catppuccin.Latte;
Console.WriteLine($"{latte.Name} {latte.Emoji}"); // Latte 🌻

// CatppuccinFlavor record contains colors as its fields.
CatppuccinColor teal = latte.Teal;

// Representation of the color in various formats.
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
```
**Note.** See also [`SimpleExample/Program.cs`](https://github.com/nikitamos/Catppuccin-cs/blob/master/SimpleExample/Program.cs).