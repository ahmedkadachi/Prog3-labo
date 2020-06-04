using System;

namespace Tool_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DataBeheer db = new DataBeheer(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=Straten;Integrated Security=True;Pooling=False");
            //db.GetGemeenteNaamID("Ronse");
            db.GetStraatByID(512);
            db.GetStraatEnGemeenteByID(512);
            db.GetStraatnaamByGemeente("Ronse");
        }
    }
}
