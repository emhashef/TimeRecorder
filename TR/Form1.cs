using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
namespace TR
{
    public partial class Form1 : Form
    {
        int secc, idd;
        DateTime d;
         private System.Threading.Timer timer;
       // private System.Threading.Timer timer2;
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
           form2  = new Form2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            Location = new Point( Screen.PrimaryScreen.Bounds.Width - Size.Width,0);


            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            SQLiteConnection sqLiteConnection = new SQLiteConnection("DataSource =Time_record.db; Version=3;");

            sqLiteConnection.Open();
            SQLiteCommand tcmd = sqLiteConnection.CreateCommand();
            tcmd.CommandText = "create table if not exists TRTable (id INTEGER PRIMARY KEY UNIQUE NOT NULL, t INTEGER NOT NULL, d  DATE NOT NULL)";
            tcmd.ExecuteNonQuery();
            tcmd.CommandText = "create table if not exists options (id INTEGER PRIMARY KEY UNIQUE NOT NULL, op_name string NOT NULL, value  boolean NOT NULL)";
            tcmd.ExecuteNonQuery();
            
            //SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter("select id,time(t, 'unixepoch') as t,d from TRTable", "DataSource =Time_record.db; Version=3;");
            //DataSet dataSet = new DataSet();
            //sQLiteDataAdapter.FillSchema(dataSet, SchemaType.Source, "TRTable");
            //dataSet.Tables[0].Columns[1].DataType = typeof(string);
            //sQLiteDataAdapter.Fill(dataSet, "TRTable");

            //dataGridView1.AutoGenerateColumns = true ;
            //dataGridView1.DataSource = dataSet;
            //dataGridView1.DataMember = "TRTable";
            SQLiteCommand optioncmmd = sqLiteConnection.CreateCommand();
            optioncmmd.CommandText = "select * from options order by id asc";
            SQLiteDataReader optionsreader =  optioncmmd.ExecuteReader();
            optionsreader.Read();
            TopMost = optionsreader.GetBoolean(2);
            optionsreader.Read();
            ShowInTaskbar = optionsreader.GetBoolean(2);
            optionsreader.Close();
            
            SQLiteCommand sqlitecmd = sqLiteConnection.CreateCommand();
            sqlitecmd.CommandText = "SELECT t,id,d FROM TRTable ORDER BY id DESC LIMIT 1";
            SQLiteDataReader sQLiteDataReader = sqlitecmd.ExecuteReader();
            if(sQLiteDataReader.Read())
                d = sQLiteDataReader.GetDateTime(2);
            if(d.Date != DateTime.Now.Date)
            {
                sQLiteDataReader.Close();
                sqlitecmd.CommandText = "insert into TRTable(t,d) VALUES (0, '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' )";
                sqlitecmd.ExecuteNonQuery();
                sqlitecmd.CommandText = "SELECT t,id,d FROM TRTable ORDER BY id DESC LIMIT 1";
                sQLiteDataReader = sqlitecmd.ExecuteReader();
                sQLiteDataReader.Read();
            }
            secc = sQLiteDataReader.GetInt32(0);
            idd = sQLiteDataReader.GetInt32(1);
            sQLiteDataReader.Close();

            sqLiteConnection.Close();

            timer = new System.Threading.Timer(new TimerCallback(thread1), null, 0, 1000);
            //timer2 = new System.Threading.Timer(new TimerCallback(thread2), null, 30000, 30000);
        }
        delegate void StringArgReturningVoidDelegate(string text);
        private void thread1(object ob)
        {

            secc++;
            StringArgReturningVoidDelegate stringArgReturningVoidDelegate = new StringArgReturningVoidDelegate(set);
            Invoke(stringArgReturningVoidDelegate, TimeSpan.FromSeconds(secc).ToString());
            save_sec();
            //

        }
        private void thread2(object ob)
        {
            
        }


        private void form_closing(object sender, FormClosingEventArgs e)
        {
            //save_sec();
        }
        private void save_sec()
        {
            SQLiteConnection sqLiteConnection = new SQLiteConnection("DataSource =Time_record.db; Version=3; ");

            sqLiteConnection.Open();

            SQLiteCommand sQLiteCommand = sqLiteConnection.CreateCommand();

            sQLiteCommand.CommandText = "update TRTable set t = " + secc + " where id = " + idd;
            sQLiteCommand.ExecuteNonQuery();

            sqLiteConnection.Close();
        } 

        private void label1_Click(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = "Time recorder is running in background";
            notifyIcon1.ShowBalloonTip(1700);
            Hide();
        }

        private void itemclicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        

        private void iconclicked(object sender, EventArgs e)
        {
            Show();
        }

        private void itemExitclicked(object sender, EventArgs e)
        {
            Close();
        }

        private void itemoptionclicked(object sender, EventArgs e)
        {
           
            form2.ShowDialog();
        }

        private void set(string st)
        {
            label1.Text = st;

        }
    }
}


