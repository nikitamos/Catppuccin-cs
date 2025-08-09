namespace Tests;
using CatppuccinCs;
using static CatppuccinCs.Catppuccin;

public class ColorTests
{
    [Theory]
    [InlineData(CatppuccinFlavorId.Latte)]
    [InlineData(CatppuccinFlavorId.Frappe)]
    [InlineData(CatppuccinFlavorId.Macchiato)]
    [InlineData(CatppuccinFlavorId.Mocha)]
    public void CorrectLatteColorNames(CatppuccinFlavorId flavorId)
    {
        var flavor = Catppuccin.GetFlavorById(flavorId)!;
        string[] l = ["Teal", "Green", "Blue"];
        Assert.Equal([flavor.Teal.Name, flavor.Green.Name, flavor.Blue.Name], new List<string>(l.AsEnumerable()));
    }
    [Theory]
    [InlineData(CatppuccinFlavorId.Latte)]
    [InlineData(CatppuccinFlavorId.Frappe)]
    [InlineData(CatppuccinFlavorId.Macchiato)]
    [InlineData(CatppuccinFlavorId.Mocha)]
    public void CorrectFrappeAnsiColorName(CatppuccinFlavorId flavorId) {
        var flavor = Catppuccin.GetFlavorById(flavorId)!;
        string[] l = ["Bright Black", "Black", "Bright Magenta", "Magenta"];
        Assert.Equal([flavor.AnsiBlackBright.Name, flavor.AnsiBlackNormal.Name, flavor.AnsiMagentaBright.Name, flavor.AnsiMagentaNormal.Name],
                    l);
    }

    // [Fact]
    // public void CorrectMacchiatoColorValues() { }

    // [Fact]
    // public void CorrectMochaColorValues() { }

    // [Fact]
    // public void CorrectLatteColorValues() { }

    [Theory]
    [MemberData(nameof(CorrectRgbaConversionDatabase))]
    public void CorrectRgbaConversion(CatppuccinColor color, uint expected)
    {
        Assert.Equal(color.AsRgba(), expected);
    }

    [Theory]
    [MemberData(nameof(CorrectArgbConversionDatabase))]
    public void CorrectArgbConversion(CatppuccinColor color, uint expected)
    {
        Assert.Equal(color.AsArgb(), expected);
    }

    public static TheoryDataRow<CatppuccinColor, uint>[] CorrectRgbaConversionDatabase = [new(Latte.Text, 0x4c4f69ff), new(Mocha.Surface0, 0x313244ff)];
    public static TheoryDataRow<CatppuccinColor, uint>[] CorrectArgbConversionDatabase = [new(Latte.Text, 0xff4c4f69), new(Mocha.Surface0, 0xff313244)];
}