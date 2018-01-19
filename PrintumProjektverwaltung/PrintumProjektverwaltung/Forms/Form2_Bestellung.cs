using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using PrintumProjektverwaltung.Models;

using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Exchange.WebServices.Data;

namespace PrintumProjektverwaltung.Forms
{
    public partial class Form2_Bestellung : Form
    {
        private printumBestellung neueBestellung;
        private List<Contact> alleAdressen;


        public Form2_Bestellung()
        {
            InitializeComponent();
        }

        public Form2_Bestellung(printumBestellung neueBestellung, List<Contact> alleAdressen)
        {
            InitializeComponent();


            this.neueBestellung = neueBestellung;

            this.alleAdressen = alleAdressen;

            this.label1.Text = alleAdressen.Count().ToString() + " Adressen aus 'Info PRINTUM'";

            this.label_ganzOben.Text += " " + neueBestellung.BestellungIDText + "                    an: " + neueBestellung.ProjektnummerText;
        }

        private void Form2_Bestellung_Load(object sender, EventArgs e)
        {

            this.bestellungenTableAdapter1.Fill(this.dataSet1.Bestellungen);

            //  die Outlookadressen werden auseglesen  
            dieInfoAdressenAuslesen();

            this.adressenDataGridView.ClearSelection();
            this.bestellungenDataGridView.ClearSelection();
        }

        private void dieInfoAdressenAuslesen()
        {
            this.dataSet1.Adressen.Clear();

            foreach (var adr in this.alleAdressen)
            {
                DAL.DataSet1.AdressenRow row = this.dataSet1.Adressen.NewAdressenRow();

                try
                {
                    string nachname = (adr.Surname == null) ? "" : adr.Surname;
                    string vorname = (adr.GivenName == null) ? "" : adr.GivenName;

                    row.Name = (vorname.Count() > 0) ? vorname + " " + nachname : nachname;

                    row.Firmenname = (adr.CompanyName == null) ? "" : adr.CompanyName;


                    row.businessHomePage = (adr.BusinessHomePage == null) ? "" : adr.BusinessHomePage;
                    row.FileAs = (adr.FileAs == null) ? "" : adr.FileAs;
                    row.ID = adr.Id.UniqueId;
                    row.JobTitle = (adr.JobTitle == null) ? "" : adr.JobTitle;

                    if (adr.DisplayName != null)
                    {
                        adr.DisplayName = (adr.GivenName == null) ? "" : adr.GivenName + " ";
                        adr.DisplayName += (adr.Surname == null) ? "" : adr.Surname;
                    }


                    row.Matchcode = row.Firmenname + ", " + "(" + row.Name + "), ";

                    PhysicalAddressEntry entry;
                    PhysicalAddressKey key = PhysicalAddressKey.Business;
                    if (adr.PhysicalAddresses.TryGetValue(key, out entry))
                    {
                        row.Strasse = (entry.Street == null) ? "" : entry.Street;
                        row.Ort = (entry.City == null) ? "" : entry.City;
                        row.PLZ = (entry.PostalCode == null) ? "" : entry.PostalCode;
                        row.Land = (entry.CountryOrRegion == null) ? "" : entry.CountryOrRegion;
                        row.PLZundORT = row.PLZ + " " + row.Ort;

                        row.Matchcode += row.PLZundORT;
                    }



                    EmailAddress eAdress1;
                    EmailAddressKey eKey1 = EmailAddressKey.EmailAddress1;
                    if (adr.EmailAddresses.TryGetValue(eKey1, out eAdress1))
                    {
                        row.Email1Adresse = eAdress1.Address;
                    }
                    else
                    {
                        row.Email1Adresse = "";
                    }

                    EmailAddress eAdress2;
                    EmailAddressKey eKey2 = EmailAddressKey.EmailAddress2;
                    if (adr.EmailAddresses.TryGetValue(eKey2, out eAdress2))
                    {
                        row.Email2Adresse = eAdress2.Address;
                    }
                    else
                    {
                        row.Email2Adresse = "";
                    }
                    StringList _categories = (adr.Categories == null) ? new StringList() : adr.Categories;

                }
                catch (Exception ex)
                {
                    var bla = ex.ToString();
                }


                this.dataSet1.Adressen.AddAdressenRow(row);

            }

        }


        private void adressenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var datarow = this.adressenDataGridView.CurrentRow;

            DAL.DataSet1.AdressenRow row = (DAL.DataSet1.AdressenRow)((DataRowView)datarow.DataBoundItem).Row;

            string txt = "AdressID: " + row.AdressID + Environment.NewLine;
            txt += " Firmenname: " + row.Firmenname + Environment.NewLine;
            //  txt += "ID: " + row.ID + Environment.NewLine;
            txt += "Name: " + row.Name + Environment.NewLine;
            txt += "JobTitle: " + row.JobTitle + Environment.NewLine;
            //         txt += "Ort: " + row.Ort + Environment.NewLine;
            txt += "Strasse: " + row.Strasse + Environment.NewLine;
            //          txt += "PLZ: " + row.PLZ + Environment.NewLine;
            txt += "PLZundORT: " + row.PLZundORT + Environment.NewLine;
            txt += "Land: " + row.Land + Environment.NewLine;
            txt += "Email1Adresse: " + row.Email1Adresse + Environment.NewLine;
            txt += "ZuHaendenVon: " + row.ZuHaendenVon + Environment.NewLine;
            txt += "Telefon1: " + row.Telefon1 + Environment.NewLine;
            txt += "Matchcode: " + row.Matchcode + Environment.NewLine;

