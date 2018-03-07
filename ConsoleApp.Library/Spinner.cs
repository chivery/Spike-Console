//var spinner = new Spinner();
//spinner.Start();
//int x = CreateDataFile();
//spinner.Stop();
//Console.Write($"File created {x} {spinner.TimeElapsed()} sec.");
//
//https://stackoverflow.com/questions/36911609/stop-task-when-task-run

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp.Library
{
    public class Spinner : IDisposable
    {
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token=default(CancellationToken);

        private const int timeout = 200;
        private const int frameLength = 6;
        private int index=0;

        public DateTime StartTime { get; set; }
        public string[] Frames { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }

        public bool Cancel { get; set; }

        public Spinner()
        {
            Initialize();
        }

        private void Initialize()
        {

            this.X = Console.CursorLeft;
            this.Y = Console.CursorTop;

            this.Left = (Console.WindowWidth  - frameLength) / 2;
            this.Top = Console.WindowHeight / 4;

            // Hide cursor to improve the visuals (note: we should re-enable at some point)
            Console.CursorVisible = false;

            Console.SetCursorPosition(Left, Top);

           // Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green ;

            //Frames = new[]
            //               {
            //                    '|',
            //                    '/',
            //                    '-',
            //                    '\\'
            //                };

            Frames = new[]
                         {
                             " *    ",
                             " **  ",
                             " ***  ",
                             " **** "
                            };
        }

        public long TimeElapsed()
        {
            return (long)(DateTime.Now -StartTime ).TotalSeconds;
        }

        public void Stop()
        {
            this.Cancel = true;

            if (source != null)
            {
                source.Cancel();
            }

            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = true;
            Console.SetCursorPosition(X, Y);
        }

        public  void Start()
        {
            //https://stackoverflow.com/questions/36911609/stop-task-when-task-run
            token = source.Token;
            var t = Task.Run(() => Spin(), token);
        }

        public void Spin()
        {
            this.StartTime = DateTime.Now;

            while (! Cancel)
            {
                NextFrame();
                Thread.Sleep(timeout);
            }
        }

        public void NextFrame()
        {
            //Console.Write($"{TimeElapsed()} {Frames[_frame]} ");
            Console.Write($" {Frames[index]} ");
            index = (index >= (Frames.Length-1)) ? 0 : ++index;
            Console.SetCursorPosition(Left, Top);
        }


        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        //static void RunSpinnerTask()
        //{
        //    CancellationTokenSource source = new CancellationTokenSource();
        //    CancellationToken token;
        //    token = source.Token;

        //    var spinner = new Spinner();

        //    var t = Task.Run(() => spinner.Spin(), token);

        //    Thread.Sleep(3000);

        //    if (source != null)
        //    {
        //        source.Cancel();
        //    }
        //}

    }
}
