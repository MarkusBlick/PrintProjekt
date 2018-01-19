using System;
using System.Windows.Forms;
using PrintumProjektverwaltung.DAL;
using PrintumProjektverwaltung.Models;
using Word = Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace PrintumProjektverwaltung.Helper
{
    internal class WordHelper
    {
        internal static void createNewWord(DataSet1.AdressenRow row)
        {
            throw new NotImplementedException();
        }

        internal static void createNewWord(printumBestellung neueBestellung)
        {
            try
            {
                DAL.PrintumProjekteEntities db = new PrintumProjekteEntities();

                string orginal = @"C:\Printum_server\99 Vorlagen\Dokumente\Template_Bestellung.doc";

                var projekt = db.Projekte.Find(neueBestellung.Projektnummer);

                string desitnation = projekt.RootOrdner + @"\06 Bestellungen\"//@"C:\Printum_server\06 printum BESTELLUNGEN\"
                                    + neueBestellung.Firmenname + "_"
                                    + neueBestellung.BestellungIDText + "_"
                                    + "sonstigeWichtigeTexte.doc";

                File.Copy(orginal, desitnation, true);

                neueBestellung.Vorlage = orginal;
                neueBestellung.Speicherort = desitnation;

                Word.Application wApp = new Word.Application();
                object filename = desitnation;
                wApp.Documents.Add();
                var dasDocument = wApp.Documents.Open(filename, ReadOnly: false, Visible: true);
                dasDocument.Activate();







                JsonSerializer serializer = new JsonSerializer();
                List<Templatmarker> liste = JsonConvert.DeserializeObject<List<Templatmarker>>(neueBestellung.Adresse);

                FindAndReplace2(dasDocument, "####Bestellnummer####", neueBestellung.BestellungIDText);
                FindAndReplace2(dasDocument, "####Projektnummer####", neueBestellung.ProjektnummerText);
                FindAndReplace2(dasDocument, "####Ansprechpartner####", Environment.UserName);
                FindAndReplace2(dasDocument, "####Datum####", DateTime.Now.Date.ToShortDateString());


                foreach (var cell in liste)
                {
                    FindAndReplace2(dasDocument, cell.MarkerName, cell.MarkerWert);
                }

                dasDocument.Save();
                dasDocument.Close();

                Marshal.ReleaseComObject(dasDocument);
                Marshal.ReleaseComObject(wApp);

                BestellungSpeichern(neueBestellung);

                System.Diagnostics.Process.Start(desitnation);

            }
            catch (Exception ex)
            {
                var bla = ex.ToString();
            }
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
                b.Vorlage = neueBestellung.Vorlage;

                db.Bestellungen.Add(b);
                db.SaveChanges();
            }
        }




        internal static void createWordAusAlterWord(string v)
        {
            string orginal = @"C:\Printum_server\99 Vorlagen\Dokumente\Bestellung-Template.docx";

            string neueDatei = @"C:\Printum_server\99 Vorlagen\Dokumente\Bestellung-Template" + DateTime.Now.Second.ToString() + @".docx";


            File.Copy(orginal, neueDatei, true);



            Word.Application wApp = new Word.Application();
            object filename = neueDatei;
            wApp.Documents.Add();
            var dasDocument = wApp.Documents.Open(filename, ReadOnly: false, Visible: true);
            dasDocument.Activate();


            try
            {
                Word.Range dieRange = dasDocument.Range();

                foreach (Word.Shape shape in dasDocument.Shapes)
                {
                    var eins = shape.Name;
                    var zwei = shape.Title;
                    var drei = shape;
                }
            }


            catch (Exception ex)
            {

                var bla = ex.ToString();
            }



            dasDocument.Save();
            dasDocument.Close();

            Marshal.ReleaseComObject(dasDocument);
            Marshal.ReleaseComObject(wApp);
        }




        private static bool FindAndReplace2(Word.Document dasDoc,
                                    object findText,
                                    object replaceWithText)
        {
            Word.Range dieRange = dasDoc.Range();

            bool result = false;

            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllwordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object readOnly = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            try
            {

                result = dieRange.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                                                        ref matchWildCards, ref matchSoundsLike, ref matchAllwordForms,
                                                        ref forward, ref wrap, ref format,
                                                        ref replaceWithText, ref replace, ref matchKashida,
                                                        ref matchDiacritics, ref matchAlefHamza, ref matchControl);




                foreach (Microsoft.Office.Interop.Word.Range item in dasDoc.StoryRanges)
                {
                    if (item.StoryType == Word.WdStoryType.wdMainTextStory
                        || item.StoryType == Word.WdStoryType.wdTextFrameStory)
                    {

                        item.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                                                            ref matchWildCards, ref matchSoundsLike, ref matchAllwordForms,
                                                            ref forward, ref wrap, ref format,
                                                            ref replaceWithText, ref replace, ref matchKashida,
                                                            ref matchDiacritics, ref matchAlefHamza, ref matchControl);
                    }
                }

            }
            catch (Exception ex)
            {
                var bla = ex.ToString();
            }
            return result;
        }



    }
}