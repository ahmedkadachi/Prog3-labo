using System;

namespace EindOpdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            GegevensExporteren ex = new GegevensExporteren();
            ex.MaakTextBestand();
        }
    }
}