            MessageBox.Show(txt);
        }


        private void button1_ausAlterBestellung_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            bool isAdresseSelected = this.adressenDataGridView.SelectedRows.Count == 1;
            bool isBestellungSelected = this.bestellungenDataGridView.SelectedRows.Count == 1;

            if (isAdresseSelected && isBestellungSelected)
            {
                var datarow = this.adressenDataGridView.CurrentRow;
                DAL.DataSet1.AdressenRow row = (DAL.DataSet1.AdressenRow)((DataRowView)datarow.DataBoundItem).Row;

                var adressRow = this.adressenDataGridView.CurrentRow;

                neueBestellung.Adresse = Helper.Jsons.datagridrowToJson(adressRow);

                neueBestellung.ErstelltVon = Environment.UserName;
                neueBestellung.Datum = DateTime.Now;
                neueBestellung.Firmenname = row.Firmenname;
                neueBestellung.Email1Adresse = row.Email1Adresse == null ? "" : row.Email1Adresse;
                neueBestellung.AdressID = row.ID;

                //  neueBestellung.Email2Adresse = row.Email2Adresse == null ? "" : row.Email2Adresse;
                //adressRow.Cells[0].Value.ToString();

                string dieVorlage = this.bestellungenDataGridView.CurrentRow.Cells["speicherortDataGridViewTextBoxColumn"].Value.ToString();

                if (File.Exists(dieVorlage))
                {
                    // hier wird die Bestellung auch gespeichert
                    Helper.ExcelHelper.createNewExcelFromOldone(neueBestellung, dieVorlage);
                    System.Diagnostics.Process.Start(neueBestellung.Speicherort);


                }
                else
                {
                    MessageBox.Show("Die Vorlage existiert nicht (mehr).");
                }



                this.Close();
            }

            else
            {
                MessageBox.Show("Bitte eine Adresse UND eine alte Bestellung auswählen");
            }

            this.Cursor = Cursors.Default;
        }

        private void button2_neueBestellung_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            bool isAdresseSelected = this.adressenDataGridView.SelectedRows.Count == 1;
            if (isAdresseSelected)
            {
                var adressRow = this.adressenDataGridView.CurrentRow;
                DAL.DataSet1.AdressenRow row = (DAL.DataSet1.AdressenRow)((DataRowView)adressRow.DataBoundItem).Row;


                neueBestellung.Adresse = Helper.Jsons.datagridrowToJson(adressRow);

                neueBestellung.ErstelltVon = Environment.UserName;
                neueBestellung.Datum = DateTime.Now;
                neueBestellung.Firmenname = row.Firmenname;

                neueBestellung.Email1Adresse = row.Email1Adresse == null ? "" : row.Email1Adresse;
                //  neueBestellung.Email2Adresse = row.Email2Adresse == null ? "" : row.Email2Adresse;
                neueBestellung.AdressID = row.ID;

                Helper.ExcelHelper.createNewExcel(neueBestellung);

                // Excel öffnen
                System.Diagnostics.Process.Start(neueBestellung.Speicherort);
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte eine Adresse auswählen");
            }


            this.Cursor = Cursors.Default;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string suchtext = this.textBox1.Text.Trim();


            string filtertext1 = "Firmenname LIKE '*" + suchtext + "*'";
            string filtertext2 = "PLZundORT LIKE '*" + suchtext + "*'";
            string filtertext3 = "Name LIKE '*" + suchtext + "*'";
            string filtertext4 = "Land LIKE '*" + suchtext + "*'";

            this.adressenBindingSource.Filter =
                                                filtertext1 + " OR " +
                                                filtertext2 + " OR " +
                                                filtertext3 + " OR " +
                                                filtertext4;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string suchtext = this.textBox2.Text.Trim();


            string filtertext1 = "BestellungIDText LIKE '*" + suchtext + "*'";
            string filtertext2 = "Adresse LIKE '*" + suchtext + "*'";
            string filtertext3 = "ErstelltVon LIKE '*" + suchtext + "*'";
            string filtertext4 = "CONVERT(Projektnummer, 'System.String') LIKE '*" + suchtext + "*'";

            this.bestellungenBindingSource.Filter =
                                                filtertext1 + " OR " +
                                                filtertext2 + " OR " +
                                                filtertext3 + " OR " +
                                                  filtertext4;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.alleAdressen = (Application.OpenForms["Form1_main"] as Form1_main).alleAdressen;
            dieInfoAdressenAuslesen();
            this.label1.Text = alleAdressen.Count().ToString() + " Adressen aus 'Info PRINTUM'";

        }
    }
}
