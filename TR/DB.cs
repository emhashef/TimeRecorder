using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace TR
{
    class DB
    {
        public SQLiteConnection connection = new SQLiteConnection($"DataSource ={Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\TimeRecorder\\Time_record.db; Version=3;");
        public DB()
        {
            
            createTableIfNotExist();

        }

        public DataTable get(string query)
        {
            connection.OpenAsync();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, connection);
            DataTable result = new DataTable();
            dataAdapter.Fill(result);
            connection.Close();
            return result;
        }
        public void run(string query)
        {
            connection.OpenAsync();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
        private void createTableIfNotExist()
        {
            run("create table if not exists TRTable (id INTEGER PRIMARY KEY UNIQUE NOT NULL, t INTEGER NOT NULL, d  DATE NOT NULL)");
        }

    }
}
