using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\windows";

            ShowLargeFilesWhithoutLinq(path);
            Console.ReadKey();
            ShowLargeFilesWhithLinq(path);
        }

        private static void ShowLargeFilesWhithLinq(string path)
        {
            //var query = from file in new DirectoryInfo(path).GetFiles()
            //            orderby file.Length descending
            //            select file;

            var query = new DirectoryInfo().GetFiles()
                .OrderBy(f => f.Length)
                .Take(5);

            foreach(var file in query)
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,-10:N0}");
            }
        }

        private static void ShowLargeFilesWhithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            foreach(var file in files)
            {
                Console.WriteLine($"{file.Name} : {file.Length}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
