using System.CodeDom.Compiler;

namespace CatppuccinGenerate;
static class IndentedTextWriterExtensions {
    public static void Block(this IndentedTextWriter w, Action action, string openBrace = "{", string closeBrace = "}") {
        w.WriteLine(openBrace);
        w.Indent++;
        action();
        w.Indent--;
        w.WriteLine(closeBrace);
    }
}