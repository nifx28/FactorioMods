using FactorioMods.Forms;
using System;
using System.Reflection;
using System.Threading;

namespace FactorioMods.Helper
{
    public static partial class Helper
    {
        private static string Version;

        static Helper()
        {
            System.Version ver = System.Version.Parse(
                Assembly.GetExecutingAssembly()
                    .GetCustomAttribute<AssemblyFileVersionAttribute>()
                    .Version);
            Version = $"v{ver.Major}.{ver.Minor}.{ver.Build}";
        }

        public static bool IsEmpty(this string str) =>
            string.IsNullOrEmpty(str);

        public static bool IsWhiteSpace(this string str) =>
            string.IsNullOrWhiteSpace(str);

        public static string GetTitle(string caption = default, bool showVer = true) =>
            (!caption.IsEmpty() ? $"{caption} - " : string.Empty) +
            MainForm.Title + (showVer ? $" {Version}" : string.Empty);

#nullable enable
        public static void Post(
            this SynchronizationContext _context,
            Action<object?> action,
            object? state = null) => _context.Post(new SendOrPostCallback(s => action.Invoke(s)), state);
        #nullable disable
    }
}
