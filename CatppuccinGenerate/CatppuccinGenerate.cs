namespace CatppuccinGenerate;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text.Json;


public class GenerateCatppuccinBindings : Task
{
    public string OutputDirectory { get; set; }
    [Output]
    public string[] Outputs { get; set; } = [];
    private const string FLAVOR_RECORD_FILENAME = "CatppuccinFlavor.g.cs";
    private const string CATPPUCCIN_CLASS_FILENAME = "Catppuccin.g.cs";
    private const string CATPPUCCIN_COLORS_ENUM_FILENAME = "CatppuccinColorId.g.cs";
    CatppuccinPalettes _palettes;
    public override bool Execute()
    {
        using var s = Assembly.GetAssembly(GetType())!.GetManifestResourceStream("palette.json")!;
        _palettes = JsonSerializer.Deserialize<CatppuccinPalettes>(s)!;
        Outputs = [
            GenerateFlavorRecord(),
            GenerateColorsEnum(),
            GenerateCatppuccin()
        ];
        return true;
    }
    public string GenerateCatppuccin()
    {
        var filepath = Path.Join(OutputDirectory, CATPPUCCIN_CLASS_FILENAME);
        using var f = File.CreateText(filepath);
        using var writer = new IndentedTextWriter(f);
        writer.WriteLine("#nullable enable\nnamespace CatppuccinCs;\npublic static partial class Catppuccin");
        writer.Block(() =>
        {
            WriteCatppuccinFlavorField(_palettes.latte, writer);
            WriteCatppuccinFlavorField(_palettes.frappe, writer, "Frappe");
            WriteCatppuccinFlavorField(_palettes.macchiato, writer);
            WriteCatppuccinFlavorField(_palettes.mocha, writer);
            WriteSwitchAccessor(writer, "public static CatppuccinFlavor? GetFlavorById", "CatppuccinFlavorId", (s) => $"CatppuccinFlavorId.{s}", ["Latte", "Frappe", "Macchiato", "Mocha"]);
            writer.WriteLine("public static readonly int ColorCount = {0};", _palettes.latte.ColorCount);
        });
        return filepath;
    }
    public void WriteCatppuccinFlavorField(CatppuccinFlavor t, IndentedTextWriter w, string csFlavorName = null)
    {
        w.WriteLine($"public static readonly CatppuccinFlavor {csFlavorName ?? Capitalize(t.name)} = new");
        w.Block(() =>
        {
            foreach (var c in t.colors)
                WriteFlavorColorField(w, c.Value.name, c.Value.accent, c.Value.hex, CatppuccinColor.GetCsColorName(c.Key));
            foreach (var c in t.ansiColors)
            {
                WriteFlavorColorField(w, c.Value.normal.name, false, c.Value.normal.hex, CatppuccinAnsiColor.GetNormalName(c.Key));
                WriteFlavorColorField(w, c.Value.bright.name, false, c.Value.bright.hex, CatppuccinAnsiColor.GetBrightName(c.Key));
            }
            w.WriteLine($"Name: \"{t.name}\",");
            w.WriteLine($"Id: CatppuccinFlavorId.{csFlavorName ?? Capitalize(t.name)},");
            w.WriteLine($"Emoji: \"{t.emoji}\",");
            w.WriteLine($"IsDark: {t.dark.ToString().ToLower()}");
        }, "(", ");");
    }

    private static void WriteFlavorColorField(IndentedTextWriter w, string humanReadableName, bool accent, string hexColor, string csColorName)
    {
        w.WriteLine($"{csColorName}: new");
        w.Block(() =>
        {
            w.WriteLine($"Name: \"{humanReadableName}\",");
            w.WriteLine($"Color: System.Drawing.ColorTranslator.FromHtml(\"{hexColor}\"),");
            w.WriteLine($"ColorId: CatppuccinColorId.{csColorName},");
            w.WriteLine($"Accent: {accent.ToString().ToLower()}");
        }
        , "(", "),");
    }

    public string GenerateFlavorRecord()
    {
        var filepath = Path.Join(OutputDirectory, FLAVOR_RECORD_FILENAME);
        using var f = File.CreateText("CatppuccinFlavor.g.cs");
        using var writer = new IndentedTextWriter(f);
        writer.WriteLine("#nullable enable\nnamespace CatppuccinCs;\npublic partial record CatppuccinFlavor");
        writer.Block(() =>
        {
            foreach (var col in _palettes.latte.CsColorNames)
                writer.WriteLine("CatppuccinColor {0},", col);
            writer.WriteLine("string Name,");
            writer.WriteLine("CatppuccinFlavorId Id,");
            writer.WriteLine("string Emoji,");
            writer.WriteLine("bool IsDark");
            writer.Indent = 0;
        }, "(", ")");
        writer.Block(() =>
        {
            WriteSwitchAccessor(writer,
                                "public CatppuccinColor? GetColorById",
                                "CatppuccinColorId",
                                s => $"CatppuccinColorId.{s}",
                                _palettes.latte.CsColorNames);
        });
        return filepath;
    }
    public string GenerateColorsEnum()
    {
        var filepath = Path.Join(OutputDirectory, CATPPUCCIN_COLORS_ENUM_FILENAME);
        using var f = File.CreateText(filepath);
        using var writer = new IndentedTextWriter(f);
        writer.WriteLine("namespace CatppuccinCs;\npublic enum CatppuccinColorId");
        writer.Block(() =>
        {
            foreach (var c in _palettes.latte.CsColorNames)
                writer.WriteLine("{0},", c);
        });
        return filepath;
    }
    public static void WriteSwitchAccessor(IndentedTextWriter w,
                                              string MethodNameWithModifiers,
                                              string ParameterType,
                                              Func<string, string> formatter,
                                              IEnumerable<string> fields)
    {
        w.Write("{0} ({1} id) =>", MethodNameWithModifiers, ParameterType);
        w.Indent++;
        w.WriteLine("id switch");
        w.Block(() =>
        {
            foreach (var f in fields)
                w.WriteLine("{0} => {1},", formatter(f), f);
            w.WriteLine("_ => null");
        }, closeBrace: "};");
        w.Indent--;
    }
    public static string Capitalize(string s) => string.Concat(s[0].ToString().ToUpper(), s.AsSpan(1));
}
