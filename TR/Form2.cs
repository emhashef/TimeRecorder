using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TR
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            

            checkBox1.Checked = (bool)Properties.Settings.Default.top_most;

            TimeSpan ts = TimeSpan.FromSeconds((long)Properties.Settings.Default.danger_time);
            dateTimePicker1.Value = new DateTime(1900,1,1,ts.Hours,ts.Minutes,ts.Seconds);
            checkbox2.Checked = !(bool)Properties.Settings.Default.show_in_taskbar;
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Properties.Settings.Default.top_most = checkBox1.Checked;
            Properties.Settings.Default.show_in_taskbar = !checkbox2.Checked;
            Properties.Settings.Default.danger_time = (long)(dateTimePicker1.Value.Hour * 3600 + dateTimePicker1.Value.Minute * 60 + dateTimePicker1.Value.Second);
            Properties.Settings.Default.Save();

        
            MessageBox.Show("setting saved !!!!\n you must restart app to see changes !!!!");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
