using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS
{
    class MMG
    {
        String mmgFloderPath;
        String processFloderPath;
        String storageFloderPath;

        String storagePathAlphabet;
        String storagePathOthers;
        String storagePathLineA;
        String storagePathLineKA;
        String storagePathLineSA;
        String storagePathLineTA;
        String storagePathLineNA;
        String storagePathLineHA;
        String storagePathLineMA;
        String storagePathLineYA;


        public void initital() {
            mmgFloderPath = ConfigurationManager.AppSettings["folder_path"];
            processFloderPath = mmgFloderPath + "\\process";
            storageFloderPath = mmgFloderPath + "\\storage";

            storagePathAlphabet = storageFloderPath + "\\A_Z";
            storagePathOthers = storageFloderPath + "\\others";
            storagePathLineA = storageFloderPath + "\\line_あ";
            storagePathLineKA = storageFloderPath + "\\line_か";
            storagePathLineSA = storageFloderPath + "\\line_さ";
            storagePathLineTA = storageFloderPath + "\\line_た";
            storagePathLineNA = storageFloderPath + "\\line_な";
            storagePathLineHA = storageFloderPath + "\\line_は";
            storagePathLineMA = storageFloderPath + "\\line_ま";
            storagePathLineYA = storageFloderPath + "\\line_や";

            System.IO.Directory.CreateDirectory(processFloderPath);
            System.IO.Directory.CreateDirectory(storageFloderPath);

            System.IO.Directory.CreateDirectory(storagePathAlphabet);
            System.IO.Directory.CreateDirectory(storagePathOthers);
            System.IO.Directory.CreateDirectory(storagePathLineA);
            System.IO.Directory.CreateDirectory(storagePathLineKA);
            System.IO.Directory.CreateDirectory(storagePathLineSA);
            System.IO.Directory.CreateDirectory(storagePathLineTA);
            System.IO.Directory.CreateDirectory(storagePathLineNA);
            System.IO.Directory.CreateDirectory(storagePathLineHA);
            System.IO.Directory.CreateDirectory(storagePathLineMA);
            System.IO.Directory.CreateDirectory(storagePathLineYA);


            var files = System.IO.Directory.GetDirectories(mmgFloderPath, "*", SearchOption.TopDirectoryOnly);
            foreach (String str in files){
                Console.WriteLine(Path.GetFileName((str)));
            }
            
        }
        
        Boolean isMangaStorageFloders(String dirName) {

            if (dirName.Contains("mSF"))
            {
                return true;
            }
            else {
                return false;
            }
        }

        public void processFloder() {
            var files = System.IO.Directory.GetDirectories(processFloderPath, "*", SearchOption.TopDirectoryOnly);
            foreach (String str in files)
            {
                var startIndex = str.IndexOf("[");
                var endIndex = str.IndexOf("]");
                var authorName = str.Substring(startIndex, endIndex - startIndex + 1);
                int initial = authorName[1];

                Console.WriteLine(Path.GetFileName((str)));
                Console.WriteLine(authorName);
                Console.WriteLine(initial);

                var targetPath = storagePathOthers;

                if (initial >= 12353 & initial <= 12362)
                {
                    targetPath = storagePathLineA;
                }
                else if (initial >= 12363 & initial <= 12372)
                {
                    targetPath = storagePathLineKA;
                }
                else if (initial >= 12373 & initial <= 12382)
                {
                    targetPath = storagePathLineSA;
                }
                else if (initial >= 12383 & initial <= 12393)
                {
                    targetPath = storagePathLineTA;
                }
                else if (initial >= 12394 & initial <= 12398)
                {
                    targetPath = storagePathLineNA;
                }
                else if (initial >= 12399 & initial <= 12413)
                {
                    targetPath = storagePathLineHA;
                }
                else if (initial >= 12414 & initial <= 12418)
                {
                    targetPath = storagePathLineMA;
                }
                else if (initial >= 12419 & initial <= 12447)
                {
                    targetPath = storagePathLineYA;
                }
                else if (initial >= 65 & initial <= 90)
                {
                    targetPath = storagePathAlphabet;
                }
                else if (initial >= 97 & initial <= 122)
                {
                    targetPath = storagePathAlphabet;
                }

                Console.WriteLine(targetPath + "\\" + authorName);
                
                
                System.IO.Directory.CreateDirectory(@targetPath + "\\" + authorName);
                try
                {
                    System.IO.Directory.Move(@str, @targetPath + "\\" + authorName + "\\" + Path.GetFileName((str)));
                }
                catch {
                }
                
            }
        }

    }
}
