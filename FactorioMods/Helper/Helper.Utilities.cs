using System.IO;

namespace FactorioMods.Helper
{
    public static partial class Helper
    {
        public static string ReplaceSeparator(this string str) =>
            str.Replace('/', Path.DirectorySeparatorChar);

        public static string AppendSeparator(this string str) =>
            !str.EndsWith(Path.DirectorySeparatorChar) ? str + Path.DirectorySeparatorChar : str;
    }
}
