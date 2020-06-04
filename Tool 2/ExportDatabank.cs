using System;
using System.Collections.Generic;
using System.Text;

namespace Tool_2
{
    class ExportDatabank
    {
        public void ExporteerNaarDatabank()
        {
            //alles eerst inlezen
            BestandLezen bl = new BestandLezen();
            bl.InlezenBestanden();

            //gegevens sorteren in dictionarys
            Verwerking v = new Verwerking();

            v.MaakSegmentAan();
            v.MaakStratenAan();
            v.MaakGemeentes();
            v.MaakProvincies();
            DataBeheer db = new DataBeheer(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=Straten;Integrated Security=True;Pooling=False");
            //db.VoegProvincieToe();
            //db.VoegGemeentesToe();
            //db.VoegStratenToe();
            //db.VoegSegmentenToe();
            //db.RelatieProvincieGemeente();
            //db.RelatieGemeenteStraat();
            db.RelatieStraatSegment();


        }
    }
}
