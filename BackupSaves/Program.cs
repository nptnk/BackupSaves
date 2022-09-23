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

            string configFolder = @"\Config";
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

                    string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                    Directory.CreateDirectory(currentDir + configFolder);
                    Directory.CreateDirectory(finishedDir+@"\"+gameName);
                    File.Create(currentDir + configFolder + @"\" + gameName + ".cfg");
                    Console.WriteLine("Where is " + gameName + "'s saves at?");

                    var dlg = new FolderBrowserDialog();

                    var result = dlg.ShowDialog();
                    Console.WriteLine(dlg.SelectedPath);
                    Directory.CreateDirectory(finishedDir + @"\" + gameName + @"\" + gameName + " Saves");
                    CloneDirectory(dlg.SelectedPath, finishedDir + @"\" + gameName + @"\" + gameName + " Saves");
                    Console.WriteLine("Created backup save for " + gameName);
                }
            }

            Console.ReadLine();
            Main();
        }

        private static void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }
        }
    }
}
