using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilGeniusUtilities
{
    class Program
    {
        static void Main(string[] args)
        {
            var DrEvil = new EvilGenius
            {
                Name = "Dr. Evil"
            };
            WriteLine(DrEvil);

            DrEvil.Minion = new Henchman { Name = "Scott Evil" };
            WriteLine(DrEvil);
        }
    }
}
