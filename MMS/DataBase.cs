using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS
{
    class DataBase
    {
        private SQLiteConnection sqlite;
        private SQLiteCommand sqlite_cmd;

        public DataBase()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            sqlite = new SQLiteConnection("Data Source=E:/sqlitestudio-3.1.1/SQLiteStudio/database.db");

        }

        public void selectQuery(string query)
        {
            // Open
            sqlite.Open();

            // 要下任何命令先取得該連結的執行命令物件
            sqlite_cmd = sqlite.CreateCommand();

            // 要下的命令新增一個表
            sqlite_cmd.CommandText = "CREATE TABLE test (id integer primary key, text varchar(10));";


            sqlite_cmd.ExecuteNonQuery();

            // 插入一筆
            sqlite_cmd.CommandText = "INSERT INTO test (id, text) VALUES (1, '測試1');";


            sqlite_cmd.ExecuteNonQuery();

            // 查詢剛新增的表test
            sqlite_cmd.CommandText = "SELECT * FROM test";

            // 執行查詢塞入 sqlite_datareader
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

            // 一筆一筆列出查詢的資料
            while (sqlite_datareader.Read())
            {
                // Print out the content of the text field:
                String s = sqlite_datareader["text"].ToString();
                Console.WriteLine(s);

            }

            //結束
            sqlite.Close();
        }
    }
}
