using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorerConcept
{
    class Program
    {
        public static Crawler Crawler { get; set; }

        public static void ColorMessage(string message, ConsoleColor color)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = original;
        }

        static void Main(string[] args)
        {
            Crawler = new Crawler(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            Crawler.PrintDirectory();

            while (true)
            {
                var cmd = Console.ReadLine();
                if (cmd.Equals("exit"))
                {
                    break;
                }
                else if (cmd.Equals("refresh"))
                {
                    Crawler.PrintDirectory();
                }
                else if(cmd.Equals("back"))
                {
                    try
                    {
                        Crawler.ExitDirectory();
                        Crawler.PrintDirectory();
                    }
                    catch(Exception)
                    {
                        ColorMessage("Folder does not exist", ConsoleColor.Red);
                    }
                }
                else if(cmd.Length > 4 && cmd.Substring(0, 4).Equals("dir "))
                {
                    try
                    {
                        Crawler.EnterDirectory(cmd.Substring(4));
                        Crawler.PrintDirectory();
                    }
                    catch (DirectoryNotFoundException)
                    {
                        ColorMessage("Folder does not exist.", ConsoleColor.Red);
                    }
                }
                else if (cmd.Length > 6 && cmd.Substring(0, 6).Equals("start "))
                {
                    var file = cmd.Substring(6);
                    ColorMessage($"Starting {file}...", ConsoleColor.Green);
                    try
                    {
                        Crawler.StartProgram(file);
                    }
                    catch (Win32Exception)
                    {
                        ColorMessage("File does not exist", ConsoleColor.Red);
                    }
                }
                else if (cmd.Length == 6 && cmd.Substring(0, 5).Equals("disk "))
                {
                    var disk = cmd.Substring(5).ToUpper();
                    ColorMessage($@"Moving to disk {disk}:\...", ConsoleColor.Green);
                    Crawler.Disk(Convert.ToChar(disk));
                    Crawler.PrintDirectory();
                    ColorMessage($@"Moved to disk {disk}:\.", ConsoleColor.Green);
                }
                else
                {
                    Crawler.PrintDirectory();
                }
            }
        }
    }
}
