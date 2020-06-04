using System;
using System.Collections.Generic;
using System.Text;

namespace EindOpdracht
{
    class Knoop
    {
        public int KnoopID { get; set; }
        public Punt Punt { get; set; }
        public Knoop(int knoopID, Punt punt)
        {
            this.KnoopID = knoopID;
            this.Punt = punt;
        }
    }
}
