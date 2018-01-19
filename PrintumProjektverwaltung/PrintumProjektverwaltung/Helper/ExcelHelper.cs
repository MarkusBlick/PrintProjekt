using System;
using PrintumProjektverwaltung.DAL;
using PrintumProjektverwaltung.Models;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Globalization;

namespace PrintumProjektverwaltung.Helper
{
    class ExcelHelper
    {

        private static Excel.Application excelApp;
        private static Excel.Workbook excelWorkbook;
        private static Excel.Worksheet excelSheet;

        private static Excel.Workbook vorlageWorkbook;
        private static Excel.Worksheet vorlageSheet;

        private static string vorlageFile = @"\\PRINTUMSERVER\99-Vorlagen\Dokumente\Bestellung-Template.xlsx";

        public static void createNewExcel(printumBestellung neueBestellung)
        {

            string destination = getBestellungFilename(neueBestellung);

            try
            {
                File.Copy(vorlageFile, destination, true);

                neueBestellung.Vorlage = vorlageFile;
                neueBestellung.Speicherort = destination;

            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }


            // das Excelsheet offnen
            if (File.Exists(destination))
            {
                try
                {
                    // die Excelanwendung
                    excelApp = new Excel.Application();
                    excelApp.DisplayAlerts = false;


                    // die Excelmappe
                    excelWorkbook = excelApp.Workbooks.Open(destination,
                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                        Type.Missing, Type.Missing);

                    int anzahlBlaetter = excelWorkbook.Sheets.Count;
                    if (anzahlBlaetter > 0)
                    {

                        // das Excelblatt
                        excelSheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                        diePlatzhalterBefuellen(excelSheet, neueBestellung);

                        excelWorkbook.Save();


                    }



                    // alles beenden und freigeben
                    excelWorkbook.Close(false, destination, null);
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelWorkbook);
                    Marshal.ReleaseComObject(excelApp);

                    BestellungSpeichern(neueBestellung);



                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteDebugLog(ex.ToString());

                }



            }


        }

