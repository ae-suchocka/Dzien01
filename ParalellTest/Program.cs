using ParalellTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParallelOper oper = new ParallelOper();
            //oper.LoopNoParallel();
            //oper.LoopWithParallel();
            //oper.LoopWithParallelForeach();
            // oper.LoopWithParallelBreakStop();
            //oper.ParallelInvoke();
            oper.LoopParallelCancel();

            Console.ReadKey();
        }
    }
}
