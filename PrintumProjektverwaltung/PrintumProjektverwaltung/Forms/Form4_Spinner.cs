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
    public partial class Form4_Spinner : Form
    {
        public Form4_Spinner()
        {
            InitializeComponent();

            timer1.Start();
                timer2.Start();
            timer3.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.label1.Text = "Excel wird geöffnet..., Outlook auch ... ";
            timer2.Stop();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.label1.Text = "Excel wird geöffnet..., Outlook auch ... das dauert halt...";
        }
    }
}
