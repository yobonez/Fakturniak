
namespace FakturniakUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.utwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontrahentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produktToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sposóbPłatnościToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NowaFaktura = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusBarGodzina = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.buttonPokazFaktury = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.utwórzToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // utwórzToolStripMenuItem
            // 
            this.utwórzToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kontrahentaToolStripMenuItem,
            this.produktToolStripMenuItem,
            this.sposóbPłatnościToolStripMenuItem});
            this.utwórzToolStripMenuItem.Name = "utwórzToolStripMenuItem";
            this.utwórzToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.utwórzToolStripMenuItem.Text = "Utwórz";
            // 
            // kontrahentaToolStripMenuItem
            // 
            this.kontrahentaToolStripMenuItem.Name = "kontrahentaToolStripMenuItem";
            this.kontrahentaToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.kontrahentaToolStripMenuItem.Text = "Kontrahenta";
            this.kontrahentaToolStripMenuItem.Click += new System.EventHandler(this.kontrahentaToolStripMenuItem_Click);
            // 
            // produktToolStripMenuItem
            // 
            this.produktToolStripMenuItem.Name = "produktToolStripMenuItem";
            this.produktToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.produktToolStripMenuItem.Text = "Produkt";
            this.produktToolStripMenuItem.Click += new System.EventHandler(this.produktToolStripMenuItem_Click);
            // 
            // sposóbPłatnościToolStripMenuItem
            // 
            this.sposóbPłatnościToolStripMenuItem.Name = "sposóbPłatnościToolStripMenuItem";
            this.sposóbPłatnościToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.sposóbPłatnościToolStripMenuItem.Text = "Sposób płatności";
            this.sposóbPłatnościToolStripMenuItem.Click += new System.EventHandler(this.sposóbPłatnościToolStripMenuItem_Click);
            // 
            // NowaFaktura
            // 
            this.NowaFaktura.Location = new System.Drawing.Point(328, 207);
            this.NowaFaktura.Name = "NowaFaktura";
            this.NowaFaktura.Size = new System.Drawing.Size(109, 37);
            this.NowaFaktura.TabIndex = 1;
            this.NowaFaktura.Text = "Nowa faktura";
            this.NowaFaktura.UseVisualStyleBackColor = true;
            this.NowaFaktura.Click += new System.EventHandler(this.NowaFaktura_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Witaj, LOGIN!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(37, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 80);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(279, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 59);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Przychody (ogółem)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Czekaj...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.toolStripStatusLabel1,
            this.toolStripStatusBarGodzina});
            this.statusStrip1.Location = new System.Drawing.Point(0, 263);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(489, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(48, 17);
            this.status.Text = "Gotowy";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(370, 17);
            this.toolStripStatusLabel1.Text = "                                                                                 " +
    "                                        ";
            // 
            // toolStripStatusBarGodzina
            // 
            this.toolStripStatusBarGodzina.Name = "toolStripStatusBarGodzina";
            this.toolStripStatusBarGodzina.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusBarGodzina.Text = "Czekaj...";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // buttonPokazFaktury
            // 
            this.buttonPokazFaktury.Location = new System.Drawing.Point(37, 207);
            this.buttonPokazFaktury.Name = "buttonPokazFaktury";
            this.buttonPokazFaktury.Size = new System.Drawing.Size(109, 37);
            this.buttonPokazFaktury.TabIndex = 6;
            this.buttonPokazFaktury.Text = "Pokaż faktury";
            this.buttonPokazFaktury.UseVisualStyleBackColor = true;
            this.buttonPokazFaktury.Click += new System.EventHandler(this.buttonPokazFaktury_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 285);
            this.Controls.Add(this.buttonPokazFaktury);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NowaFaktura);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Fakturniak";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button NowaFaktura;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem utwórzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kontrahentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem produktToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sposóbPłatnościToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusBarGodzina;
        private System.Windows.Forms.Button buttonPokazFaktury;
    }
}

