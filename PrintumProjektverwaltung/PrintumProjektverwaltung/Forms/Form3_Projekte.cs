using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PrintumProjektverwaltung.Forms
{
    public partial class Form3_Projekte : Form
    {
        DAL.PrintumProjekteEntities db = new DAL.PrintumProjekteEntities();
        DAL.Projekte neuesP = new DAL.Projekte();
        int letzteMaxProjektnummer = 0;

        // C:\ZSD\PrintumProjekte\99 Vorlagen
        string ordnerStruktur = @"\\PRINTUMSERVER\99-Vorlagen\Ordnerstruktur";
        string projektRoot = @"\\PRINTUMSERVER\PriPro\";


        public Form3_Projekte()
        {
            InitializeComponent();
        }




        private void Form3_Projekte_Load(object sender, EventArgs e)
        {
            letzteMaxProjektnummer = db.Projekte.Max(x => x.Projektnummer);
            neuesP.Projektnummer = letzteMaxProjektnummer + 1;
            this.textBox1_Projektnummer.Text = neuesP.Projektnummer.ToString();
            this.dateTimePicker2_lieferung.Value = DateTime.Now.AddMonths(3);

        }



        private void button1_Projekterstellen_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            neuesP.Projektname = this.textBox_Name.Text;

            int dieNeueNummer;
            if (Int32.TryParse(this.textBox1_Projektnummer.Text.Trim(), out dieNeueNummer))
            {
                neuesP.Projektnummer = dieNeueNummer;
            }
            else
            {
                MessageBox.Show("Ist die Projektnummer wirklich eine reine Nummer?");
                return;
            }


            string projektOrdner = projektRoot + this.textBox1_Projektnummer.Text + " - " + neuesP.Projektname;
            neuesP.RootOrdner = projektOrdner;

            try
            {
                Directory.CreateDirectory(projektOrdner);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(ordnerStruktur, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(ordnerStruktur, projektOrdner));
                }


                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(ordnerStruktur, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(ordnerStruktur, projektOrdner), true);
                }
            }
            catch (Exception ex)
            {


                var bla = ex.ToString();
            }

            if (checkProjektnummer())
            {
                neuesP.aktiv = true;
                neuesP.Produktionsbeginn = dateTimePicker1_beginn.Value;
                neuesP.Inbetriebname = dateTimePicker2_lieferung.Value;

                db.Projekte.Add(neuesP);
                db.SaveChanges();



                this.Close();

            }





        }

        private bool checkProjektnummer()
        {
            int dieNeue;

            bool istDasEineZahl = Int32.TryParse(this.textBox1_Projektnummer.Text, out dieNeue);
            if (istDasEineZahl)
            {
                var gibtsSchon = db.Projekte.Find(dieNeue);
                if (gibtsSchon == null)
                {
                    neuesP.Projektnummer = dieNeue;
                    return true;

                }
                else
                {
                    MessageBox.Show("Die Projektnummer gibt es schon!" + Environment.NewLine + Environment.NewLine +
                        gibtsSchon.Projektnummer.ToString() + " - " + gibtsSchon.Projektname);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Das ist keine gültige Projektnummer." + Environment.NewLine + Environment.NewLine + "Nur Zahlen sind erlaubt.");
                return false;
            }

        }



        private void label_ganzOben_Click(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