        public static List<printumBestellPositionen> getBestellPositionen(Excel.Workbook excelWorkbook, string bestellnr)
        {
            List<printumBestellPositionen> list = new List<printumBestellPositionen>();
            int anfang;
            int ende;

            try
            {

                Excel.Worksheet dasSheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
                Excel.Range dieRange = dasSheet.UsedRange;
                anfang = getAnfangDerTabelle(dieRange);
                ende = getEndeDerTabelle(dieRange);

                for (int i = anfang; i < ende; i++)
                {

                    Excel.Range zeile = ((Excel.Range)dieRange.Rows[i]);

                    printumBestellPositionen bestellpos = new printumBestellPositionen();
                    bestellpos.BestellnungIDTest = bestellnr;

                    var z1 = (Excel.Range)zeile.Columns[1]; // Pos
                    var z2 = (Excel.Range)zeile.Columns[2]; // Stück
                    var z3 = (Excel.Range)zeile.Columns[3]; // Artikel
                    var z4 = (Excel.Range)zeile.Columns[4]; // Liefertermin
                    var z5 = (Excel.Range)zeile.Columns[5]; // Einzelpreis EUR
                    var z6 = (Excel.Range)zeile.Columns[6]; // Gesamt EUR

                    try
                    {

                        var zelle1 = z1.Value2; // Pos
                        var zelle2 = z2.Value2; // Stück
                        var zelle3 = z3.Value2; // Artikel
                        var zelle4 = z4.Value2; // Liefertermin
                        var zelle5 = z5.Value2; // Einzelpreis EUR
                        var zelle6 = z6.Value2; // Gesamt EUR


                        try
                        {
                            bestellpos.Pos = Convert.ToInt32(zelle1);
                        }
                        catch (Exception ex)
                        {
                            var bla = ex.ToString();
                        }

                        try
                        {
                            bestellpos.Stueck = Convert.ToInt32(zelle2);
                        }
                        catch (Exception ex)
                        {
                            var bla = ex.ToString();
                        }

                        try
                        {
                            bestellpos.Artikelbezeichnung = zelle3.ToString();
                        }
                        catch (Exception ex)
                        {
                            var bla = ex.ToString();
                        }

                        try
                        {
                            if (zelle4 != null)
                            {
                                // in Datum umwandeln
                                string wert = zelle4.ToString();
                                wert = wert.ToLower();
                                if (wert.Contains("kw"))
                                {

                                    string kwzahl = wert.Replace("kw", "");
                                    kwzahl = kwzahl.Replace("-", "");
                                    kwzahl = kwzahl.Trim();
                                    int diekw;
                                    if (Int32.TryParse(kwzahl, out diekw))
                                    {
                                        bestellpos.Liefertermin = FirstDateOfWeekISO8601(diekw);
                                    }
                                }
                                else
                                {
                                    bestellpos.Liefertermin = DateTime.FromOADate(zelle4);

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            var bla = ex.ToString();
                        }

                        try
                        {
                            bestellpos.Einzelpreis = Convert.ToDouble(zelle5);
                        }
                        catch (Exception ex)
                        {
                            var bla = ex.ToString();
                        }

                        try
                        {
                            bestellpos.Gesammtpreis = Convert.ToDouble(zelle6);
                        }
                        catch (Exception ex)
                        {
                            var bla = ex.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        Helper.LogHelper.WriteDebugLog(ex.ToString());
                    }
                    if (bestellpos.Artikelbezeichnung.Length > 0 &&
                        bestellpos.Stueck > 0
                        )
                    {
                        list.Add(bestellpos);

                    }




                }

            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }


            return list;
        }

        public static void createNewExcelFromOldone(printumBestellung neueBestellung, string dieVorlage)
        {

            string destination = getBestellungFilename(neueBestellung);

            try
            {
                File.Copy(vorlageFile, destination, true);

                neueBestellung.Vorlage = vorlageFile;
                neueBestellung.Speicherort = destination;

            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }


            // das Excelsheet offnen
            if (File.Exists(destination))
            {
                try
                {
                    // die Excelanwendung
                    excelApp = new Excel.Application();
                    excelApp.DisplayAlerts = false;


                    // die Excelmappe
                    excelWorkbook = excelApp.Workbooks.Open(destination,
                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                        Type.Missing, Type.Missing);

                    int anzahlBlaetter = excelWorkbook.Sheets.Count;
                    if (anzahlBlaetter > 0)
                    {

                        // das Excelblatt
                        excelSheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                        diePlatzhalterBefuellen(excelSheet, neueBestellung);

                        dieAltenPositionenUebernehmen(excelSheet, dieVorlage);

                        excelWorkbook.Save();


                    }



                    // alles beenden und freigeben
                    excelWorkbook.Close(false, destination, null);
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelWorkbook);
                    Marshal.ReleaseComObject(excelApp);

                    // in die Datenbank
                    BestellungSpeichern(neueBestellung);


                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteDebugLog(ex.ToString());
                }



            }
        }




        private static void dieAltenPositionenUebernehmen(Excel.Worksheet excelSheet, string dieVorlage)
        {
            int anfangVorlage;
            int endeTabelle;
            int endeZielTabelle;
            int endeVorlageAlles;
            int anfangZiel;
            int endeZielAlles;
            int anzahlZuKopieren;

            try
            {

                vorlageWorkbook = excelApp.Workbooks.Open(dieVorlage,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing);

                int anzahlBlaetter = vorlageWorkbook.Sheets.Count;
                if (anzahlBlaetter > 0)
                {

                    // das Excelblatt
                    vorlageSheet = (Excel.Worksheet)vorlageWorkbook.Sheets[1];
                    Excel.Range vorlageZeilen = vorlageSheet.UsedRange;

                    anfangVorlage = getAnfangDerTabelle(vorlageZeilen);
                    endeTabelle = getEndeDerTabelle(vorlageZeilen);
                    endeVorlageAlles = vorlageZeilen.Rows.Count;
                    anzahlZuKopieren = endeVorlageAlles - anfangVorlage;

                    Excel.Range zielZeilen = excelSheet.UsedRange;
                    anfangZiel = getAnfangDerTabelle(zielZeilen);
                    endeZielTabelle = anfangZiel + (endeTabelle - anfangVorlage);
                    endeZielAlles = anfangZiel + anzahlZuKopieren;



                    // Range Tabelle
                    string inserttext = "A" + anfangVorlage.ToString() + ":F" + endeTabelle.ToString();
                    Excel.Range TabellezuKopieren = vorlageSheet.get_Range(inserttext);


                    string copytext = "A" + anfangZiel.ToString() + ":F" + endeZielTabelle.ToString();
                    Excel.Range hierreinTabelle = excelSheet.get_Range(inserttext);

                    TabellezuKopieren.Copy(Type.Missing);
                    hierreinTabelle.PasteSpecial(Excel.XlPasteType.xlPasteAll,
                                            Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                                            false, false);


                    // danach
                    string danachtext = "A" + (endeTabelle + 1).ToString() + ":F" + (endeVorlageAlles + 1).ToString();
                    Excel.Range danachZuKopieren = vorlageSheet.get_Range(inserttext);


                    string copyrest = "A" + (endeZielTabelle + 1).ToString() + "F:" + (endeZielAlles + 1).ToString();
                    Excel.Range hierreinRest = excelSheet.get_Range(inserttext);

                    danachZuKopieren.Copy(Type.Missing);
                    hierreinRest.PasteSpecial(Excel.XlPasteType.xlPasteAll,
                                            Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                                            false, false);


                }


                // alles beenden und freigeben
                vorlageWorkbook.Close(false, dieVorlage, null);
                Marshal.ReleaseComObject(vorlageWorkbook);
            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }

        }

        private static int getAnfangDerTabelle(Excel.Range zeilen)
        {
            int zeilenindex = -1;


            int allezeilen = zeilen.Rows.Count;

            for (int i = 15; i < allezeilen; i++)
            {
                try
                {
                    Excel.Range zeile = ((Excel.Range)zeilen.Rows[i]);

                    // var zeilenId = zeile.ID;
                    var spaltenanzahl = zeile.Count;

                    var zelle = (Excel.Range)zeile.Columns[1];

                    if (zelle.Value == "Pos")
                    {
                        var zelle2 = (Excel.Range)zeile.Columns[2];
                        string wert2 = zelle2.Value.ToString();
                        wert2 = wert2.ToLower().Trim();
                        if (wert2.Contains("stück")
                            || wert2.Contains("paar")
                            || wert2.Contains("liter")
                            || wert2.Contains("meter")
                            || wert2.Contains("rolle")
                            || wert2.Contains("fass"))
                        {
                            var zelle3 = (Excel.Range)zeile.Columns[3];
                            if (zelle3.Value == "Artikel")
                            {
                                // hier sollte klar sein , dass wir in der ersten Zeile der Tabellesind...
                                zeilenindex = i + 1;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteDebugLog(ex.ToString());
                }

            }

            return zeilenindex;
        }

        private static int getEndeDerTabelle(Excel.Range zeilen)
        {

            int iPos = getAnfangDerTabelle(zeilen);



            int zeilenindex = -1;


            int allezeilen = zeilen.Rows.Count;

            for (int i = iPos; i < allezeilen; i++)
            {
                try
                {
                    // Excel.Range zeile = ((Excel.Range)zeilen.Rows[i]);


                    Excel.Range _cellPos = (Excel.Range)(zeilen.Cells[i, 1]);
                    Excel.Range _cellStueck = (Excel.Range)(zeilen.Cells[i, 2]);
                    Excel.Range _cellPreis = (Excel.Range)(zeilen.Cells[i, 5]);
                    Excel.Range _cellGesammt = (Excel.Range)(zeilen.Cells[i, 6]);



                    string _0Value = _cellPos.Value2 != null ? _cellPos.Value2.ToString() : "";
                    string _1Value = _cellStueck.Value2 != null ? _cellStueck.Value2.ToString() : "";
                    string _2Format = _cellPreis.Value2 != null ? _cellPreis.NumberFormat : "";
                    string _3Format = _cellGesammt.Value2 != null ? _cellGesammt.NumberFormat : "";



                    int c0;
                    bool try0 = int.TryParse(_0Value, out c0);

                    int c1;
                    bool try1 = int.TryParse(_1Value, out c1);

                    if (
                        // _2Format.Contains("#,##0.00") == false ||
                        //_3Format.Contains("#,##0.00") == false ||
                        try0 == false ||
                        try1 == false)
                    {

                        zeilenindex = i;
                        break;
                    }






                }
                catch (Exception ex)
                {
                    Helper.LogHelper.WriteDebugLog(ex.ToString());
                }

            }

            return zeilenindex;
        }

        private static void BestellungSpeichern(printumBestellung neueBestellung)
        {
            using (PrintumProjekteEntities db = new PrintumProjekteEntities())
            {
                Bestellungen b = new Bestellungen();


                b.Bestellung_ID = neueBestellung.Bestellung_ID;
                b.BestellungIDText = neueBestellung.BestellungIDText;
                b.Adresse = neueBestellung.Adresse;
                b.ErstelltVon = neueBestellung.ErstelltVon;
                b.geaendert = neueBestellung.geaendert;
                b.GeaendertVon = neueBestellung.GeaendertVon;
                b.Projektnummer = neueBestellung.Projektnummer;
                b.Speicherort = neueBestellung.Speicherort;
                b.AdressID = neueBestellung.AdressID;
                b.Vorlage = neueBestellung.Vorlage;
                b.Datum = DateTime.Now;

                List<Templatmarker> liste = JsonConvert.DeserializeObject<List<Templatmarker>>(neueBestellung.Adresse);
                foreach (var cell in liste)
                {
                    if (cell.MarkerName != null)
                    {
                        if (cell.MarkerName.Length > 0)
                        {
                            if (cell.MarkerName == "####Matchcode####")
                            {
                                b.Adresse = cell.MarkerWert;
                            }

                        }
                    }


                }

                b.EmailAdresse = neueBestellung.Email1Adresse;
                b.AdressID = neueBestellung.AdressID;

                db.Bestellungen.Add(b);
                db.SaveChanges();
            }
        }

        private static void diePlatzhalterBefuellen(Excel.Worksheet excelSheet, printumBestellung neueBestellung)
        {
            //bool success = excelSheet.Rows.Replace(What: "####Bestellnummer####", Replacement: "17-12345");
            try
            {
                excelSheet.Rows.Replace(What: "####Bestellnummer####", Replacement: neueBestellung.BestellungIDText);

                excelSheet.Rows.Replace(What: "####Projektnummer####", Replacement: neueBestellung.ProjektnummerText);
                excelSheet.Rows.Replace(What: "####Ansprechpartner####", Replacement: Environment.UserName);
                excelSheet.Rows.Replace(What: "####Datum####", Replacement: DateTime.Now.Date.ToShortDateString());

                var leftHeader = excelSheet.PageSetup.LeftHeader.Replace("####Bestellnummer####", neueBestellung.BestellungIDText);
                var middleHeader = excelSheet.PageSetup.CenterHeader.Replace("####Projektnummer####", neueBestellung.ProjektnummerText);
                excelSheet.PageSetup.LeftHeader = leftHeader;
                excelSheet.PageSetup.CenterHeader = middleHeader;

                List<Templatmarker> liste = JsonConvert.DeserializeObject<List<Templatmarker>>(neueBestellung.Adresse);


                foreach (var cell in liste)
                {
                    if (cell.MarkerName != null)
                    {
                        if (cell.MarkerName.Length > 0)
                        {
                            excelSheet.Rows.Replace(What: cell.MarkerName, Replacement: cell.MarkerWert);

                        }
                    }


                }



            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }
        }


        private static string getBestellungFilename(printumBestellung neueBestellung)
        {
            string destination = "";


            try
            {
                // das leere Template öffnen
                DAL.PrintumProjekteEntities db = new PrintumProjekteEntities();


                var projekt = db.Projekte.Find(neueBestellung.Projektnummer);

                destination = projekt.RootOrdner + @"\06 Bestellungen\"//@"C:\Printum_server\06 printum BESTELLUNGEN\"
                                   + neueBestellung.Firmenname + "_"
                                   + neueBestellung.BestellungIDText + "_"
                                   + "Printumbestellung.xlsx";
            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }
            return destination;
        }

        public static DateTime FirstDateOfWeekISO8601(int weekOfYear)
        {
            int year = DateTime.Now.Year;

            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
    }
}
