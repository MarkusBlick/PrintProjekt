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
    public partial class Form6_WE : Form
    {
        Form1_main f1;
        private string bestllnr;

        public Form6_WE()
        {
            InitializeComponent();
        }

        public Form6_WE(string bestllnr)
        {
            InitializeComponent();

            this.bestllnr = bestllnr;
        }

        private void Form6_WE_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.BestellungenNeuLaden();
            f1.Visible = true;
        }

        private void Form6_WE_Load(object sender, EventArgs e)
        {
            f1 = Application.OpenForms["Form1_main"] as Form1_main;
            f1.Visible = false;

            this.bestellungenTableAdapter.Fill(this.dataSet11.Bestellungen);
            nachBestellungsuchen("nix");
            this.bestellungPositionenTableAdapter.Fill(this.dataSet11.BestellungPositionen);
            nachPositionenSuchen("nix");

            if (bestllnr.Count()>0)
            {
                this.textBox1_suchen.Text = bestllnr;
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
                var wert = row.Cells["wEdatumDataGridViewTextBoxColumn"].Value;
                if (wert != null && wert.ToString() != "01.01.0001 00:00:00" && wert != DBNull.Value)
                {
                    row.Cells["geliefertCheck"].Value = true;
                }
            }

            this.bestellungPositionenDataGridView.ClearSelection();
        }

        private void button1_PrintumProjekte_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_suchen_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1_suchen.Text.Count() > 3)
            {
                nachBestellungsuchen(this.textBox1_suchen.Text);
            }
        }

        private void textBox1_suchen_Enter(object sender, EventArgs e)
        {
            this.textBox1_suchen.SelectAll();
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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.bestellungPositionenDataGridView.Rows)
            {
                row.Cells[0].Value = true;
                row.Cells[1].Value = DateTime.Now;

                int bestllporsID = (int)row.Cells["bestellPosIDDataGridViewTextBoxColumn"].Value;
                DateTime dasDatumn = (DateTime)row.Cells["wEdatumDataGridViewTextBoxColumn"].Value;
                aenderungSpeichern(row);
            }


        }

        private void aenderungSpeichern(DataGridViewRow row)
        {
            int bestllporsID = (int)row.Cells["bestellPosIDDataGridViewTextBoxColumn"].Value;

            DateTime? dasDate = null;
            var vl = row.Cells["wEdatumDataGridViewTextBoxColumn"].Value;
            if (vl.ToString() != "01.01.0001 00:00:00")
            {
                dasDate = (DateTime?)row.Cells["wEdatumDataGridViewTextBoxColumn"].Value;
            }


            //if (row.Cells["wEdatumDataGridViewTextBoxColumn"].Value == null)
            //{
            //    this.dataSet11.BestellungPositionen.Where(x => x.BestellPos_ID == bestllporsID).First().WE_datum = dasDate;
            //}
            //DateTime dasDatumn = (DateTime)row.Cells["wEdatumDataGridViewTextBoxColumn"].Value;

            //this.dataSet11.BestellungPositionen.Where(x => x.BestellPos_ID == bestllporsID).First().WE_datum = dasDatumn;
            //this.bestellungPositionenTableAdapter.Update(this.dataSet11.BestellungPositionen);
            //this.bestellungPositionenDataGridView.ClearSelection();

            using (DAL.PrintumProjekteEntities db = new DAL.PrintumProjekteEntities())
            {
                DAL.BestellungPositionen b = (from x in db.BestellungPositionen
                                              where x.BestellPos_ID == bestllporsID
                                              select x).First();
                b.WE_datum = dasDate;

                db.SaveChanges();

            }

        }
    }
}
