using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintumProjektverwaltung.Models
{
   public class printumBestellPositionen
    {

        public int BestellPos_ID { get; set; }
        public int Projektnummer { get; set; }
        public string BestellnungIDTest { get; set; }
        public Nullable<int> Pos { get; set; }
        public Nullable<int> Stueck { get; set; }
        public string Artikelbezeichnung { get; set; }
        public Nullable<System.DateTime> Liefertermin { get; set; }
        public Nullable<double> Einzelpreis { get; set; }
        public Nullable<double> Gesammtpreis { get; set; }
    }
}
