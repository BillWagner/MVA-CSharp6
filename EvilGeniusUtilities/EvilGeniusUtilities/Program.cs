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
            WriteLine(DrEvil.EvilPoints());

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

/* ==++== 
/  
/   Copyright (c) Microsoft Corporation.  All rights reserved. 
/  
/ ==--== 
*============================================================ 
* 
* Class:  FormattableString 
* 
* 
* Purpose: implementation of the FormattableString 
* class. 
* 
==========================================================*/
namespace System
{

    /// <summary> 
    /// A composite format string along with the arguments to be formatted. An instance of this 
    /// type may result from the use of the C# or VB language primitive ""interpolated string"". 
    /// </summary> 
    public abstract class FormattableString : IFormattable
    {
        /// <summary> 
        /// The composite format string. 
        /// </summary> 
        public abstract string Format { get; }

        /// <summary> 
        /// Returns an object array that contains zero or more objects to format. Clients should not 
        /// mutate the contents of the array. 
        /// </summary> 
        public abstract object[] GetArguments();

        /// <summary> 
        /// The number of arguments to be formatted. 
        /// </summary> 
        public abstract int ArgumentCount { get; }

        /// <summary> 
        /// Returns one argument to be formatted from argument position <paramref name=""index""/>. 
        /// </summary> 
        public abstract object GetArgument(int index);

        /// <summary> 
        /// Format to a string using the given culture. 
        /// </summary> 
        public abstract string ToString(IFormatProvider formatProvider);

        string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
        {
            return ToString(formatProvider);
        }

        /// <summary> 
        /// Format the given object in the invariant culture. This static method may be 
        /// imported in C# by 
        /// <code> 
        /// using static System.FormattableString; 
        /// </code>. 
        /// Within the scope 
        /// of that import directive an interpolated string may be formatted in the 
        /// invariant culture by writing, for example, 
        /// <code> 
        /// Invariant($""{{ lat = {latitude}; lon = {longitude} }}"") 
        /// </code> 
        /// </summary> 
        public static string Invariant(FormattableString formattable)
        {
            if (formattable == null)
            {
                throw new ArgumentNullException("formattable");
            }

            return formattable.ToString(Globalization.CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return ToString(Globalization.CultureInfo.CurrentCulture);
        }
    }



    namespace Runtime.CompilerServices
    {
        /// <summary> 
        /// A factory type used by compilers to create instances of the type <see cref=""FormattableString""/>. 
        /// </summary> 
        public static class FormattableStringFactory
        {
            /// <summary> 
            /// Create a <see cref=""FormattableString""/> from a composite format string and object 
            /// array containing zero or more objects to format. 
            /// </summary> 
            public static FormattableString Create(string format, params object[] arguments)
            {
                if (format == null)
                {
                    throw new ArgumentNullException("format");
                }

                if (arguments == null)
                {
                    throw new ArgumentNullException("arguments");
                }

                return new ConcreteFormattableString(format, arguments);
            }

            private sealed class ConcreteFormattableString : FormattableString
            {
                private readonly string _format;
                private readonly object[] _arguments;

                internal ConcreteFormattableString(string format, object[] arguments)
                {
                    _format = format;
                    _arguments = arguments;
                }

                public override string Format { get { return _format; } }
                public override object[] GetArguments() { return _arguments; }
                public override int ArgumentCount { get { return _arguments.Length; } }
                public override object GetArgument(int index) { return _arguments[index]; }
                public override string ToString(IFormatProvider formatProvider) { return string.Format(formatProvider, Format, _arguments); }
            }
        }
    }
}

