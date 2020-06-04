using System;

namespace Tool_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ExportDatabank export = new ExportDatabank();
            export.ExporteerNaarDatabank();
        }
    }
}
