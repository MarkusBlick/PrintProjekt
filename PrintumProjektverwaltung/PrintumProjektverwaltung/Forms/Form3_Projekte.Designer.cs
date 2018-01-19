namespace PrintumProjektverwaltung.Forms
{
    partial class Form3_Projekte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3_Projekte));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1_Projektnummer = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1_Projekterstellen = new System.Windows.Forms.Button();
            this.panel_unten = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label_Datum = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label_user = new System.Windows.Forms.Label();
            this.panel_balkenoben = new System.Windows.Forms.Panel();
            this.panel_balkenUnten = new System.Windows.Forms.Panel();
            this.panel1_header = new System.Windows.Forms.Panel();
            this.label_ganzOben = new System.Windows.Forms.Label();
            this.panelOb_balkenoben = new System.Windows.Forms.Panel();
            this.panelOb_balkenUnten = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1_beginn = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2_lieferung = new System.Windows.Forms.DateTimePicker();
            this.panel_unten.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1_header.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projektnummer:";
            // 
            // textBox1_Projektnummer
            // 
            this.textBox1_Projektnummer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1_Projektnummer.Location = new System.Drawing.Point(152, 99);
            this.textBox1_Projektnummer.Name = "textBox1_Projektnummer";
            this.textBox1_Projektnummer.Size = new System.Drawing.Size(89, 19);
            this.textBox1_Projektnummer.TabIndex = 4;
            this.textBox1_Projektnummer.TabStop = false;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(152, 159);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(377, 26);
            this.textBox_Name.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1_Projekterstellen
            // 
            this.button1_Projekterstellen.BackColor = System.Drawing.Color.IndianRed;
            this.button1_Projekterstellen.FlatAppearance.BorderSize = 0;
            this.button1_Projekterstellen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1_Projekterstellen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.button1_Projekterstellen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_Projekterstellen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1_Projekterstellen.Location = new System.Drawing.Point(378, 371);
            this.button1_Projekterstellen.Name = "button1_Projekterstellen";
            this.button1_Projekterstellen.Size = new System.Drawing.Size(151, 32);
            this.button1_Projekterstellen.TabIndex = 3;
            this.button1_Projekterstellen.Text = "Projekt erstellen";
            this.button1_Projekterstellen.UseVisualStyleBackColor = false;
            this.button1_Projekterstellen.Click += new System.EventHandler(this.button1_Projekterstellen_Click);
            // 
            // panel_unten
            // 
            this.panel_unten.Controls.Add(this.panel8);
            this.panel_unten.Controls.Add(this.label_user);
            this.panel_unten.Controls.Add(this.panel_balkenoben);
            this.panel_unten.Controls.Add(this.panel_balkenUnten);
            this.panel_unten.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_unten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.panel_unten.Location = new System.Drawing.Point(0, 437);
            this.panel_unten.Name = "panel_unten";
            this.panel_unten.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel_unten.Size = new System.Drawing.Size(544, 25);
            this.panel_unten.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label_Datum);
            this.panel8.Controls.Add(this.pictureBox2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(261, 1);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 1, 0, 2);
            this.panel8.Size = new System.Drawing.Size(283, 23);
            this.panel8.TabIndex = 5;
            // 
            // label_Datum
            // 
            this.label_Datum.AutoSize = true;
            this.label_Datum.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_Datum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label_Datum.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_Datum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Datum.Location = new System.Drawing.Point(0, 1);
            this.label_Datum.Name = "label_Datum";
            this.label_Datum.Size = new System.Drawing.Size(0, 16);
            this.label_Datum.TabIndex = 4;
            this.label_Datum.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(171, 1);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(112, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label_user
            // 
            this.label_user.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label_user.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_user.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_user.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_user.Location = new System.Drawing.Point(8, 4);
            this.label_user.Margin = new System.Windows.Forms.Padding(0);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(309, 13);
            this.label_user.TabIndex = 3;
            this.label_user.Text = "© 2017";
            // 
            // panel_balkenoben
            // 
            this.panel_balkenoben.BackColor = System.Drawing.SystemColors.Control;
            this.panel_balkenoben.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_balkenoben.Location = new System.Drawing.Point(0, 0);
            this.panel_balkenoben.Name = "panel_balkenoben";
            this.panel_balkenoben.Size = new System.Drawing.Size(544, 1);
            this.panel_balkenoben.TabIndex = 0;
            // 
            // panel_balkenUnten
            // 
            this.panel_balkenUnten.BackColor = System.Drawing.SystemColors.Control;
            this.panel_balkenUnten.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_balkenUnten.Location = new System.Drawing.Point(0, 24);
            this.panel_balkenUnten.Name = "panel_balkenUnten";
            this.panel_balkenUnten.Size = new System.Drawing.Size(544, 1);
            this.panel_balkenUnten.TabIndex = 1;
            // 
            // panel1_header
            // 
            this.panel1_header.Controls.Add(this.label_ganzOben);
            this.panel1_header.Controls.Add(this.panelOb_balkenoben);
            this.panel1_header.Controls.Add(this.panelOb_balkenUnten);
            this.panel1_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1_header.Location = new System.Drawing.Point(0, 0);
            this.panel1_header.Name = "panel1_header";
            this.panel1_header.Padding = new System.Windows.Forms.Padding(5);
            this.panel1_header.Size = new System.Drawing.Size(544, 46);
            this.panel1_header.TabIndex = 8;
            // 
            // label_ganzOben
            // 
            this.label_ganzOben.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_ganzOben.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label_ganzOben.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_ganzOben.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_ganzOben.Location = new System.Drawing.Point(5, 6);
            this.label_ganzOben.Margin = new System.Windows.Forms.Padding(0);
            this.label_ganzOben.Name = "label_ganzOben";
            this.label_ganzOben.Size = new System.Drawing.Size(534, 34);
            this.label_ganzOben.TabIndex = 4;
            this.label_ganzOben.Text = "Neues Projekte erstellen";
            // 
            // panelOb_balkenoben
            // 
            this.panelOb_balkenoben.BackColor = System.Drawing.SystemColors.Control;
            this.panelOb_balkenoben.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOb_balkenoben.Location = new System.Drawing.Point(5, 5);
            this.panelOb_balkenoben.Name = "panelOb_balkenoben";
            this.panelOb_balkenoben.Size = new System.Drawing.Size(534, 1);
            this.panelOb_balkenoben.TabIndex = 3;
            // 
            // panelOb_balkenUnten
            // 
            this.panelOb_balkenUnten.BackColor = System.Drawing.SystemColors.Control;
            this.panelOb_balkenUnten.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOb_balkenUnten.Location = new System.Drawing.Point(5, 40);
            this.panelOb_balkenUnten.Name = "panelOb_balkenUnten";
            this.panelOb_balkenUnten.Size = new System.Drawing.Size(534, 1);
            this.panelOb_balkenUnten.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Inbetriebname:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Produktionsbeginn:";
            // 
            // dateTimePicker1_beginn
            // 
            this.dateTimePicker1_beginn.Location = new System.Drawing.Point(186, 235);
            this.dateTimePicker1_beginn.Name = "dateTimePicker1_beginn";
            this.dateTimePicker1_beginn.Size = new System.Drawing.Size(343, 26);
            this.dateTimePicker1_beginn.TabIndex = 1;
            // 
            // dateTimePicker2_lieferung
            // 
            this.dateTimePicker2_lieferung.Location = new System.Drawing.Point(186, 307);
            this.dateTimePicker2_lieferung.Name = "dateTimePicker2_lieferung";
            this.dateTimePicker2_lieferung.Size = new System.Drawing.Size(343, 26);
            this.dateTimePicker2_lieferung.TabIndex = 2;
            // 
            // Form3_Projekte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(544, 462);
            this.Controls.Add(this.dateTimePicker2_lieferung);
            this.Controls.Add(this.dateTimePicker1_beginn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1_header);
            this.Controls.Add(this.textBox1_Projektnummer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1_Projekterstellen);
            this.Controls.Add(this.panel_unten);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form3_Projekte";
            this.Text = "neues Projekt";
            this.Load += new System.EventHandler(this.Form3_Projekte_Load);
            this.panel_unten.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1_header.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1_Projektnummer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1_Projekterstellen;
        private System.Windows.Forms.Panel panel_unten;
        private System.Windows.Forms.Panel panel8;
        public System.Windows.Forms.Label label_Datum;
        public System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label_user;
        private System.Windows.Forms.Panel panel_balkenoben;
        private System.Windows.Forms.Panel panel_balkenUnten;
        private System.Windows.Forms.Panel panel1_header;
        protected System.Windows.Forms.Label label_ganzOben;
        private System.Windows.Forms.Panel panelOb_balkenoben;
        private System.Windows.Forms.Panel panelOb_balkenUnten;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1_beginn;
        private System.Windows.Forms.DateTimePicker dateTimePicker2_lieferung;
    }
}