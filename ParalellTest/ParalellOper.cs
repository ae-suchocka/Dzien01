using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParalellTest
{
    internal class ParallelOper
    {   Random random = new Random();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public void LoopParallelCancel()
        {
            new Thread(() =>
            {
                Thread.Sleep(3_000);
                cancellationTokenSource.Cancel();

            }).Start();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 5;
            parallelOptions.CancellationToken = cancellationTokenSource.Token;
            try
            {
                Parallel.For(0, 10, parallelOptions, i =>
                {
                    long total = LongOperation();
                    Console.WriteLine($"{i} - {total}");
                    Thread.Sleep(1000);

                });
            }
            catch (OperationCanceledException exc)
            {

                Console.WriteLine( exc.Message);
            }
            

            sw.Stop();
            Console.WriteLine($"LoopWithParallel - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopNoParallel()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                long total = LongOperation();
                Console.WriteLine( $"{i} - {total}");
            }
            sw.Stop();
            Console.WriteLine(  $"LoopNoParallel - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithParallelBreakStop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 5;
            Parallel.For(0, 10, parallelOptions, (i, loopState) =>
            {
                long total = LongOperation();
                if (i >= 5)
                {
                    loopState.Stop();
                }
                Console.WriteLine($"{i} - {total}");

            });

            sw.Stop();
            Console.WriteLine($"LoopWithParallelBreakStop - {sw.Elapsed.TotalMilliseconds}");
        }
        public void LoopWithParallel()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 5;
            Parallel.For(0, 10,parallelOptions ,i =>
            {
                long total = LongOperation();
                Console.WriteLine($"{i} - {total}");

            });

            sw.Stop();
            Console.WriteLine($"LoopWithParallel - {sw.Elapsed.TotalMilliseconds}");
        }

        public void LoopWithParallelForeach()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 5;
            List<int> integersList = Enumerable.Range(0, 10).ToList();
            Parallel.ForEach(integersList, parallelOptions, i =>
            {
                long total = LongOperation();
                Console.WriteLine($"{i} - {total}");

            });

            sw.Stop();
            Console.WriteLine($"LoopWithParallelForeach - {sw.Elapsed.TotalMilliseconds}");
        }

        public void ParallelInvoke()
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 5,
            };
            Parallel.Invoke(options, 
                () => TestTask(1),
                () => TestTask(2),
                () => TestTask(3),
                () => TestTask(4)
                );

        }
        private long LongOperation()
        {
            long total = 0;
            for (int i = 0; i < 1_000_000; i++)
            {
                total += i;
            }
            return total;
        }    

        private void TestTask(int nr)
        {
            Console.WriteLine(  $"Start zadania [{nr}]");
            Thread.Sleep( random.Next(500, 1200));
            Console.WriteLine($"Koniec zadania [{nr}]");

        }
    }
}