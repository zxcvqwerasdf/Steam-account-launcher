using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_accounts_launcher
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }
        public bool isAdding = false;
        public string accLoginSelected;
        public string steamPathSelected;
        public string nameSelected;
        private void Class1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string accountLogin = textBox1.Text.ToLower();
            string steamPath = textBox2.Text;
            string name = textBox3.Text;
            if (!string.IsNullOrEmpty(accountLogin) && !string.IsNullOrEmpty(steamPath) && !string.IsNullOrEmpty(name))
            {
                accLoginSelected = accountLogin;
                steamPathSelected = steamPath;
                nameSelected = name;
                isAdding = true;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;

            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                textBox2.Text = folderPath;
            }
        }
    }
}
