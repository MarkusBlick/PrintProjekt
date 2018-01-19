using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PrintumProjektverwaltung.Helper
{
    internal class Jsons
    {
        internal static string datagridrowToJson(DataGridViewRow row)
        {


            List<Models.Templatmarker> liste = new List<Models.Templatmarker>();



            foreach (DataGridViewCell cell in row.Cells)
            {
                var spalte = cell.AccessibilityObject.Name;
                var wert = cell.AccessibilityObject.Value;

                Models.Templatmarker mk = new Models.Templatmarker();

                if (spalte.Contains("Name"))
                {
                    mk.MarkerName = "####Name####";
                    mk.MarkerWert = wert;

                }

                if (spalte.Contains("Firmenname"))
                {
                    mk.MarkerName = "####Firmenname####";
                    mk.MarkerWert = wert;

                }

                if (spalte.Contains("Strasse"))
                {
                    mk.MarkerName = "####Strasse####";
                    mk.MarkerWert = wert;
                }

                if (spalte.Contains("PLZ"))
                {
                    mk.MarkerName = "####PLZ####";
                    mk.MarkerWert = wert;
                }

                if (spalte.Contains("Ort"))
                {
                    mk.MarkerName = "####Ort####";
                    mk.MarkerWert = wert;

                }

                if (spalte.Contains("Land"))
                {
                    mk.MarkerName = "####Land####";
                    mk.MarkerWert = wert;

                }
                if (spalte.Contains("ZuHaendenVon"))
                {
                    mk.MarkerName = "####ZuHaendenVon####";
                    mk.MarkerWert = wert;

                }
                if (spalte.Contains("PLZundORT"))
                {
                    mk.MarkerName = "####PLZundORT####";
                    mk.MarkerWert = wert;

                }
                if (spalte.Contains("Matchcode"))
                {
                    mk.MarkerName = "####Matchcode####";
                    mk.MarkerWert = wert;

                }

                liste.Add(mk);
            }

            string json = JsonConvert.SerializeObject(liste, Formatting.Indented);
            return json;
        }

    }
}