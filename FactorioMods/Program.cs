using FactorioMods.Forms;
using System;
using System.Windows.Forms;

namespace FactorioMods
{
    static class Program
    {
        /// <summary>
        ///  ���ε{�����D�n�i�J�I�C
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
