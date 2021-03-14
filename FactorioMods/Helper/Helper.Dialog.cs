using FactorioMods.Forms;
using System.Windows.Forms;

namespace FactorioMods.Helper
{
    public static partial class Helper
    {
        public static void Show(this Form form, string text, string caption = default, MessageBoxButtons buttons = default, MessageBoxIcon icon = default, bool showVer = true)
        {
            _ = MessageBox.Show(form, $"　{text}", GetTitle(caption, showVer), buttons, icon);
        }

        public static void ShowInfo(this Form form, string text, string caption = default, bool showVer = true)
        {
            Show(form, text, caption, default, MessageBoxIcon.Information, showVer);
        }

        public static void ShowError(this Form form, string text, string caption = default, bool showVer = true)
        {
            Show(form, text, caption, default, MessageBoxIcon.Error, showVer);
        }
    }
}
