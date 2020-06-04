using System;
using System.Collections.Generic;
using System.Text;

namespace EindOpdracht
{
    class Straat
    {
        public Graaf Graaf { get; set; }
        public int StraatID { get; set; }
        public string StraatNaam { get; set; }

        public Straat(int straatID, string straatNaam, Graaf graaf)
        {
            this.StraatID = straatID;
            this.StraatNaam = straatNaam;
            this.Graaf = graaf;
        }


    }
}
