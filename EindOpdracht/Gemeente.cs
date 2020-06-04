using System;
using System.Collections.Generic;
using System.Text;

namespace EindOpdracht
{
    class Gemeente
    {
        public int GemeenteID { get; set; }
        public string GemeenteNaam { get; set; }
        public List<Straat> Straat = new List<Straat>();

        public Gemeente( int gemeenteID, string gemeenteNaam, List<Straat> straat)
        {
            this.GemeenteID = gemeenteID;
            this.GemeenteNaam = gemeenteNaam;
            this.Straat = straat;
        }
    }
}
