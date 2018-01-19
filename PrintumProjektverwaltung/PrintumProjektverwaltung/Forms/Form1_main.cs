using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using PrintumProjektverwaltung.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;


namespace PrintumProjektverwaltung.Forms
{
    public partial class Form1_main : Form
    {
        public ExchangeService service;
        public ContactsFolder contactsfolder;
        public FindItemsResults<Item> contactItems;
        public List<Contact> alleAdressen;
        public printumUser derUser;


        private List<printumProjekt> alleProjekte;

        private List<printumBestellPositionen> akutelleBestellung;

        private TreeView treeviewCach;

        public Form1_main()
        {
            InitializeComponent();

            // Konstruktor usw für die Outlookadressen
            service = new ExchangeService(ExchangeVersion.Exchange2013);
            service.Url = new Uri("https://outlook.office365.com/ews/exchange.asmx");
            service.Credentials = new WebCredentials("info@printum.de", "#PR1n+um#");
            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;
            alleAdressen = new List<Contact>();
            treeviewCach = new TreeView();
        }
        private void Form1_main_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet11.BestellungPositionen". Sie können sie bei Bedarf verschieben oder entfernen.
            this.bestellungPositionenTableAdapter.FillBy(this.dataSet11.BestellungPositionen);

            // TODO: This line of code loads data into the 'dataSet1.Bestellungen' table. You can move, or remove it, as needed.
            this.bestellungenTableAdapter.Fill(this.dataSet11.Bestellungen);
            this.label_Datum.Text = DateTime.Now.Date.ToString().Substring(0, 10) + "   Bernds Finest (" + System.Windows.Forms.Application.ProductVersion + ")";

            //derUser = new printumUser();
            //derUser.getUserFromAD(Environment.UserName);

            this.label_user.Text = System.Environment.UserName;

            alleProjekte = loadDieProjekte();

            Helper.buildTree.addProjekte(this.treeView1, alleProjekte);


            this.bestellungenDataGridView.ClearSelection();
            bestellungPositionenDataGridView.Visible = false;

