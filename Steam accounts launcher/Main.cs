using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_accounts_launcher
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        static string settingsFileName = "settings.ini";
        Structs.Settings currentSettings;
        private void Main_Load(object sender, EventArgs e)
        {

            if (File.Exists(settingsFileName))
            {
                currentSettings = JsonConvert.DeserializeObject<Structs.Settings>(File.ReadAllText("settings.ini"));
            }
            else
            {
                currentSettings = new Structs.Settings() { accounts = new List<Structs.AccountSettings>() };
            }
            UpdateListBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 || listBox1.SelectedIndex >= currentSettings.accounts.Count())
            {
                if (MessageBox.Show("Delete?", "caution", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    currentSettings.accounts.RemoveAt(listBox1.SelectedIndex);
                    SaveCurrentSettings();
                    UpdateListBox();
                }
            }
        }
        void UpdateListBox()
        {
            listBox1.Items.Clear();
            foreach (var each in currentSettings.accounts)
            {
                if (!each.name.ToLower().StartsWith("main"))
                {
                    listBox1.Items.Add($"{each.name} - {each.login}");
                }
                else
                {
                    listBox1.Items.Add($"{each.name}");
                }
            }
        }
        void SaveCurrentSettings()
        {
            string json = JsonConvert.SerializeObject(currentSettings);
            File.WriteAllText(settingsFileName, json);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddForm f = new AddForm();
            f.ShowDialog();
            if (f.isAdding)
            {
                currentSettings.accounts.Add(new Structs.AccountSettings() { login = f.accLoginSelected, steamDirectory = f.steamPathSelected, name= f.nameSelected });
                SaveCurrentSettings();
                UpdateListBox();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 || listBox1.SelectedIndex >= currentSettings.accounts.Count())
            {
                return;
            }
            string oldText = button1.Text;
            button1.Enabled = false;
            button1.Text = "...";
            if (!await KillAllSteamProccesses())
            {
                MessageBox.Show("Can't shutdown all steam processes, try again later", "error");
                button1.Text = oldText;
                button1.Enabled = true;
                return;
            }
            SetAccountAndStart(currentSettings.accounts[listBox1.SelectedIndex]);
            button1.Text = oldText;
            button1.Enabled = true;

        }
        void SetAccountAndStart(Structs.AccountSettings input)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam", true);
            string steamExePath = Path.Combine(input.steamDirectory, "steam.exe");
            if (!File.Exists(steamExePath))
            {
                return;
            }
            key.SetValue("AutoLoginUser", input.login, RegistryValueKind.String);
            key.SetValue("SourceModInstallPath", Path.Combine(input.steamDirectory, "steamapps", "sourcemods"), RegistryValueKind.String);
            key.SetValue("SteamExe", steamExePath.Replace("\\", "/"), RegistryValueKind.String);
            key.SetValue("SteamPath", input.steamDirectory.Replace("\\", "/"), RegistryValueKind.String);

            string vdfUserConfigPath = Path.Combine(input.steamDirectory, "config", "loginusers.vdf");
            if (File.Exists(vdfUserConfigPath))
            {
                string readed = File.ReadAllText(vdfUserConfigPath);
                readed = readed.Replace("\"SkipOfflineModeWarning\"\t\t\"0\"", "\"SkipOfflineModeWarning\"\t\t\"1\"");
                File.WriteAllText(vdfUserConfigPath, readed);
            }
           
            Process.Start(steamExePath);
        }
        async Task<bool> KillAllSteamProccesses()
        {
            string[] namesToKill = new string[] { "steamwebhelper", "steam", "steamservice" };
            Process[] procs = Process.GetProcesses();
            foreach(var each in procs)
            {
                try
                {
                    if (namesToKill.Contains(each.ProcessName))
                    {
                        each.Kill();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            await Task.Delay(3000);
            bool isKilledAll = true;
            procs = Process.GetProcesses();
            foreach (var each in procs)
            {
                try
                {
                    if (namesToKill.Contains(each.ProcessName))
                    {
                        isKilledAll = false;
                        break;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return isKilledAll;

        }
    }
}
