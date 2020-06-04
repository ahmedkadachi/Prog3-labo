using System;
using System.Collections.Generic;
using System.Text;

namespace Tool_2
{
    class Gemeente
    {
        public int IDGemeente { get; set; }
        public string GemeenteNaam { get; set; }
        public List<Straat> Straat = new List<Straat>();

        public Gemeente(int iDGemeente, string gemeenteNaam, List<Straat> straat)
        {
            this.IDGemeente = iDGemeente;
            this.GemeenteNaam = gemeenteNaam;
            this.Straat = straat;
        }
    }
}
