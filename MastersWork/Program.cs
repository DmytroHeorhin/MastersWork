using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MastersWork.Core;

namespace MastersWork
{
    class Program
    {
        static void Main(string[] args)
        {      
            var bitsPerSecond = 1000000u;
            var bitsPerBlock = 1000u;
            byte power = 5;
            var researcher = new Researcher(bitsPerSecond, bitsPerBlock, power);
            var sw = Stopwatch.StartNew();
            var result = researcher.PerformResearch(1, 100);
            sw.Stop();



            Console.WriteLine("Bit error probability: 10^-" + power);
            Console.WriteLine();

            var i = 0;
            foreach (var r in result)
            {
                i++;
                Console.WriteLine("***********    " + i + "   ************");
                Console.WriteLine("Bit error ratio: " + r.BitErrorRatio);
                Console.WriteLine("Errored seconds: " + r.ErroredSeconds);
                Console.WriteLine("Errored seconds ratio: " + r.ErroredSecondsRatio);
                Console.WriteLine("Severely errored seconds: " + r.SeverelyErroredSeconds);
                Console.WriteLine("Severely errored seconds ratio: " + r.SeverelyErroredSecondsRatio);
                Console.WriteLine();
            }

            Console.WriteLine(sw.Elapsed);
        }
    }
}
