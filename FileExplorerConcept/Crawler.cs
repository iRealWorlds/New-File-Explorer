using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorerConcept
{
    class Crawler
    {
        private string _CurrentPath;
        public string CurrentPath {
            get
            {
                return _CurrentPath;
            }
            set
            {
                _CurrentPath = value;
                CurrentDirectory = new DirectoryInfo(value);
            }
        }
        public DirectoryInfo CurrentDirectory { get; set; }

        public Crawler(string path)
        {
            CurrentPath = path;
        }

        public void EnterDirectory(string dirname)
        {
            CurrentPath = Path.Combine(CurrentPath, dirname);
            if (!CurrentDirectory.Exists)
            {
                ExitDirectory();
                throw new DirectoryNotFoundException();
            }
        }

        public void ExitDirectory()
        {
            CurrentPath = CurrentDirectory.Parent.FullName;
        }

        public void PrintDirectory()
        {
            Console.Clear();
            Console.WriteLine(CurrentPath);
            Program.ColorMessage($"Directory Listing for {CurrentDirectory.Name}:", ConsoleColor.Green);
            foreach (var entry in CurrentDirectory.GetDirectories())
                Console.WriteLine($"-- {entry.Name}");
            foreach (var entry in CurrentDirectory.GetFiles())
                Console.WriteLine($"- {entry.Name}");
            Console.WriteLine();
            Program.ColorMessage("| - Type 'dir directory_name' to enter it.", ConsoleColor.Yellow);
            Program.ColorMessage("| - Type 'start file_name' to run a file.", ConsoleColor.Yellow);
            Program.ColorMessage("| - Type 'disk DISK (i.e C, D)' to run a file.", ConsoleColor.Yellow);
            Program.ColorMessage("| - Use 'back' to go to parent.", ConsoleColor.Yellow);
            Program.ColorMessage("| - Use 'exit' to exit program.", ConsoleColor.Yellow);
        }

        public void StartProgram(string name)
        {
            var process = new Process();
            process.StartInfo.FileName = Path.Combine(CurrentPath, name);
            process.Start();
        }

        public void Disk(char disk)
        {
            CurrentPath = $@"{disk}:\";
        }
    }
}
