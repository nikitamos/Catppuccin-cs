# Experimental C# Bindings of [Catppuccin Color Theme](https://github.com/catppuccin/catppuccin)
The aim of this project is to provide minimalistic yet convenient bindings of Catppuccin color themes to C# programming languages.

The color themes are automatically parsed and appropriate bindings are auto-generated.

## Work-in-progress notice
The work on the project is still in progress. Public API may change. No backward compatibility is guaranteed. `System.Drawing.Color` may be replaced with something else in future.

**Note.** To build `CatppuccinCs` the `dotnet build` command must be run twice.
### Jobs to be done
* [ ] Write tests.
* [ ] Write docs.
* [ ] Fix first build failure.
* [ ] Stabilize `CatppuccinColor` API.
* [ ] Release on [NuGet](nuget.org).

## Example

```csharp
using CatppuccinCs;
using System.Drawing;
// Catppuccin class is an access point to the flavors.
var latte = Catppuccin.Latte;
Console.WriteLine($"{latte.Name} {latte.Emoji}"); // Latte 🌻

// So far color values are stored in System.Drawing.Color structure.
// Color [A=255, R=221, G=120, B=120]
Console.WriteLine($"{latte.Flamingo.Color}");
Console.WriteLine($"{latte.AnsiBlueBright.Name}"); // Bright Blue
```
**Note.** See also `SimpleExample/Program.cs`.