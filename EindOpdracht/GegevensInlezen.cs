using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace EindOpdracht
{
    class GegevensInlezen
    {
        public void InlezenBestanden()
        {
            Console.WriteLine("Alle CSV bestanden inlezen...");
            //lijst van alle segmenten ophalen en segmenten aanmaken
            using (StreamReader r = new StreamReader(@"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\WRdata.csv"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    if (!line.Contains("W")) //checken of de text die ik inlees de eerste regel niet bevat
                    {
                        var gegevensSplitsen = line.Split(";");
                        //splitsen op de ;
                        string segmentID = gegevensSplitsen[0]; //= wegsegmentID
                        string punten = gegevensSplitsen[1];//= geo
                        string morf = gegevensSplitsen[2];// = morfologie
                        string status = gegevensSplitsen[3];// = status
                        string beginKnoop = gegevensSplitsen[4];// = beginwegknoopid
                        string eindKnoop = gegevensSplitsen[5];// = eindwegknoopid
                        string linksStraatnaamID = gegevensSplitsen[6];// = linksstraatnaamID
                        string rechtsStraatnaamID = gegevensSplitsen[7];// = RechtsstraatnaamID

                        string[] gegevensLijst = {segmentID,punten, morf,status,beginKnoop,
                            eindKnoop,linksStraatnaamID,rechtsStraatnaamID };
                        //segement aanmaken en in array steken

                        if (int.Parse(linksStraatnaamID) != -9 && int.Parse(rechtsStraatnaamID) != -9)
                        {
                            Gegevens.segmentLijst.Add(int.Parse(segmentID), gegevensLijst);
                        }

                    }
                }
            }


            //lijst met alle adressen ophalen en mijn graaf builden
            using (StreamReader s = new StreamReader(@"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\WRstraatnamen.csv"))
            {
                string straatLine;
                while ((straatLine = s.ReadLine()) != null)
                {
                    if (!straatLine.Contains("EXN"))
                    {
                        var splitStraatGegevens = straatLine.Split(";");//elke straat opsplitsen in zijn ID en zijn Naam
                        //splitStraatGegevens[0] = straatID
                        //splitStraatGegevens[1] = straatNaam
                        if (int.Parse(splitStraatGegevens[0]) != -9)
                        {
                            Gegevens.straatGegevens.Add(int.Parse(splitStraatGegevens[0]), splitStraatGegevens[1]);
                        }
                    }
                }
            }

            //gemeentenaam inlezen
            using (StreamReader r = new StreamReader(@"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\WRGemeentenaam.csv"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    if (!line.Contains("g"))
                    {
                        var data = line.Split(";");
                        //data[0] = gemeenteNaamID
                        //data[1] = gemeenteID
                        //data[2] = taalCodeGemeenteNaam
                        //data[3] = GemeenteNaam

                        if (data[2].Equals("nl"))
                        {
                            Gegevens.gemeenteNaamGegevens.Add(int.Parse(data[1]), data[3]);
                        }
                    }

                }
            }

            //de data met de link tussen mijn gemeentes en straten uitlezen
            using (StreamReader r = new StreamReader(@"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\WRGemeenteID.csv"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    if (!line.Contains("s"))
                    {
                        var data = line.Split(";");
                        //data[0] = straatNaamId
                        //data[1] = gemeenteId

                        Gegevens.gemeenteEnStraatGegevens.Add(int.Parse(data[0]), int.Parse(data[1]));
                    }
                }
            }

            //de data met al mijn provincies uithalen
            using (StreamReader r = new StreamReader(@"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\ProvincieInfo.csv"))
            {
                string line;
                while((line = r.ReadLine()) != null)
                {
                    if (!line.Contains("g"))
                    {
                        var data = line.Split(";");
                        //data[0] = gemeetneId
                        //data[1] = provincieID
                        //data[2] = taalCodeProvincieNaam
                        //data[3] = ProvincieNaam

                        if (data[2].Equals("nl"))
                        {
                            string[] temp = { data[1], data[3] };
                            Gegevens.provincieInfo.Add(int.Parse(data[0]), temp);
                        }
                    }
                }
            }

            //de data met de provincieids lezen
            using (StreamReader r = new StreamReader(@"C:\Users\Ahmed\Desktop\school\03 - Programmeren\labo\EindOpdracht\EindOpdracht\ProvincieIDsVlaanderen.csv"))
            {
                string line;
                while((line = r.ReadLine()) != null)
                {
                    var data = line.Split(",");
                    for (int i = 0; i < data.Length; i++)
                    {
                        Gegevens.provincieIDS.Add(int.Parse(data[i]));
                    }
                }
            }
            Console.WriteLine("Alle CSV bestanden inlezen... OK");
        }
    }
}
