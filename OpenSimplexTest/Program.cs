using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSimplexTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("10k^2 test:");
            Test test = new Test(10000);
            List<Performance> testPerformance = test.Run(2);

            //test.TestBuilder();

            //Console.WriteLine("1k^3 test:");
            //Test test2 = new Test(1000);
            //List<Performance> test2Performance = test2.Run(3);
#if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
#endif
        }
    }
}
