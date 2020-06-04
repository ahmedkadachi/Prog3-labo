using System;
using System.Collections.Generic;
using System.Text;

namespace Tool_2
{
    class Provincie
    {
        public int IDProvincie { get; set; }
        public string ProvincieNaam { get; set; }
        public List<Gemeente> Gemeente = new List<Gemeente>();

        public Provincie(int iDProvincie, string provincieNaam, List<Gemeente> gemeente)
        {
            this.IDProvincie = iDProvincie;
            this.ProvincieNaam = provincieNaam;
            this.Gemeente = gemeente;
        }
    }
}
