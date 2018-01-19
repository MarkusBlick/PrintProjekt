using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintumProjektverwaltung.Models
{
    class printumDatei
    {
        public string Name { get; set; }
        public string Pfad { get; set; }
        public string Typ { get; set; }
        public string Schlagworte { get; set; }
        public System.DateTime erstellt { get; set; }
        public Nullable<System.DateTime> geaendert { get; set; }
        public string erstelltVon { get; set; }
        public string geaendetVon { get; set; }
        public int Projektnummer { get; set; }

        internal static List<printumDatei> getDateienBy(int projektnummer)
        {
            throw new NotImplementedException();
        }
    }
}