            //  BeginInvoke((Action)(() => GetItemsAsync()));
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() => GetItemsAsync());

        }

        private List<printumProjekt> loadDieProjekte()
        {

            List<printumProjekt> dieListe = new List<printumProjekt>();
            using (DAL.PrintumProjekteEntities db = new DAL.PrintumProjekteEntities())
            {
                var dbProjekte = from p in db.Projekte
                                 orderby p.Projektnummer descending
                                 select new printumProjekt
                                 {
                                     Projektnummer = p.Projektnummer,
                                     Projektname = p.Projektname,
                                     Projektbeginn = p.Projektbeginn,
                                     RootOrdner = p.RootOrdner,
                                     Inbetriebname = p.Inbetriebname,
                                     Produktionsbeginn = p.Produktionsbeginn,
                                     aktiv = p.aktiv

                                 };

                try
                {
                    if (dbProjekte.Count() > 0)
                    {
                        foreach (var item in dbProjekte)
                        {
                            // item.Unterordner = printumOrdner.getOrdnerBy(item.Projektnummer); // T0Do
                            //item.Dateien = printumDatei.getDateienBy(item.Projektnummer); // ToDo
                        }
                        dieListe = dbProjekte.ToList();
                    }
                }
                catch (Exception ex)
                {

                    Helper.LogHelper.WriteDebugLog(ex.ToString());
                }


            }
            return dieListe;
        }


        private void button1_readFolder_Click(object sender, EventArgs e)
        {
            Form5_Projektkosten f5 = new Form5_Projektkosten();
            f5.alleProjekte = alleProjekte;
            f5.Show();
        }


        private async System.Threading.Tasks.Task GetItemsAsync()
        {
            await System.Threading.Tasks.Task.Delay(1000).ConfigureAwait(false);


            ItemView view = new ItemView(10, 0, OffsetBasePoint.Beginning);

            view.OrderBy.Add(ItemSchema.DateTimeCreated, SortDirection.Descending);


            do
            {
                contactItems = service.FindItems(WellKnownFolderName.Contacts, view);

                foreach (Item item in contactItems)
                {
                    // Do something with the item.
                    if (item is Contact)
                    {
                        Contact cont = (Contact)item;
                        alleAdressen.Add(cont);
                    }


                }

                //any more batches?
                if (contactItems.NextPageOffset.HasValue)
                {
                    view.Offset = contactItems.NextPageOffset.Value;
                }
            }
            while (contactItems.MoreAvailable);

        }



        private void button2_neueBestellung_Click(object sender, EventArgs e)
        {
            var tvNode = this.treeView1.SelectedNode;

            if (tvNode == null)
            {
                MessageBox.Show("Wie..." + Environment.NewLine +
                                                   Environment.NewLine +
                                                   "kein Projekt ausgewählt?");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                printumBestellung neueBestellung = new printumBestellung();
                neueBestellung.Bestellung_ID = neueBestellung.getNextBestellnr();



                try
                {
                    var item = this.treeView1.SelectedNode;

                    printumProjekt currProjekt = (printumProjekt)item.Tag;

                    neueBestellung.Projektnummer = currProjekt.Projektnummer;
                    neueBestellung.ProjektnummerText = currProjekt.Projektnummer.ToString();

                }
                catch (Exception es)
                {
                    var bal = es.ToString();
                }

                Form2_Bestellung FormBestellung = new Form2_Bestellung(neueBestellung, alleAdressen);
                var FormErgebniss = FormBestellung.ShowDialog();

                this.bestellungenTableAdapter.Fill(this.dataSet11.Bestellungen);

                this.Cursor = Cursors.Default;
            }
        }



        private void button3_neuesProjekt_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Form3_Projekte f3 = new Form3_Projekte();
            f3.ShowDialog();

            alleProjekte = loadDieProjekte();
            Helper.buildTree.addProjekte(this.treeView1, alleProjekte);

            this.Cursor = Cursors.Default;

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.treeView1.SelectedNode != null)
            {
                printumProjekt currP = (printumProjekt)this.treeView1.SelectedNode.Tag;
                if (currP.RootOrdner != null)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(currP.RootOrdner);

                    }
                    catch (Exception ex)
                    {

                    }

                }

            }
            this.Cursor = Cursors.Default;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.bestellungPositionenDataGridView.Visible = false;

            printumProjekt dasProjekt = (printumProjekt)this.treeView1.SelectedNode.Tag;
            if (dasProjekt != null)
            {
                string projekt = dasProjekt.Projektnummer.ToString();

                string filtertext4 = "CONVERT(Projektnummer, 'System.String') LIKE '*" + projekt + "*'";

                this.bestellungenBindingSource.Filter = filtertext4;
                this.bestellungenDataGridView.ClearSelection();

            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            Helper.buildTree.filter(this.treeView1, alleProjekte, this.textBox1.Text);


        }

        private void bestellungenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = this.bestellungenDataGridView.CurrentRow;

            if (row != null)
            {
                string pfadPDF = "";
                string pfadEXCEL = "";

                var zelleExcel = row.Cells["speicherortDataGridViewTextBoxColumn"];
                if (zelleExcel != null)
                {
                    pfadEXCEL = zelleExcel.Value.ToString();

                }

                var zellePDF = row.Cells["PDFpfad"];
                if (zellePDF != null)
                {
                    pfadPDF = zellePDF.Value.ToString();

                }


                if (File.Exists(pfadPDF))
                {
                    System.Diagnostics.Process.Start(pfadPDF);
                }
                else
                {
                    if (File.Exists(pfadEXCEL))
                    {
                        System.Diagnostics.Process.Start(pfadEXCEL);
                    }
                    else
                    {
                        MessageBox.Show("Die Datei gibt es nicht (oder nicht mehr...).");
                    }
                }
            }
        }


        private void button_loeschen_Click(object sender, EventArgs e)
        {
            bool istBestellungausgewählt = this.bestellungenDataGridView.SelectedRows.Count == 1;

            if (istBestellungausgewählt)
            {
                var row = this.bestellungenDataGridView.CurrentRow;

                if (row != null)
                {
                    if (row.Cells["istAbgeschickt"].Value != null)
                    {

                    }
                    else
                    {
                        schonAbgeschickt(row, "  Kann also nicht mehr gelöscht werden.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Bitte eine  Bestellung auswählen!");
            }
        }

        private void button4_mail_Click(object sender, EventArgs e)
        {

            bool istBestellungausgewählt = this.bestellungenDataGridView.SelectedRows.Count == 1;

            if (istBestellungausgewählt)
            {

                var row = this.bestellungenDataGridView.CurrentRow;

                if (row != null)
                {
                    if (row.Cells["istAbgeschickt"].Value != null)
                    {
                        bool istAbgeschickt;
                        bool.TryParse(row.Cells["istAbgeschickt"].Value.ToString(), out istAbgeschickt);
                        if (istAbgeschickt == false)
                        {

                            var zelle1 = row.Cells["speicherortDataGridViewTextBoxColumn"];
                            if (zelle1 != null)
                            {
                                string pfad = zelle1.Value.ToString();
                                var email = row.Cells["EmailAdresse"].Value.ToString();
                                var bestellnr = row.Cells["bestellungIDTextDataGridViewTextBoxColumn"].Value.ToString();
                                var projektnr = row.Cells["projektnummerDataGridViewTextBoxColumn"].Value.ToString();
                                int projetnrInt = 0;
                                int.TryParse(projektnr, out projetnrInt);

                                if (File.Exists(pfad))
                                {
                                    Form4_Spinner f4 = new Form4_Spinner();
                                    f4.ShowDialog();


                                    akutelleBestellung = Helper.OutlookHelper.createNewEmailmitAnhang(pfad, bestellnr, projetnrInt, email);

                                    // jetzt die mail überwachen.

                                }
                            }
                            else
                            {
                                MessageBox.Show("Die Datei gibt es nicht (oder nicht mehr...).");
                            }


                        }
                        else // wurde schon abgeschickt
                        {
                            schonAbgeschickt(row, "");
                        }


                    }
                    else
                    {
                        schonAbgeschickt(row, "");
                    }
                }


            }
            else
            {
                MessageBox.Show("Bitte eine  Bestellung auswählen!");
            }
        }

        private void schonAbgeschickt(DataGridViewRow row, string msg)
        {

            string txt = "Die Bestellung wurde schon abgeschickt! " + msg + Environment.NewLine + Environment.NewLine;
            DateTime datum;
            if (DateTime.TryParse(row.Cells["datumDataGridViewTextBoxColumn"].Value.ToString(), out datum))
            {
                txt += " am: " + datum.ToLongDateString() + "  um: " + datum.ToShortTimeString() + Environment.NewLine;
                txt += " von: " + row.Cells["GeaendertVon"].Value.ToString();
            }
            MessageBox.Show(txt);
        }


        public void BestellungenNeuLaden()
        {
            this.bestellungenTableAdapter.Fill(this.dataSet11.Bestellungen);
            this.bestellungPositionenTableAdapter.FillBy(this.dataSet11.BestellungPositionen);
        }

        private void button5_alleBestellungenanzeigen_Click(object sender, EventArgs e)
        {
            this.bestellungenBindingSource.Filter = "BestellungIDText LIKE '*'";
            bestellungPositionenDataGridView.Visible = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.bestellungPositionenDataGridView.Visible = false;


            string suchtext = this.textBox2.Text.Trim();

            string filtertext1 = "BestellungIDText LIKE '*" + suchtext + "*'";
            string filtertext2 = "CONVERT(Projektnummer, 'System.String') LIKE '*" + suchtext + "*'";
            string filtertext3 = "Adresse LIKE '*" + suchtext + "*'";
            string filtertext4 = "EmailAdresse LIKE '*" + suchtext + "*'";

            this.bestellungenBindingSource.Filter =
                                                filtertext1 + " OR " +
                                                filtertext2 + " OR " +
                                                filtertext3 + " OR " +
                                                filtertext4;
        }

        private void bestellungenDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            var row = this.bestellungenDataGridView.CurrentRow;

            if (row != null)
            {

                if (row.Selected)
                {
                    string bestellnr = row.Cells["bestellungIDTextDataGridViewTextBoxColumn"].Value.ToString();

                    string filtertext = "BestellnungIDTest LIKE '" + bestellnr + "'";
                    this.bestellungPositionenBindingSource.Filter = filtertext;

                    bestellungPositionenDataGridView.Visible = true;


                }
                else
                {
                    bestellungPositionenDataGridView.Visible = false;
                }
            }
        }



        private void button6_WE_Click(object sender, EventArgs e)
        {
            Form6_WE f6;
            if (this.bestellungenDataGridView.SelectedRows.Count == 1)
            {
                string bestllnr = this.bestellungenDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                f6 = new Form6_WE(bestllnr);
            }
            else
            {
                f6 = new Form6_WE();
            }

            f6.Show();
        }

        private void button7_Auftragsbestaetigung_Click(object sender, EventArgs e)
        {
            Form7_AB f7;
            if (this.bestellungenDataGridView.SelectedRows.Count == 1)
            {
                string bestllnr = this.bestellungenDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                f7 = new Form7_AB(bestllnr);
            }
            else
            {
                f7 = new Form7_AB();
            }

            f7.Show();
        }

        private void button8_Adressen_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            loadDieProjekte();
            BestellungenNeuLaden();

            this.Cursor = Cursors.Default;

        }

        private void timer1_tableLoad_Tick(object sender, EventArgs e)
        {
            // this.label1.Text = "Projekte (nachladen)";
            alleProjekte = loadDieProjekte();
            Helper.buildTree.addProjekte(this.treeView1, alleProjekte);
            BestellungenNeuLaden();
            // this.label1.Text = "Projekte";

        }

        // rechte Maus aktiviert die Node
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
        }

        private void toolStripMenuItem1_projektname_Click(object sender, EventArgs e)
        {
            var derTag = this.treeView1.SelectedNode.Tag;
            if (derTag != null)
            {
                Form1_1_Projektname f11 = new Form1_1_Projektname(derTag);
                f11.ShowDialog();

                alleProjekte = loadDieProjekte();
                Helper.buildTree.addProjekte(this.treeView1, alleProjekte);
            }
        }



        private void projektLöschenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var derTag = this.treeView1.SelectedNode.Tag;
            if (derTag != null)
            {
                printumProjekt dasP = (printumProjekt)derTag;

                var sollIch = MessageBox.Show("Soll das Projekt " + dasP.Projektnummer.ToString()
                    + " " + dasP.Projektname + " wirklich GELÖSCHT werden?", "LÖSCHEN", MessageBoxButtons.OKCancel);

                if (sollIch == DialogResult.OK)
                {
                    Helper.ProjekteHelper.deleteProjekt(dasP);
                }
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var tooltext = "Die nächste Bestellnummer ist : " + Environment.NewLine + Environment.NewLine;

            try
            {

                printumBestellung neueBestellung = new printumBestellung();
                neueBestellung.Bestellung_ID = neueBestellung.getNextBestellnr();

                tooltext += "   " + neueBestellung.BestellungIDText + Environment.NewLine + "   . ";

                MessageBox.Show(tooltext);

            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }
        }

        //private void toolTip2_nextNumber_Popup(object sender, PopupEventArgs e)
        //{
        //      //   
        //}
        }
}

