using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelLibrarySample
{
    class Program
    {
        static void Main(string[] args)
        {

            //var t1 = new Task(() => DoSomethingImportantWork(1, 5000));
            //t1.Start();
            //var t2 = new Task(() => DoSomethingImportantWork(2, 3000));
            //t2.Start();
            //var t3 = new Task(() => DoSomethingImportantWork(3, 1500));
            //t3.Start();

            //var t1 = Task.Factory.StartNew(() => DoSomethingImportantWork(1, 5000));
            //var t2 = Task.Factory.StartNew(() => DoSomethingImportantWork(2, 3000));
            //var t3 = Task.Factory.StartNew(() => DoSomethingImportantWork(3, 1500));

            // chaining
            //var t1 = Task.Factory.StartNew(() => DoSomethingImportantWork(1, 5000)).ContinueWith((t) => DoSomeOtherImportantWork(1, 1000));
            //var t2 = Task.Factory.StartNew(() => DoSomethingImportantWork(2, 3000)).ContinueWith((t) => DoSomeOtherImportantWork(2, 2000));
            //var t3 = Task.Factory.StartNew(() => DoSomethingImportantWork(3, 1500)).ContinueWith((t) => DoSomeOtherImportantWork(3, 5000));

            //Task.WhenAll(t1, t2, t3);
            //Task.WaitAll(t1, t2, t3);

            //var taskList = new List<Task> { t1, t2, t3 };
            //Task.WaitAll(taskList.ToArray());

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("Do some other work again.");
            //    Thread.Sleep(250);
            //    Console.WriteLine($"i = {i}");
            //}

            //var taskList = new List<Task> { t1, t2, t3 };
            //Task.WaitAll(taskList.ToArray());

            //var intList = Enumerable.Range(1, 15);
            //Parallel.ForEach(intList, i => Console.WriteLine(i));

            //Parallel.For(0, 100, i =>
            //{
            //    Console.WriteLine(i);
            //});

            var cts = new CancellationTokenSource();

            try
            {
                var t1 =
                    Task.Factory.StartNew(() => DoSomethingImportantWork(1, 3000, cts.Token))
                    .ContinueWith(prevTask => DoSomeOtherImportantWork(1, 5000, cts.Token));
                Thread.Sleep(800);
                cts.Cancel();
                Console.WriteLine("Hello");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("press any key to quit.");
            Console.ReadKey();
        }

        static void DoSomethingImportantWork(int id, int sleepTime, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation requested.");
                ct.ThrowIfCancellationRequested();
            }
            Console.WriteLine($"task {id} is beginning.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"task {id} has completed.");
        }

        static void DoSomeOtherImportantWork(int id, int sleepTime, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation requested.");
                ct.ThrowIfCancellationRequested();
            }
            Console.WriteLine($"task {id} is beginning more work.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"task {id} has completed more work.");
        }

    }
}
