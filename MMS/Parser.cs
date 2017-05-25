using NSoup;
using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS
{
    class Parser
    {

        public void testParsor() {

            IConnection connection = NSoupClient.Connect("http://www.books.com.tw/");
            connection.Timeout(30000);
            Document document = connection.Get();
            var title = document.Title;
            var raw =  document.Html(); 

            var thumbs = document.Select("meta");

            foreach (Element element in thumbs) {
                Console.WriteLine(element.OwnText());
            }

        }

    }
}
