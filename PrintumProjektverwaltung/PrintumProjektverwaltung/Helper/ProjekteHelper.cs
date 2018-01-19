using System;
using System.Linq;
using PrintumProjektverwaltung.Models;
using PrintumProjektverwaltung.DAL;
using System.Collections.Generic;

namespace PrintumProjektverwaltung.Helper
{
    internal class ProjekteHelper
    {
        internal static void deleteProjekt(printumProjekt dasP)
        {
            using (PrintumProjekteEntities db = new PrintumProjekteEntities())
            {
                List<Bestellungen> dieBestellungen = new List<Bestellungen>();
                List<BestellungPositionen> diePositionen = new List<BestellungPositionen>();

                var q1 = from b in db.Bestellungen
                         where b.Projektnummer == dasP.Projektnummer
                         select b;
                dieBestellungen = q1.ToList();

                foreach (var bestell in dieBestellungen)
                {
                    var q2 = from p in db.BestellungPositionen
                             where p.BestellnungIDTest == bestell.BestellungIDText
                             select p;
                    diePositionen.AddRange(q2.AsEnumerable());
                }

                try
                {
                    db.BestellungPositionen.RemoveRange(diePositionen.AsEnumerable());


                    db.Bestellungen.RemoveRange(dieBestellungen.AsEnumerable());

                    Helper.folderHelper.deleteFolder(dasP.RootOrdner);

                    Projekte p = db.Projekte.Where(x => x.Projektnummer == dasP.Projektnummer).FirstOrDefault();
                    db.Projekte.Remove(p);


                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteDebugLog(ex.ToString());
                }
            }
        }
    }
}