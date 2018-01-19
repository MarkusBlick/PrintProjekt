using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintumProjektverwaltung.Models
{
    public class printumBestellung
    {
        public int Bestellung_ID { get; set; }
        public string BestellungIDText { get; set; }
        public Nullable<int> Projektnummer { get; set; }
        public string ProjektnummerText { get; set; }
        public string Firmenname { get; set; }
        public string Adresse { get; set; }
        public string Speicherort { get; set; }
        public string Vorlage { get; set; }
        public Nullable<System.DateTime> Datum { get; set; }
        public string ErstelltVon { get; set; }
        public string GeaendertVon { get; set; }
        public Nullable<System.DateTime> geaendert { get; set; }
        public string Email1Adresse { get; set; }
        public string Email2Adresse { get; set; }
        public string AdressID { get; set; }



        public string PDFpfad { get; set; }
        public Nullable<bool> istAbgeschickt { get; set; }

        public List<DAL.BestellungPositionen> bestellPositionen { get; set; }


        public int getNextBestellnr()
        {
            int dieNr = 0;


            using (DAL.PrintumProjekteEntities db = new DAL.PrintumProjekteEntities())
            {
                try
                {
                    // liest die DB aus
                    int maxBestellnr = db.Bestellungen.Max(x => x.Bestellung_ID);

                    // die ersten 2 nummern der Bestellnummer auslesen
                    string maxStringganz = maxBestellnr.ToString();
                    string maxStringkurz = maxStringganz.Substring(0, 2);
                    int maxkurz = Convert.ToInt32(maxStringkurz);

                    // 2-stellige Jahr
                    string JahrString = DateTime.Now.Year.ToString().Substring(2);
                    int jahrkurz = Convert.ToInt32(JahrString);


                    // die Erste Bestellung im neuen Jahr
                    if (jahrkurz > maxkurz)
                    {
                        string neuesJahrString = DateTime.Now.Year.ToString().Substring(2) + "0001";
                        dieNr = Convert.ToInt32(neuesJahrString);
                    }
                    // einen höher zählen
                    else
                    {
                        dieNr = maxBestellnr + 1;
                    }

                    this.BestellungIDText = getBestellungIDTextFrom(dieNr);
                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteDebugLog(ex.ToString());
                }

            }


            return dieNr;
        }

        private string getBestellungIDTextFrom(int dieNr)
        {
            string vorn = dieNr.ToString().Substring(0, 2);
            string hinten = dieNr.ToString().Substring(2);
            string textNr = vorn + "-" + hinten;
            return textNr;
        }
    }
}
