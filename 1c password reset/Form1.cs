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

namespace _1c_password_reset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string file1c = @"C:\Program Files\1cv8\common\1cestart.exe";

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(file1c))
            {
               
                Process.Start(file1c);

            }
            else if (!File.Exists(file1c))
            {
                label1.Text = "Файл для запуска 1с не существует!";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
