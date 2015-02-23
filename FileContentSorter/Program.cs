using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortFileContents
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var handler = new FileHandler())
            {
                string errMsg = handler.SortTheFile(args);
                if (errMsg.Length > 0)
                {
                    Console.WriteLine(errMsg);
                }
                else
                {
                    Console.WriteLine("File Sorted Successfully !!!");
                }
                Console.ReadLine();
            }
        }
    }
}
