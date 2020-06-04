using System;
using System.Collections.Generic;
using System.Text;

namespace EindOpdracht
{
    class Provincie
    {
        public int ProvincieID { get; set; }
        public string ProvincieNaam { get; set; }
        public List<Gemeente> Gemeente = new List<Gemeente>();

        public Provincie(int provincieID, string provincieNaam, List<Gemeente> gemeente)
        {
            this.ProvincieID = provincieID;
            this.ProvincieNaam = provincieNaam;
            this.Gemeente = gemeente;
        }
    }
}
