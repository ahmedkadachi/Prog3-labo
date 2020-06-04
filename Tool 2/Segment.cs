using System;
using System.Collections.Generic;
using System.Text;

namespace Tool_2
{
    class Segment
    {
        public Knoop BeginKnoop { get; set; }
        public Knoop EindKnoop { get; set; }
        public int SegmentID { get; set; }
        public List<Punt> Verticles { get; set; }

        public Segment(Knoop beginKnoop, Knoop eindKnoop, int segmentID, List<Punt> verticles)
        {
            this.BeginKnoop = beginKnoop;
            this.EindKnoop = eindKnoop;
            this.SegmentID = segmentID;
            this.Verticles = verticles;
        }
    }
}
