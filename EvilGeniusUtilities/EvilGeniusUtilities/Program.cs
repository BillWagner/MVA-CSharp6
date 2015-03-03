using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

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

            DrEvil.ReplaceHenchman(new Henchman { Name = "Mini Me" });
            WriteLine(DrEvil.EvilHistory());

            var TheMaster = new EvilGenius("The Master");
            var tokens = EvilGenius.ToJson(new[] { DrEvil, TheMaster });

            var evil = EvilGenius.FromJson(tokens);

            foreach (var e in evil)
            {
                WriteLine(e);
            }

            // Yes, this is evil, but it's in a console app so 
            // I can't await:
            AsyncEvilCreation();
        }

        private static async Task AsyncEvilCreation()
        {
            try
            {
                //var nameless = new EvilGenius(default(string));

                var empty = new EvilGenius("     ");
            }
            catch (Exception e) when (logException(e))
            {

            }
            catch (ArgumentNullException n)
            {
                await LogErrorToFileAsync("Dude, can't have nameless evil genius", n);
            }
            catch (ArgumentException e3) when (!Debugger.IsAttached)
            {
                await LogErrorToFileAsync("Evil names cannot be blank", e3);
            }
        }

        public static bool logException(Exception e)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine("Error: {0}", e);
            Console.ForegroundColor = oldColor;
            return false;
        }

        public static async Task LogErrorToFileAsync(string msg, Exception e)
        {
            using (var file = File.AppendText("errors.log"))
            {
                await file.WriteAsync($"{msg}: Exception: {e}");
            }
        }
    }

}
