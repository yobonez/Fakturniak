
namespace FakturniakUI
{
    partial class FormSzukaj
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
            this.dataGridViewSzukaj = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwisko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodPocztowy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Miasto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PESEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KRS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REGON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonWybierz = new System.Windows.Forms.Button();
            this.textBoxSzukaj = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSzukaj)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSzukaj
            // 
            this.dataGridViewSzukaj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSzukaj.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nazwa,
            this.Imie,
            this.Nazwisko,
            this.Adres,
            this.KodPocztowy,
            this.Miasto,
            this.email,
            this.Telefon,
            this.PESEL,
            this.NIP,
            this.KRS,
            this.REGON});
            this.dataGridViewSzukaj.Location = new System.Drawing.Point(12, 35);
            this.dataGridViewSzukaj.Name = "dataGridViewSzukaj";
            this.dataGridViewSzukaj.RowTemplate.Height = 25;
            this.dataGridViewSzukaj.Size = new System.Drawing.Size(810, 353);
            this.dataGridViewSzukaj.TabIndex = 0;
            this.dataGridViewSzukaj.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKontrahenci_CellClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Nazwa
            // 
            this.Nazwa.HeaderText = "Nazwa";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            // 
            // Imie
            // 
            this.Imie.HeaderText = "Imie";
            this.Imie.Name = "Imie";
            this.Imie.ReadOnly = true;
            // 
            // Nazwisko
            // 
            this.Nazwisko.HeaderText = "Nazwisko";
            this.Nazwisko.Name = "Nazwisko";
            this.Nazwisko.ReadOnly = true;
            // 
            // Adres
            // 
            this.Adres.HeaderText = "Adres";
            this.Adres.Name = "Adres";
            this.Adres.ReadOnly = true;
            // 
            // KodPocztowy
            // 
            this.KodPocztowy.HeaderText = "Kod pocztowy";
            this.KodPocztowy.Name = "KodPocztowy";
            this.KodPocztowy.ReadOnly = true;
            // 
            // Miasto
            // 
            this.Miasto.HeaderText = "Miasto";
            this.Miasto.Name = "Miasto";
            this.Miasto.ReadOnly = true;
            // 
            // email
            // 
            this.email.HeaderText = "e-mail";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // Telefon
            // 
            this.Telefon.HeaderText = "Telefon";
            this.Telefon.Name = "Telefon";
            this.Telefon.ReadOnly = true;
            // 
            // PESEL
            // 
            this.PESEL.HeaderText = "PESEL";
            this.PESEL.Name = "PESEL";
            this.PESEL.ReadOnly = true;
            // 
            // NIP
            // 
            this.NIP.HeaderText = "NIP";
            this.NIP.Name = "NIP";
            this.NIP.ReadOnly = true;
            // 
            // KRS
            // 
            this.KRS.HeaderText = "KRS";
            this.KRS.Name = "KRS";
            this.KRS.ReadOnly = true;
            // 
            // REGON
            // 
            this.REGON.HeaderText = "REGON";
            this.REGON.Name = "REGON";
            this.REGON.ReadOnly = true;
            // 
            // buttonWybierz
            // 
            this.buttonWybierz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWybierz.Location = new System.Drawing.Point(356, 397);
            this.buttonWybierz.Name = "buttonWybierz";
            this.buttonWybierz.Size = new System.Drawing.Size(122, 35);
            this.buttonWybierz.TabIndex = 1;
            this.buttonWybierz.Text = "Wybierz";
            this.buttonWybierz.UseVisualStyleBackColor = true;
            this.buttonWybierz.Click += new System.EventHandler(this.buttonWybierz_Click);
            // 
            // textBoxSzukaj
            // 
            this.textBoxSzukaj.Location = new System.Drawing.Point(329, 6);
            this.textBoxSzukaj.Name = "textBoxSzukaj";
            this.textBoxSzukaj.Size = new System.Drawing.Size(168, 23);
            this.textBoxSzukaj.TabIndex = 12;
            this.textBoxSzukaj.Text = "Szukaj...";
            this.textBoxSzukaj.TextChanged += new System.EventHandler(this.textBoxSzukaj_TextChanged);
            // 
            // FormSzukaj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 444);
            this.Controls.Add(this.textBoxSzukaj);
            this.Controls.Add(this.buttonWybierz);
            this.Controls.Add(this.dataGridViewSzukaj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormSzukaj";
            this.Text = "FormSzukaj";
            this.Load += new System.EventHandler(this.FormSzukaj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSzukaj)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSzukaj;
        private System.Windows.Forms.Button buttonWybierz;
        private System.Windows.Forms.TextBox textBoxSzukaj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwisko;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adres;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodPocztowy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Miasto;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefon;
        private System.Windows.Forms.DataGridViewTextBoxColumn PESEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn NIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn KRS;
        private System.Windows.Forms.DataGridViewTextBoxColumn REGON;
    }
}