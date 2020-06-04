using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Tool_2
{
    class Verwerking
    {
        //data vanuit mijn reader
        public static Dictionary<int, string[]> segmentLijst = new Dictionary<int, string[]>();
        public static Dictionary<int, string> straatGegevens = new Dictionary<int, string>();
        public static Dictionary<int, string> gemeenteNaamGegevens = new Dictionary<int, string>();
        public static Dictionary<int, int> gemeenteEnStraatGegevens = new Dictionary<int, int>();
        public static Dictionary<int, string[]> provincieInfo = new Dictionary<int, string[]>();
        public static List<int> provincieIDS = new List<int>();
        public static Dictionary<int, int> eigenCSV = new Dictionary<int, int>();


        //mijn opgevulde en gesorteede segmentenDictionary
        public static Dictionary<int, List<Segment>> alleSegmenten = new Dictionary<int, List<Segment>>();
        public static Dictionary<int, Graaf> alleGrafen = new Dictionary<int, Graaf>();
        public static Dictionary<int, Straat> alleStraten = new Dictionary<int, Straat>();
        public static Dictionary<int, Gemeente> alleGemeentes = new Dictionary<int, Gemeente>();
        public static Dictionary<int, Provincie> alleProvincies = new Dictionary<int, Provincie>();

        public void MaakProvincies()
        {
            Console.WriteLine("Aanmaken van provincies...");
            //alle provincieID doorlopen
            //checken als prvinfo value 0 = provincie id  =========== privID dan nieuwe prov aanmaken en alle gemeentes hiervan ophalen
            foreach (int data in provincieIDS)
            {
                List<Gemeente> gemeente = new List<Gemeente>();
                int provincieID = 0;
                string provincieNaam = "";
                foreach (KeyValuePair<int, string[]> provs in provincieInfo)
                {
                    // checken of dat de data gelijk is aan prov.value[0] == prov id
                    if (data == int.Parse(provs.Value[0]))
                    {
                        if (alleGemeentes.ContainsKey(provs.Key))
                        {
                            gemeente.Add(alleGemeentes[provs.Key]);
                            provincieID = int.Parse(provs.Value[0]);
                            provincieNaam = provs.Value[1];
                        }
                    }
                }
                Provincie prov = new Provincie(provincieID, provincieNaam, gemeente);
                alleProvincies.Add(provincieID, prov);
            }
            Console.WriteLine("Aanmaken van provincies... OK");
        }
        public void MaakGemeentes()
        {
            Console.WriteLine("Aanmaken van gemeentes...");
            foreach (KeyValuePair<int, string> gemeente in gemeenteNaamGegevens)
            {
                List<Straat> straten = new List<Straat>();
                //als datakEY overeenkomt met de value 'link tussen 2 doc' dan nemen we de key daarven en slaan we deze in een list op en op het einde list aanmaken
                foreach (KeyValuePair<int, int> link in gemeenteEnStraatGegevens)
                {
                    if (gemeente.Key == link.Value)
                    {
                        //controle om te zien of het wel bestaat
                        if (alleStraten.ContainsKey(link.Key))
                        {
                            straten.Add(alleStraten[link.Key]);
                        }
                    }

                }
                //maak nieuwe gemeente 
                Gemeente gem = new Gemeente(gemeente.Key, gemeente.Value, straten);
                alleGemeentes.Add(gemeente.Key, gem);
            }
            Console.WriteLine("Aanmaken van gemeentes... OK");

        }

        public void MaakStratenAan()
        {
            Console.WriteLine("Aanmaken van straten...");
            foreach (KeyValuePair<int, List<Segment>> data in alleSegmenten)
            {
                int straatID = data.Key;
                Graaf graaf = new Graaf(straatID);
                Straat straat = new Straat(straatID, straatGegevens[data.Key], graaf.BuildGraaf(straatID, data.Value));
                alleStraten.Add(straatID, straat);
            }
            Console.WriteLine("Aanmaken van straten... OK");
        }


        public void MaakSegmentAan()
        {
            Console.WriteLine("Aanmaken van segmenten...");
            List<Punt> allePunten = new List<Punt>();
            //om mijn segment te krijgen moet ik knopen aanmaken, een segment bestaat uit een begin en eindknoop
            //voor een begin knoop moet ik mijn aller eerste punt(x,y) van mijn regel nemen
            //voor mijn eind knoop moet ik mijn aller laatste punt(x,y) van mijn regel nemen
            Punt beginPunt = null;
            Punt eindPunt = null;
            Knoop beginKnoop;
            Knoop eindKnoop;
            foreach (KeyValuePair<int, string[]> data in segmentLijst)
            {
                //data.Value[0] = wegsegmentID
                //data.Value[1] = geo
                //data.Value[2] = morfologie
                //data.Value[3] = status
                //data.Value[4] = beginwegknoopid
                //data.Value[5] = eindwegknoopid
                //data.Value[6] = linksstraatnaamID
                //data.Value[7] = RechtsstraatnaamID

                var GeoLinestring = data.Value[1].Split("("); // splitsen op geo en linsestring wegkrijgen
                var GeoBuitenHaakje = GeoLinestring[1].Split(")"); // splitsen op geo en de buiten ) wegrkijgen
                var GeoKoppels = GeoBuitenHaakje[0].Split(", "); // splitsen op geo per koppels dus [0] = x1,y1, [1] = x2,y2;
                for (int i = 0; i < GeoKoppels.Length; i++) // splitsen op geo per X en y coordinaat dus [0] = x, [1] = Y
                {
                    var GeoPunten = GeoKoppels[i].Split(" "); // splitsen op geo per X en y coordinaat dus [0] = x, [1] = Y
                    allePunten.Add(new Punt(double.Parse(GeoPunten[0]), double.Parse(GeoPunten[1]))); //List met punten opvullen

                    //voor een begin knoop te krijgen moet ik mijn aller EERSTE begin x,y coordinaten nemen
                    if (i == 0)
                        beginPunt = new Punt(double.Parse(GeoPunten[0]), double.Parse(GeoPunten[1]));
                    //voor een begin knoop te krijgen moet ik mijn aller LAATSTE begin x,y coordinaten nemen
                    if (i == GeoKoppels.Length - 1)
                        eindPunt = new Punt(double.Parse(GeoPunten[0]), double.Parse(GeoPunten[1]));
                }

                beginKnoop = new Knoop(int.Parse(data.Value[4]), beginPunt);
                eindKnoop = new Knoop(int.Parse(data.Value[5]), eindPunt);
                Segment segment = new Segment(beginKnoop, eindKnoop, int.Parse(data.Value[0]), allePunten);

                //checken of mijn linkerstraatID en rechterStraatID gelijk zijn
                //zo wel dan check ik of de Key ID al bestaat zo ja dan voeg ik hem toe zo niet dan maak ik een nieuwe aan
                //zo niet doe ik het zelfde maar voor elke ID dus zowel links als rechts
                int linkerID = int.Parse(data.Value[6]);
                int rechterID = int.Parse(data.Value[7]);
                if (linkerID == rechterID)
                {
                    if (alleSegmenten.ContainsKey(linkerID))
                    {
                        alleSegmenten[linkerID].Add(segment);
                    }
                    else
                    {
                        List<Segment> list = new List<Segment>();
                        list.Add(segment);
                        alleSegmenten.Add(linkerID, list);
                    }
                }
                else
                {
                    List<Segment> list = new List<Segment>();
                    list.Add(segment);

                    if (alleSegmenten.ContainsKey(linkerID))
                    {
                        alleSegmenten[linkerID].Add(segment);
                    }
                    else
                    {
                        alleSegmenten.Add(linkerID, list);
                    }

                    if (alleSegmenten.ContainsKey(rechterID))
                    {
                        alleSegmenten[rechterID].Add(segment);
                    }
                    else
                    {
                        alleSegmenten.Add(rechterID, list);
                    }
                }
            }
            Console.WriteLine("Aanmaken van segmenten... OK");
        }
    }
}
