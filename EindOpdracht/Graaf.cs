using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EindOpdracht
{
    class Graaf
    {
        
        public int GraafID { get; set; }
        public Dictionary<Knoop, List<Segment>> Map = new Dictionary<Knoop, List<Segment>>();

        public Graaf(int graafID)
        {
            this.GraafID = graafID;
        }
        public Graaf BuildGraaf(int id, List<Segment> segment)
        {
            Graaf graaf = new Graaf(id);
            if (segment != null)
            {
                for (int i = 0; i < segment.Count; i++)
                {
                    //checken voor de beginknoop
                    //kijken of hij er al in zit, zo niet toevoegen
                    if (graaf.Map.Keys.AsEnumerable().Where(y => y.KnoopID == segment[i].BeginKnoop.KnoopID).Count() == 1)
                    {
                        //toevoegen bij een bestaande key
                        graaf.Map.Where(y => y.Key.KnoopID == segment[i].BeginKnoop.KnoopID).FirstOrDefault().Value.Add(segment[i]);
                    }
                    else
                    {
                        //nieuwe aanmaken
                        graaf.Map.Add(segment[i].BeginKnoop, new List<Segment>() { segment[i] });
                    }


                    //checken voor de eindknoop
                    //kijken of hij er al in zit, zo niet toevoegen
                    if (graaf.Map.Keys.AsEnumerable().Where(y => y.KnoopID == segment[i].EindKnoop.KnoopID).Count() == 1)
                    {
                        //toevoegen bij een bestaande key
                        graaf.Map.Where(y => y.Key.KnoopID == segment[i].EindKnoop.KnoopID).FirstOrDefault().Value.Add(segment[i]);
                    }
                    else
                    {
                        //nieuwe aanmaken
                        graaf.Map.Add(segment[i].EindKnoop, new List<Segment>() { segment[i] });
                    }
                }
                
            }
            return graaf;
        }

        //public List<Knoop> getKnopen()
        //{
        //    List<Knoop> knooplijst = new List<Knoop>();
        //    knooplijst.Add(Map[]);
        //    return knooplijst;
        //}
    }
}
