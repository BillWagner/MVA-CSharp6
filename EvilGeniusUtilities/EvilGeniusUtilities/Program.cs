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
            var DrEvil = new EvilGenius("Dr. Evil");
            WriteLine(DrEvil);

            DrEvil.ReplaceHenchman(new Henchman { Name = "Scott Evil" });
            WriteLine(DrEvil);

            var TheMaster = new EvilGenius("The Master");
            var tokens = EvilGenius.ToJson(new[] { DrEvil, TheMaster });

            var evil = EvilGenius.FromJson(tokens);
            foreach (var e in evil)
            {
                WriteLine(e);
                WriteLine(e.EvilHistory());
            }
        }
    }
}
