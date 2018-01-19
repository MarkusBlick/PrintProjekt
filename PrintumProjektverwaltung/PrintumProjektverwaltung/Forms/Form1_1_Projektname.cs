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
    public partial class Form1_1_Projektname : Form
    {
        private object derTag;
        private printumProjekt dasP;

        public Form1_1_Projektname()
        {
            InitializeComponent();
        }

        public Form1_1_Projektname(object derTag)
        {
            InitializeComponent();

            this.derTag = derTag;

        }

        private void button1_speichern_Click(object sender, EventArgs e)
        {

            using (DAL.PrintumProjekteEntities db = new DAL.PrintumProjekteEntities())
            {
                var q = (from p in db.Projekte
                         where p.Projektnummer == dasP.Projektnummer
                         select p)
                        .FirstOrDefault();
                q.Projektname = this.textBox1.Text;
                db.SaveChanges();
                this.Close();
            }
        }

        private void Form1_1_Projektname_Load(object sender, EventArgs e)
        {
            Helper.LogHelper.WriteDebugLog("Projektname kann gändert werden");

            try
            {
                    this.dasP = (printumProjekt)this.derTag;
                    this.label1.Text = dasP.Projektnummer.ToString();
                    this.textBox1.Text = dasP.Projektname;
                }
            catch (Exception ex)
                {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }
        }
    }
}
