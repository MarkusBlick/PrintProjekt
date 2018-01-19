using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintumProjektverwaltung.Forms
{
    public partial class Form7_AB : Form
    {
        Form1_main f1;
        string bestellnr;

        public Form7_AB()
        {
            InitializeComponent();
        }

        public Form7_AB(string _bestellnummer)
        {
            InitializeComponent();
            bestellnr = _bestellnummer;

        }

        private void button1_PrintumProjekte_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_alles_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.bestellungPositionenDataGridView.Rows)
            {
                row.Cells[0].Value = true;
                row.Cells[1].Value = DateTime.Now;

                int bestllporsID = (int)row.Cells["bestellPosIDDataGridViewTextBoxColumn"].Value;
                DateTime dasDatumn = (DateTime)row.Cells["aBdatumDataGridViewTextBoxColumn"].Value;
                aenderungSpeichern(row);
            }

        }

        private void Form7_AB_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.BestellungenNeuLaden();
            f1.Visible = true;
        }

        private void Form7_AB_Load(object sender, EventArgs e)
        {
            f1 = Application.OpenForms["Form1_main"] as Form1_main;
            f1.Visible = false;

            this.bestellungenTableAdapter.Fill(this.dataSet11.Bestellungen);
            nachBestellungsuchen("nix");
            this.bestellungPositionenTableAdapter.Fill(this.dataSet11.BestellungPositionen);
            nachPositionenSuchen("nix");

            if (bestellnr.Count() > 0)
            {
                this.textBox1_suchen.Text = bestellnr;
            }
        }

        private void textBox1_suchen_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1_suchen.Text.Count() > 3)
            {
                nachBestellungsuchen(this.textBox1_suchen.Text);
            }
        }

        private void nachBestellungsuchen(string such)
        {
            this.bestellungenBindingSource.Filter = "BestellungIDText like '%" + such + "%'";
        }

        private void nachPositionenSuchen(string such)
        {
            this.bestellungPositionenBindingSource.Filter = "BestellnungIDTest = '" + such + "'";
            foreach (DataGridViewRow row in this.bestellungPositionenDataGridView.Rows)
            {
                var wert = row.Cells["aBdatumDataGridViewTextBoxColumn"].Value;
                if (wert != null && wert.ToString() != "01.01.0001 00:00:00" && wert != DBNull.Value)
                {
                    row.Cells["bestaetigtCheck"].Value = true;
                }
            }

            this.bestellungPositionenDataGridView.ClearSelection();
        }

        private void bestellungPositionenDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var bla = this.bestellungPositionenDataGridView[e.ColumnIndex, e.RowIndex].Value;
            if (e.ColumnIndex == 0)
            {
                if (bla == null)
                {
                    this.bestellungPositionenDataGridView[e.ColumnIndex + 1, e.RowIndex].Value = DateTime.Now;
                    this.bestellungPositionenDataGridView[e.ColumnIndex, e.RowIndex].Value = true;
                    aenderungSpeichern(this.bestellungPositionenDataGridView.Rows[e.RowIndex]);
                }
                else
                {
                    bool wert = (bool)bla;
                    this.bestellungPositionenDataGridView[e.ColumnIndex + 1, e.RowIndex].Value = "";
                    this.bestellungPositionenDataGridView[e.ColumnIndex, e.RowIndex].Value = null;
                    aenderungSpeichern(this.bestellungPositionenDataGridView.Rows[e.RowIndex]);
                }



            }
        }

        private void bestellungenBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            string txt = "nix";
            if (this.bestellungenBindingSource.Count == 1)
            {
                var bRow = (DataRowView)this.bestellungenBindingSource[0];
                var bbrow = bRow.Row;
                if (bbrow != null)
                {
                    var bCell = bbrow[1];
                    if (bCell != null)
                    {
                        nachPositionenSuchen(bCell.ToString());

                    }
                }


            }
        }


        private void aenderungSpeichern(DataGridViewRow row)
        {
            int bestllporsID = (int)row.Cells["bestellPosIDDataGridViewTextBoxColumn"].Value;
            string bestllIDText = row.Cells["bestellnungIDTestDataGridViewTextBoxColumn"].Value.ToString();


            DateTime? dasDate = null;
            var vl = row.Cells["aBdatumDataGridViewTextBoxColumn"].Value;
            if (vl.ToString() != "01.01.0001 00:00:00" && vl != DBNull.Value)
            {
                dasDate = (DateTime?)row.Cells["aBdatumDataGridViewTextBoxColumn"].Value;
            }



            using (DAL.PrintumProjekteEntities db = new DAL.PrintumProjekteEntities())
            {
                DAL.BestellungPositionen b = (from x in db.BestellungPositionen
                                              where x.BestellPos_ID == bestllporsID
                                              select x).First();
                b.AB_datum = dasDate;

                db.SaveChanges();

                try
                {



                    var alleBs = from x in db.BestellungPositionen
                                 where x.BestellnungIDTest == bestllIDText
                                 select x;

                    var alleMitDatum = from x2 in db.BestellungPositionen
                                       where x2.BestellnungIDTest == bestllIDText
                                       && x2.AB_datum != null
                                       select x2;


                    DAL.Bestellungen bestll = (from x in db.Bestellungen
                                               where x.BestellungIDText == bestllIDText
                                               select x).First();

                    if (alleMitDatum.Count() == 0)
                    {
                        // False
                        bestll.AB = false;

                    }
                    else
                    {
                        if (alleBs.Count() == alleMitDatum.Count())
                        {
                            // True
                            bestll.AB = true;
                        }
                        else
                        {
                            // null
                            bestll.AB = null;
                        }
                    }
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
