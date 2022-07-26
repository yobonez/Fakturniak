
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.utwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontrahentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produktToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sposóbPłatnościToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NowaFaktura = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.utwórzToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
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
            this.NowaFaktura.Location = new System.Drawing.Point(323, 207);
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
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Witaj, LOGIN!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NowaFaktura);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}

