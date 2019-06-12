using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)

        {
            Console.WriteLine("Hi!! This is a file/folder monitor. You have to provide a path you would like to monitor");
            Console.WriteLine("----------------------------------------------------------------------------------------");

            //Console.WriteLine("Press 'Y' to get started");

            // while (Console.Read() != 'Y') ;

            Console.WriteLine("Enter the path which you would like to monitor");

            Console.WriteLine("----------------------------------------------------------------------------------------");

            string input = Console.ReadLine();

            Run(input);
        }

        [PermissionSet(SecurityAction.Demand,Name ="FullTrust")]
        private static void Run(string input)
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = input;

                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                    | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                watcher.Filter = "";

                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;

                Console.WriteLine($"File watcher has started to monitor - {input}");

                Console.WriteLine("Press 'q' to quit");

                Console.WriteLine("----------------------------------------------------------------------------------------");

                while (Console.Read() != 'q') ;
            }
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }
    }
}
