namespace CatppuccinGenerate;
using Microsoft.Build.Utilities;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Reflection;
using System.Text.Json;


public class GenerateCatppuccinBindings : Task
{
    CatppuccinPalettes _palettes;
    public override bool Execute()
    {
        using var s = Assembly.GetAssembly(GetType())!.GetManifestResourceStream("palette.json")!;
        _palettes = JsonSerializer.Deserialize<CatppuccinPalettes>(s)!;
        GenerateThemeRecord();
        GenerateColorsEnum();
        GenerateCatppuccin();
        return true;
    }
    public void GenerateCatppuccin()
    {
        using var f = File.CreateText("Catppuccin.g.cs");
        using var writer = new IndentedTextWriter(f);
        writer.WriteLine("namespace CatppuccinCs;\npublic static class Catppuccin {");
        writer.Indent++;
        WriteThemeField(_palettes.latte, writer);
        WriteThemeField(_palettes.frappe, writer, "Frappe");
        WriteThemeField(_palettes.macchiato, writer);
        WriteThemeField(_palettes.mocha, writer);
        writer.Indent--;
        writer.WriteLine("}");
    }
    public void WriteThemeField(CatppuccinTheme t, IndentedTextWriter w, string? csThemeName = null)
    {
        w.WriteLine($"public static readonly CatppuccinTheme {Capitalize(t.name)} = new(");
        w.Indent++;
        foreach (var c in t.colors)
        {
            WriteThemeColorField(w, c.Value.name, c.Value.accent, c.Value.hex, CatppuccinColor.GetCsColorName(c.Key));
        }
        foreach (var c in t.ansiColors)
        {
            WriteThemeColorField(w, c.Value.name, false, c.Value.hex, CatppuccinAnsiColor.GetNormalName(c.Key));
            WriteThemeColorField(w, c.Value.name, false, c.Value.hex, CatppuccinAnsiColor.GetBrightName(c.Key));
        }
        w.WriteLine($"Name: \"{t.name}\",");
        w.WriteLine($"Id: CatppuccinThemeId.{csThemeName ?? Capitalize(t.name)},");
        w.WriteLine($"Emoji: \"{t.emoji}\",");
        w.WriteLine($"IsDark: {t.dark.ToString().ToLower()}");
        w.Indent--;
        w.WriteLine(");");
    }

    private static void WriteThemeColorField(IndentedTextWriter w, string humanReadableName, bool accent, string hexColor, string csColorName)
    {
        w.WriteLine($"{csColorName}: new(");
        w.Indent++;
        w.WriteLine($"Name: \"{humanReadableName}\",");
        w.WriteLine($"Color: System.Drawing.ColorTranslator.FromHtml(\"{hexColor}\"),");
        w.WriteLine($"ColorId: CatppuccinColorId.{csColorName},");
        w.WriteLine($"Accent: {accent.ToString().ToLower()}");
        w.Indent--;
        w.WriteLine("),");
    }

    public void GenerateThemeRecord()
    {
        using var f = File.CreateText("CatppuccinTheme.g.cs");
        using var writer = new IndentedTextWriter(f);
        writer.WriteLine("namespace CatppuccinCs;\npublic record CatppuccinTheme(");
        writer.Indent++;
        foreach (var col in _palettes.latte.CsColorNames)
        {
            writer.WriteLine("CatppuccinColor {0},", col);
        }
        writer.WriteLine("string Name,");
        writer.WriteLine("CatppuccinThemeId Id,");
        writer.WriteLine("string Emoji,");
        writer.WriteLine("bool IsDark");
        writer.Indent = 0;
        writer.WriteLine(");");
    }
    public void GenerateColorsEnum()
    {
        using var f = File.CreateText("CatppuccinColorId.g.cs");
        using var writer = new IndentedTextWriter(f);
        writer.WriteLine("namespace CatppuccinCs;\npublic enum CatppuccinColorId {");
        writer.Indent = 1;

        foreach (var c in _palettes.latte.CsColorNames)
            writer.WriteLine("{0},", c);

        writer.Indent = 0;
        writer.WriteLine("}");
    }
    public static string Capitalize(string s) => string.Concat(s[0].ToString().ToUpper(), s.AsSpan(1));
}
