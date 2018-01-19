using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintumProjektverwaltung.Models
{
    class printumOrdner
    {
        public int Projektnummer { get; set; }
        public string Name { get; set; }
        public string Pfad { get; set; }
        public string Rootname { get; set; }

        internal static List<printumOrdner> getOrdnerBy(int projektnummer)
        {
            throw new NotImplementedException();
        }
    }
}
