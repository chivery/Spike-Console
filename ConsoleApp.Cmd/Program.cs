using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Library;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp.Cmd
{
    class Program
    {

        

        static void Main(string[] args)
        {
            // RunSpinnerTask();

            var spinner = new Spinner();
            spinner.Start();
             int x = CreateDataFile();
            spinner.Stop();

            Console.Write($"File created {x} {spinner.TimeElapsed()} sec.");

            Console.ReadKey();
        }



    


        static int CreateDataFile()
        {
            int count = 500000;
            string path = $"{AppDomain.CurrentDomain.BaseDirectory} data.txt";
            string s = null;
            for (int i = 0; i < count; i++)
            {
                s += "a";
            }
            File.WriteAllText(path, s);

            //Console.WriteLine("File created successfully.");
            return count;
        }


    }
}
