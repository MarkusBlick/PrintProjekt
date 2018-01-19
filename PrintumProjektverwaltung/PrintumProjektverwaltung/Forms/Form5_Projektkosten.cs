using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrintumProjektverwaltung.Models;

namespace PrintumProjektverwaltung.Forms
{
    public partial class Form5_Projektkosten : Form
    {
        public Form5_Projektkosten()
        {
            InitializeComponent();
        }

        internal List<printumProjekt> alleProjekte { get; set; }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            printumProjekt dasProjekt = (printumProjekt)this.treeView1.SelectedNode.Tag;
            if (dasProjekt != null)
            {
                string projekt = dasProjekt.Projektnummer.ToString();

                string filtertext4 = "CONVERT(Projektnummer, 'System.String') LIKE '*" + projekt + "*'";

                this.BestellProProjektbindingSource.Filter = filtertext4;

                this.label1.Text = this.bestellungPositionenDataGridView.Rows.Count.ToString() + " Positionen";
                addiereGesammtpreis();

            }

        }



        private void textBox5_bestellung_TextChanged(object sender, EventArgs e)
        {
            printumProjekt dasProjekt = (printumProjekt)this.treeView1.SelectedNode.Tag;

            if (dasProjekt != null)
            {
                string projekt = dasProjekt.Projektnummer.ToString();

                string filtertext = "CONVERT(Projektnummer, 'System.String') LIKE '*" + projekt + "*' ";


                string suchtext = this.textBox5_bestellung.Text.Trim();

                string filtertext1 = "BestellnungIDTest LIKE '*" + suchtext + "*'";
                string filtertext2 = "Adresse LIKE '*" + suchtext + "*'";
                string filtertext4 = "Artikelbezeichnung LIKE '*" + suchtext + "*'";

                this.BestellProProjektbindingSource.Filter =
                                                    filtertext + " AND ( " +
                                                    filtertext1 + " OR " +
                                                    filtertext2 + " OR " +
                                                    filtertext4 + " )";

            }
        }


        private void addiereGesammtpreis()
        {
            double sum = 0;
            for (int i = 0; i < this.bestellungPositionenDataGridView.Rows.Count; i++)
            {
                sum += Convert.ToDouble(this.bestellungPositionenDataGridView.Rows[i].Cells["dataGridViewTextBoxColumn9"].Value);
            }
            this.textBox2.Text = sum.ToString() + " €";
        }

        private void Form5_Projektkosten_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet1.BestellungPositionen". Sie können sie bei Bedarf verschieben oder entfernen.
            this.bestellungProProjektTableAdapter1.Fill(this.dataSet1.BestellungProProjekt);
            Helper.buildTree.addProjekte(this.treeView1, alleProjekte);
        }

        private void bestellungPositionenBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.BestellProProjektbindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        private void textBox5_projekte_TextChanged(object sender, EventArgs e)

        { 
            Helper.buildTree.filter(this.treeView1, alleProjekte, this.textBox5_projekte.Text);
        }

    }
}
