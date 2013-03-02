using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BookIndexing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("input.txt"))
            {
                var f = File.ReadAllText("input.txt");
                var indexer = new Indexer();
                var result = indexer.Process(f);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Put all your text in input.txt file in the same directory as this application and run again.");
            }

        }
    }
}
