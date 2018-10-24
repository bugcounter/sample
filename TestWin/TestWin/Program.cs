using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWin
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                BCWin.BCFindWindow("README");
                Console.ReadKey();
            }
        }
    }
}
