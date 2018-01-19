using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;


namespace PrintumProjektverwaltung.Forms
{
    public partial class Form8_Adressen : Form
    {
        private List<Contact> alleAdressen;


        public Form8_Adressen()
        {
            InitializeComponent();
        }

        public Form8_Adressen(List<Contact> alleAdressen)
        {
            InitializeComponent();
            this.alleAdressen = alleAdressen;
        }

        private void Form8_Adressen_Load(object sender, EventArgs e)
        {

        }
    }
}
