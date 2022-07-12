
namespace FakturniakUI
{
    partial class UCProdukt
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelProduktUsluga = new System.Windows.Forms.Label();
            this.labelNetto = new System.Windows.Forms.Label();
            this.labelBrutto = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelProduktUsluga
            // 
            this.labelProduktUsluga.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProduktUsluga.AutoEllipsis = true;
            this.labelProduktUsluga.Location = new System.Drawing.Point(1, -1);
            this.labelProduktUsluga.Name = "labelProduktUsluga";
            this.labelProduktUsluga.Padding = new System.Windows.Forms.Padding(2);
            this.labelProduktUsluga.Size = new System.Drawing.Size(250, 19);
            this.labelProduktUsluga.TabIndex = 0;
            this.labelProduktUsluga.Text = "Przykładowy produkt/usługa";
            this.labelProduktUsluga.Click += new System.EventHandler(this.labelProduktUsluga_Click);
            // 
            // labelNetto
            // 
            this.labelNetto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNetto.AutoEllipsis = true;
            this.labelNetto.Location = new System.Drawing.Point(263, 0);
            this.labelNetto.Name = "labelNetto";
            this.labelNetto.Size = new System.Drawing.Size(34, 19);
            this.labelNetto.TabIndex = 1;
            this.labelNetto.Text = "50.00";
            // 
            // labelBrutto
            // 
            this.labelBrutto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBrutto.AutoEllipsis = true;
            this.labelBrutto.Location = new System.Drawing.Point(341, 0);
            this.labelBrutto.Name = "labelBrutto";
            this.labelBrutto.Size = new System.Drawing.Size(34, 19);
            this.labelBrutto.TabIndex = 2;
            this.labelBrutto.Text = "62.50";
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(255, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(2, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "label9";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(333, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(2, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            // 
            // UCProdukt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelBrutto);
            this.Controls.Add(this.labelNetto);
            this.Controls.Add(this.labelProduktUsluga);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCProdukt";
            this.Size = new System.Drawing.Size(408, 17);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelProduktUsluga;
        private System.Windows.Forms.Label labelNetto;
        private System.Windows.Forms.Label labelBrutto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
    }
}
