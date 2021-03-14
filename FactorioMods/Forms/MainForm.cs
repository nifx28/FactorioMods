using FactorioMods.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Windows.Forms;
using static FactorioMods.Helper.Helper;

namespace FactorioMods.Forms
{
    public partial class MainForm : Form
    {
        private string appData, modsPath;
        private readonly string modsConf = "mod-list.json";

        public static string Title { get; private set; }
        private readonly SynchronizationContext _context;

        public MainForm()
        {
            InitializeComponent();
            Title = Text;
            
            appData = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData)
                    .AppendSeparator();

            modsPath = Path.Combine(appData, "Factorio/mods".ReplaceSeparator())
                .AppendSeparator();

            _context = SynchronizationContext.Current;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            worker.RunWorkerAsync(radEnabled.Checked);
            progBar.PerformStep();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool byEnabled = (bool)e.Argument;
            var worker = sender as BackgroundWorker;
            var confFile = Path.Combine(modsPath, modsConf);

            if (!File.Exists(confFile))
            {
                _context.Post(state =>
                {
                    this.ShowError(@"找不到路徑 """ + confFile.Replace(appData, string.Empty) + @"""。", "背景作業");
                });
                e.Cancel = true;
                return;
            }

            var platform = string.Empty;
            var instPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ProgramFiles),
                "Factorio");

            if (Directory.Exists(instPath))
            {
                platform = "[Setup]";
            }
            else
            {
                instPath = null;
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 427520"))
                {
                    if (key != null)
                    {
                        instPath = key.GetValue("InstallLocation") as string;
                    }
                }

                if (Directory.Exists(instPath))
                {
                    platform = "[Steam]";
                }
            }

            var baseVer = "default";

            if (!instPath.IsEmpty())
            {
                instPath = Path.Combine(instPath, "data/base/info.json".ReplaceSeparator());

                if (File.Exists(confFile))
                {
                    var info = JsonSerializer.Deserialize<InfoDesc>(File.ReadAllBytes(instPath));
                    baseVer = info.Version;
                }
            }

            var fac = JsonSerializer.Deserialize<Models.FactorioMods>(File.ReadAllBytes(confFile));

            fac.Mods.Where(x =>
            {
                if (x.Enabled == byEnabled)
                {
                    if (x.Name.Length > fac.NameLength)
                    {
                        fac.NameLength = x.Name.Length;
                    }

                    fac.ModsDict[x.Name + (!x.Version.IsEmpty() ? $"_{x.Version}" : string.Empty)] =
                        Tuple.Create(x, (x.Name == "base" ? $"({baseVer}) {platform}" : string.Empty), string.Empty);
                    return true;
                }

                return false;
            }).ToList();

            Directory.GetFiles(modsPath, "*.zip")
                .Where(x =>
                {
                    x = x.Replace(modsPath, string.Empty);
                    var key = Path.GetFileNameWithoutExtension(x);

                    if (fac.ModsDict.ContainsKey(key))
                    {
                        var item = fac.ModsDict[key];
                        fac.ModsDict[key] = Tuple.Create(item.Item1, key, x);
                        return true;
                    }

                    key = x.Substring(0, x.LastIndexOf('_'));

                    if (fac.ModsDict.ContainsKey(key))
                    {
                        var item = fac.ModsDict[key];
                        fac.ModsDict[key] = Tuple.Create(item.Item1, key, x);
                        return true;
                    }

                    return false;
                }).ToList();

            worker.ReportProgress(100, fac.ModsDict.Count);
            e.Result = fac as Factorio;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = GetTitle($"{e.UserState} 個模組");
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                var fac = e.Result as Models.Factorio;

                foreach (KeyValuePair<string, Tuple<ModDesc, string, string>> item in fac.ModsDict)
                {
                    lstMods.Items.Add(
                        new KeyValuePair<string, Tuple<ModDesc, string, string>>(
                            $"{item.Key.PadRight(fac.NameLength + 1)}: {(item.Value.Item3.IsEmpty() ? item.Value.Item2 : item.Value.Item3)}",
                            item.Value));
                }
            }

            progBar.Value = progBar.Maximum;
        }

        private void btnReload_CheckedChanged(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                progBar.Value = progBar.Minimum;
                lstMods.Items.Clear();
                progBar.PerformStep();
                worker.RunWorkerAsync(radEnabled.Checked);
            }
        }

        private void btnPack_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.Enabled = false;
            progBar.Value = progBar.Minimum;

            var confPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                $"Exports/{modsConf}".ReplaceSeparator());

            var zipPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                $"Exports/mods-{DateTimeOffset.Now:yyyyMMdd}.zip".ReplaceSeparator());

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(confPath));

                using (StreamWriter sw = File.CreateText(confPath))
                {
                    var fac = new Models.FactorioMods();
                    var list = new List<ModDesc>();

                    foreach (KeyValuePair<string, Tuple<ModDesc, string, string>> item in lstMods.Items)
                    {
                        var modDesc = item.Value.Item1;
                        modDesc.Enabled = true;
                        list.Add(modDesc);
                    }

                    fac.Mods = list;
                    sw.Write(JsonSerializer.Serialize(fac, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        WriteIndented = true
                    }));
                }

                Directory.CreateDirectory(Path.GetDirectoryName(zipPath));

                using (ZipArchive arc = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    progBar.PerformStep();
                    arc.CreateEntryFromFile(confPath, modsConf);

                    foreach (KeyValuePair<string, Tuple<ModDesc, string, string>> item in lstMods.Items)
                    {
                        if (!item.Value.Item3.IsEmpty())
                        {
                            progBar.PerformStep();
                            arc.CreateEntryFromFile(
                                Path.Combine(modsPath, item.Value.Item3),
                                item.Value.Item3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                button.Enabled = true;
                progBar.Value = progBar.Maximum;

                this.ShowError($@"匯出到 ""{zipPath}"" 失敗！{Environment.NewLine}　{Marshal.GetExceptionForHR(ex.HResult).Message}", "匯出壓縮包");
                return;
            }

            progBar.Value = progBar.Maximum;

            this.ShowInfo($@"匯出到 ""{zipPath}"" 完成。", "匯出壓縮包");
            button.Enabled = true;
        }
    }
}
