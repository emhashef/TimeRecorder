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
        long secc;
        long idd;
        DateTime d;
        long settime;
        bool danger = false ;
        private System.Threading.Timer timer;
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
         
           
            TopMost = (bool)Properties.Settings.Default["top_most"];
            ShowInTaskbar = (bool)Properties.Settings.Default["show_in_taskbar"];
            settime = (long)Properties.Settings.Default["danger_time"];
            

            DB db = new DB();
            DataTable table = db.get($"SELECT * FROM TRTable Where d = '{DateTime.Now.Date.ToString("yyyy-MM-dd")}'");

            if(table.Rows.Count == 0)
            {
                db.run($"insert into TRTable(t,d) VALUES (0, '{DateTime.Now.Date.ToString("yyyy-MM-dd")}' )");
                table = db.get("SELECT t,id,d FROM TRTable ORDER BY id DESC LIMIT 1");
                
            }
            secc = (long)table.Rows[0]["t"];
            idd = (long)table.Rows[0]["id"];
            

            

            timer = new System.Threading.Timer(new TimerCallback(thread1), null, 0, 1000);
            
        }
        delegate void StringArgReturningVoidDelegate(string text);
        private void thread1(object ob)
        {
            if (settime <= secc && !danger)
            {
                label1.ForeColor = Color.Red;
                danger = true;
                StringArgReturningVoidDelegate fds = new StringArgReturningVoidDelegate(redable);
                Invoke(fds, new object[] {null});
                
            }
            secc++;
            StringArgReturningVoidDelegate stringArgReturningVoidDelegate = new StringArgReturningVoidDelegate(set);
            Invoke(stringArgReturningVoidDelegate, TimeSpan.FromSeconds(secc).ToString());
            save_sec();
            //

        }
        private void thread2(object ob)
        {
            
        }
        private void redable (string st){
            Show();


        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            //save_sec();
        }
        private void save_sec()
        {
            DB db = new DB();
            db.run("update TRTable set t = " + secc + " where id = " + idd);
          
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


