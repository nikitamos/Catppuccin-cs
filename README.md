# C# Bindings for Catppuccin Color Themes
The aim of this project is to provide minimalistic yet convenient bindings of Catppuccin color themes to C# programming languages.

Original palettes are defined in JSON format in catppuccin/palette repo. They are automatically parsed and appropriate bindings are auto-generated.

# How to use
<!-- ## Installation
The library is still in development and there is no NuGet package yet so the only way to use it is to add this repo to your project as a git submodule and manually `ProjectReference` CatppuccinCs.

## Architecture
The symbols are located in the `CatppuccinCs` namespace. -->

<!-- The `Catppuccin` class provides  -->

```csharp
using CatppuccinCs;
using System.Drawing;
// Catppuccin class is an access point to the themes.
var latte = Catppuccin.Latte;
Console.WriteLine($"{latte.Name} {latte.Emoji}"); // Latte 🌻

// So far colors are stored in System.Drawing.Color structure.
// Color [A=255, R=221, G=120, B=120]
Console.WriteLine($"{latte.Flamingo.Color}");

// Implicit cast to System.Drawing.Color is available.
Color c = latte.Base;
```
Please note, that the development is still in progress and existing API is subject to change (especially `System.Drawing.Color` may be replaced with something else).