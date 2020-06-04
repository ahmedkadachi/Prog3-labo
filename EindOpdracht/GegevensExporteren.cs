using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EindOpdracht
{
    class GegevensExporteren
    {
        List<string> alMijnGegevens = new List<string>();
        List<String> gegevensVoorCsv = new List<string>();
        public void MaakTextBestand()
        {
            //alle gegevens inlezen en mijn arrays vullen
            GegevensInlezen l1 = new GegevensInlezen();
            l1.InlezenBestanden();

            Gegevens g1 = new Gegevens();
            g1.MaakSegmentAan();
            g1.MaakStratenAan();
            g1.MaakGemeentes();
            g1.MaakProvincies();


            //totaal aantal straten
            TotaalAantalStraten();
            //aantal straten per provincie
            Console.WriteLine("Aanmaken van raport...");
            TotaalAantalStratenPerProvincie();
            Console.WriteLine("Aanmaken van raport... OK");

            //straat info
            Console.WriteLine("Aanmaken van CSV...");
            StraatInfo();
            Console.WriteLine("Aanmaken van CSV... OK");

            //een 'mooie' rapport
            string fileName = @"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\gegevens.txt";
            File.Create(fileName).Close();
            File.WriteAllLines(fileName, alMijnGegevens);

            //een csv bestand voor tool 2
            string csvName = @"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\zelfGemaakteCSV.CSV";
            File.Create(csvName).Close();
            File.WriteAllLines(csvName, gegevensVoorCsv);
            //segmentID;beginknoop;eindknoop;lijstverticles;straatID;straatnaam;gemeenteID;gemeenteNaam;provincieID;provincieNaam
        }

        private void StraatInfo()
        {
           
            //begin en eindkopp krijgen per segment per straat per gemeente per provincie
            foreach (KeyValuePair<int, Provincie> provincie in Gegevens.alleProvincies)
            {
                alMijnGegevens.Add("Straatinfo <" + provincie.Value.ProvincieNaam + ">");
                //voor elke gemeente per provincie
                foreach (Gemeente gemeente in provincie.Value.Gemeente)
                {
                    string gemeenteInfoString = "   *  <"+gemeente.GemeenteNaam+"> : aantal straten: " + gemeente.Straat.Count + ", totale lengte: ";

                    string kortsteStraat = "";
                    string langsteStraat = "";
                    double tijdelijkeKleinsteAfstand = 1000000;
                    double tijdelijkeGrootsteAfstand = 0;
                    int korsteStraatID = 0;
                    int langsteStraatID = 0;
                    double totaleLengte = 0;

                    //voor elke straat per gemeente per provincie
                    foreach (Straat straat in gemeente.Straat)
                    {
                        //voor elke graaf per straat per gemeente per provincie
                        foreach(var graaf in straat.Graaf.Map.Values)
                        {
                            //voor elke segment per graaf per straat per gemeente per provincie
                            foreach(Segment segment in graaf)
                            {
                                //alles wat ik nodig heb voor mijn CSV
                                //segmentID;beginknoop;eindknoop;straatID;straatnaam;gemeenteID;gemeenteNaam;provincieID;provincieNaam
                                gegevensVoorCsv.Add(segment.SegmentID + ";" + segment.BeginKnoop.Punt.X + "," + segment.BeginKnoop.Punt.Y + ";"
                                    + segment.EindKnoop.Punt.X + "," + segment.EindKnoop.Punt.Y + ";" + straat.StraatID
                                    + ";" + straat.StraatNaam.Trim() + ";" + gemeente.GemeenteID + ";" + gemeente.GemeenteNaam + ";"
                                    + provincie.Key + ";" + provincie.Value.ProvincieNaam);

                                //afstanden tussen begin knoop en eindknoop berekenen en checken welke de grootste is
                                double afstand = Math.Sqrt(Math.Pow(segment.BeginKnoop.Punt.X - segment.EindKnoop.Punt.X, 2) 
                                    + Math.Pow(segment.BeginKnoop.Punt.Y - segment.EindKnoop.Punt.Y, 2));

                                totaleLengte += afstand;
                                //checken of het de kleinste is, zo ja, slaan we deze op, zo niet doen we gewoon verder
                                if(afstand< tijdelijkeKleinsteAfstand)
                                {
                                    tijdelijkeKleinsteAfstand = afstand;
                                    kortsteStraat = straat.StraatNaam.Trim();
                                    korsteStraatID = segment.SegmentID;
                                }
                                //check of het de grootste afstand is of niet
                                if(afstand> tijdelijkeGrootsteAfstand)
                                {
                                    tijdelijkeGrootsteAfstand = afstand;
                                    langsteStraat = straat.StraatNaam.Trim();
                                    langsteStraatID = segment.SegmentID;
                                }


                            }
                        }
                    }
                    gemeenteInfoString += totaleLengte;
                    alMijnGegevens.Add(gemeenteInfoString);
                    alMijnGegevens.Add("        *   Kortste straat: "+ korsteStraatID +" "+ kortsteStraat + " afstand: " + tijdelijkeKleinsteAfstand);
                    alMijnGegevens.Add("        *   langste straat: "+ langsteStraatID +" "+ langsteStraat + " afstand: " + tijdelijkeGrootsteAfstand);
                }
            }
            
        }

        private void TotaalAantalStratenPerProvincie()
        {
            alMijnGegevens.Add("Aantal straten per provincie:");
            //elke provincie afgaan
            //elke gemeente afgaan 
            //tellen
            foreach(KeyValuePair<int, Provincie> provincie in Gegevens.alleProvincies)
            {
                string provincieNaam = "*   <" + provincie.Value.ProvincieNaam + ">: ";
                int count = 0;
                foreach (Gemeente gemeente in provincie.Value.Gemeente)
                {
                    count += gemeente.Straat.Count;
                }
                provincieNaam += count;
                alMijnGegevens.Add(provincieNaam);
            }
        }

        public void TotaalAantalStraten()
        {
            int aantal = Gegevens.alleStraten.Count;
            alMijnGegevens.Add("<Totaal aantal straten> : " + aantal);
        }
    }
}
