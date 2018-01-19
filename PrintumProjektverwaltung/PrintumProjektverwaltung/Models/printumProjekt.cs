using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintumProjektverwaltung.DAL;

namespace PrintumProjektverwaltung.Models
{
    class printumProjekt
    {
        public int Projektnummer { get; set; }       
        public string Projektname { get; set; }
        public string ProjektnummerText { get; set; }
        public string RootOrdner { get; set; }
        public string OrdnersturkturJSON { get; set; }
        public string BestellungenJSON { get; set; }
        public string MailsJSON { get; set; }
        public List<printumOrdner> Unterordner { get; set; }
        public List<printumDatei> Dateien { get; set; }
        public Nullable<System.DateTime> Projektbeginn { get; set; }
        public Nullable<System.DateTime> Produktionsbeginn { get; set; }
        public Nullable<System.DateTime> Inbetriebname { get; set; }
        public bool aktiv { get; set; }

        private Projekte _projekte = new Projekte();

        internal void saveInDB()
        {
            using (PrintumProjekteEntities db = new PrintumProjekteEntities())
            {
                var ergebniss = db.Projekte.Find(Projektnummer);

                if (ergebniss == null)
                {
                    try
                    {


                        _projekte.Projektnummer = Projektnummer;
                        _projekte.Projektname = Projektname;
                        _projekte.RootOrdner = RootOrdner;
                        _projekte.OrdnersturkturJSON = OrdnersturkturJSON;
                        _projekte.MailsJSON = MailsJSON;

                        db.Projekte.Add(_projekte);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Helper.LogHelper.WriteDebugLog(ex.ToString());
                    }


                    if (Unterordner.Count > 0)
                    {
                        foreach (var ordner in Unterordner)
                        {
                            try
                            {
                                var ergebnissOrdner = from o in db.Ordner
                                                      where o.Name == ordner.Name
                                                            && o.Pfad == ordner.Pfad
                                                            && o.Projektnummer == ordner.Projektnummer
                                                      select o;
                                if (ergebnissOrdner.Count() == 0)
                                {
                                    Ordner od = new Ordner();
                                    od.Projektnummer = ordner.Projektnummer;
                                    od.Name = ordner.Name;
                                    od.Pfad = ordner.Pfad;
                                    od.Root = ordner.Rootname;
                                    db.Ordner.Add(od);
                                }

                            }
                            catch (Exception ex)
                            {
                                Helper.LogHelper.WriteDebugLog(ex.ToString());
                            }
                        }
                        db.SaveChanges();
                    }

                    if (Dateien.Count > 0)
                    {
                        foreach (var datei in Dateien)
                        {
                            try
                            {

                                var ergebnissDatei = from d in db.Dateien
                                                     where d.Name == datei.Name
                                                            && d.Pfad == datei.Pfad
                                                            && d.Typ == datei.Typ
                                                     select d;
                                if (ergebnissDatei.Count() == 0)
                                {
                                    DAL.Dateien da = new DAL.Dateien();
                                    da.erstellt = datei.erstellt;
                                    da.geaendert = datei.geaendert;
                                    da.Name = datei.Name;
                                    da.Pfad = datei.Pfad;
                                    da.Typ = datei.Typ;
                                    da.Projektnummer = datei.Projektnummer;
                                    db.Dateien.Add(da);
                                }
                            }
                            catch (Exception ex)
                            {
                                Helper.LogHelper.WriteDebugLog(ex.ToString());
                            }
                        }
                        db.SaveChanges();

                    }
                }

            }

        }
    }
}
