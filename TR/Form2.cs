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
            SQLiteConnection sqLiteConnection = new SQLiteConnection("DataSource =Time_record.db; Version=3;");

            sqLiteConnection.Open();
            SQLiteCommand optioncmmd = sqLiteConnection.CreateCommand();
            optioncmmd.CommandText = "select * from options ";
            SQLiteDataReader optionsreader = optioncmmd.ExecuteReader();
            optionsreader.Read();

            checkBox1.Checked = optionsreader.GetBoolean(1);
            
            checkbox2.Checked = !optionsreader.GetBoolean(2);
            optionsreader.Close();
            sqLiteConnection.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqLiteConnection = new SQLiteConnection("DataSource =Time_record.db; Version=3;");

            sqLiteConnection.Open();
            SQLiteCommand optioncmmd = sqLiteConnection.CreateCommand();
            if (checkBox1.Checked)
                optioncmmd.CommandText = "update options set topmost = 1 where id = 1";
            else
                optioncmmd.CommandText = "update options set topmost = 0 where id = 1";
            optioncmmd.ExecuteNonQuery();
            if (checkbox2.Checked)
                optioncmmd.CommandText = "update options set showintaskbar = 0 where id = 1";
            else
                optioncmmd.CommandText = "update options set showintaskbar = 1 where id = 1";
            optioncmmd.ExecuteNonQuery();

            //SQLiteDataReader optionsreader = optioncmmd.ExecuteReader();
            //optionsreader.Read();

            //checkBox1.Checked = optionsreader.GetBoolean(2);
            //optionsreader.Read();
            //checkbox2.Checked = !optionsreader.GetBoolean(2);
            //optionsreader.Close();
            sqLiteConnection.Close();
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
