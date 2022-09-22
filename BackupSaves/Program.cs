using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace BackupSaves
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Loading saves!");

            string saveFolder = @"\BackupSaves";
            string subDir = @"\Documents";
            string username = System.Environment.GetEnvironmentVariable("USERPROFILE");

            string finishedDir = username + subDir + saveFolder;
            bool exists = Directory.Exists(finishedDir);
            if (!exists)
            {
                Console.WriteLine("Backup saves folder not found, creating...");
                Directory.CreateDirectory(finishedDir);
                Console.WriteLine("Reloading!\n");
                Main();
            }
            else
            {
                Console.WriteLine("Saves found!");
                Console.WriteLine("Load or create save [1 or 2]");
                int loadorsave = Int32.Parse(Console.ReadLine());
                if (loadorsave == 1)
                {
                    Console.WriteLine("What game do you want to load?");
                }
                else if (loadorsave == 2)
                {
                    Console.Clear();
                    Console.WriteLine("What game do you want to create a save on?");
                    string gameName = Console.ReadLine();
                    Directory.CreateDirectory(finishedDir+@"\"+gameName);
                    Console.WriteLine("Where is " + gameName + "'s saves at?");

                    var dlg = new FolderBrowserDialog();

                    var result = dlg.ShowDialog();
                    Console.WriteLine(dlg.SelectedPath);
                }
            }

            Console.ReadLine();
        }
    }
}
