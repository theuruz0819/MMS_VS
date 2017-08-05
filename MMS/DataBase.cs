using MMS.Data;
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
            sqlite = new SQLiteConnection("Data Source=H:/Balder/ERO/Manga/database.db");

        }

        public List<Tag> getAllTags() {

            List<Tag> allTags = new List<Tag>();

            // Open
            sqlite.Open();

            // 要下任何命令先取得該連結的執行命令物件
            sqlite_cmd = sqlite.CreateCommand();

            // 查詢剛新增的表test
            sqlite_cmd.CommandText = "SELECT * FROM TAG_TABLE";

            // 執行查詢塞入 sqlite_datareader
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

            // 一筆一筆列出查詢的資料
            while (sqlite_datareader.Read())
            {
                // Print out the content of the text field:
                String s = sqlite_datareader["tag"].ToString();
                allTags.Add(new Tag() { name = s });
                Console.WriteLine(s);
            }

            //結束
            sqlite.Close();
            return allTags;
        }

        public List<Manga> getQueryMangaRes(List<Tag>tags) {
            List<Manga> mangas = new List<Manga>();

            if (tags.Count < 1) {
                return mangas;
            }

            // Open
            sqlite.Open();

            // 要下任何命令先取得該連結的執行命令物件
            sqlite_cmd = sqlite.CreateCommand();

            // 查詢剛新增的表test
            String statment = "SELECT DISTINCT BT.title, BT.path FROM BOOK_TABLE BT INNER JOIN BOOK_TAGS_TABLE BTT ON BT.path = BTT.book_path WHERE BTT.tag IN ";
            String tagStatment = "(";
            foreach (Tag tag in tags) {
                tagStatment = tagStatment + "'" + tag.name +"',";
            }
            tagStatment = tagStatment.Remove(tagStatment.LastIndexOf(","));
            tagStatment = tagStatment + ")";
            sqlite_cmd.CommandText = statment + tagStatment;
            Console.WriteLine(sqlite_cmd.CommandText);

            // 執行查詢塞入 sqlite_datareader
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

            // 一筆一筆列出查詢的資料
            while (sqlite_datareader.Read())
            {
                // Print out the content of the text field:
                String title = sqlite_datareader["title"].ToString();
                String path = sqlite_datareader["path"].ToString();
                mangas.Add(new Manga { title = title, path = path});
                Console.WriteLine(title);
            }

            //結束
            sqlite.Close();

            return mangas;
        }

        public List<Manga> getQueryMangaAndSelection(List<Tag> tags)
        {
            List<Manga> mangas = new List<Manga>();

            if (tags.Count < 1)
            {
                return mangas;
            }

            // Open
            sqlite.Open();

            // 要下任何命令先取得該連結的執行命令物件
            sqlite_cmd = sqlite.CreateCommand();

            // 查詢剛新增的表test
            String statment = "SELECT BT.title, BT.path FROM BOOK_TABLE BT INNER JOIN BOOK_TAGS_TABLE BTT ON BT.path = BTT.book_path WHERE BTT.tag IN ";
            String tagStatment = "(";
            foreach (Tag tag in tags)
            {
                tagStatment = tagStatment + "'" + tag.name + "',";
            }
            tagStatment = tagStatment.Remove(tagStatment.LastIndexOf(","));
            tagStatment = tagStatment + ")";
            sqlite_cmd.CommandText = statment + tagStatment;
            Console.WriteLine(sqlite_cmd.CommandText);

            // 執行查詢塞入 sqlite_datareader
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            Dictionary<String, int> counter = new Dictionary<String, int>();
            // 一筆一筆列出查詢的資料
            while (sqlite_datareader.Read())
            {
                // Print out the content of the text field:
                String title = sqlite_datareader["title"].ToString();
                String path = sqlite_datareader["path"].ToString();
                var manga = new Manga { title = title, path = path };

                if (counter.Keys.Contains(path)) {
                    counter[path] = counter[path] + 1;
                } else
                {
                    counter.Add(path, 1);
                }
                if (!mangas.Contains(manga))
                {
                    mangas.Add(manga);
                }
                
            };
            List<Manga> returnMange = new List<Manga>();
            List<String> returnPath = new List<String>();

            foreach (var manga in mangas)
            {
                if (counter[manga.path] >= tags.Count) {
                    if (!returnPath.Contains(manga.path)) {
                        returnMange.Add(manga);
                        returnPath.Add(manga.path);
                    }
                    
                }
            }
            //結束
            sqlite.Close();

            return returnMange;
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
