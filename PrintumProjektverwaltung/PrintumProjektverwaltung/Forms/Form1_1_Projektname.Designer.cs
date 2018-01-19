namespace PrintumProjektverwaltung.Forms
{
    partial class Form1_1_Projektname
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1_speichern = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 89);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(406, 29);
            this.textBox1.TabIndex = 1;
            // 
            // button1_speichern
            // 
            this.button1_speichern.BackColor = System.Drawing.Color.IndianRed;
            this.button1_speichern.FlatAppearance.BorderSize = 0;
            this.button1_speichern.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1_speichern.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.button1_speichern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_speichern.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1_speichern.Location = new System.Drawing.Point(34, 147);
            this.button1_speichern.Name = "button1_speichern";
            this.button1_speichern.Size = new System.Drawing.Size(180, 32);
            this.button1_speichern.TabIndex = 9;
            this.button1_speichern.Text = "speichern";
            this.button1_speichern.UseVisualStyleBackColor = false;
            this.button1_speichern.Click += new System.EventHandler(this.button1_speichern_Click);
            // 
            // Form1_1_Projektname
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(551, 218);
            this.Controls.Add(this.button1_speichern);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1_1_Projektname";
            this.ShowIcon = false;
            this.Text = "Projektname ändern";
            this.Load += new System.EventHandler(this.Form1_1_Projektname_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1_speichern;
    }
}