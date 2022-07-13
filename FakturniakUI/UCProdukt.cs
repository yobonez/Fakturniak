using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace FakturniakUI
{
    public partial class UCProdukt : UserControl
    {
        public UCProdukt()
        {
            InitializeComponent();
        }

        #region Properties

        private int _id_produktu;
        private int _id_stawki;
        private int _id_jednostki;
        private string _nazwa;
        private Decimal _cena_netto;
        private Decimal _cena_brutto;
        private bool _last_focused;


        [Category ("Custom Props")]
        public int id_produktu
        {
            get { return _id_produktu; }
            set { _id_produktu = value; }
        }

        [Category("Custom Props")]
        public string nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; labelProduktUsluga.Text = value; }
        }

        [Category("Custom Props")]
        public Decimal cena_netto
        {
            get { return _cena_netto; }
            set { _cena_netto = value; labelNetto.Text = value.ToString(); }
        }

        [Category("Custom Props")]
        public Decimal cena_brutto
        {
            get { return _cena_brutto; }
            set { _cena_brutto = value; labelBrutto.Text = value.ToString(); }
        }

        [Category("Custom Props")]
        public bool last_focused
        {
            get { return _last_focused; }
            set { _last_focused = value; }
        }

        [Category("Custom Props")]
        public int id_stawki
        {
            get { return _id_stawki; }
            set { _id_stawki = value; }
        }

        [Category("Custom Props")]
        public int id_jednostki
        {
            get { return _id_jednostki; }
            set { _id_jednostki = value; }
        }
        #endregion Properties

        private void labelProduktUsluga_Click(object sender, System.EventArgs e)
        {
            this.Focus();
            this.last_focused = true;
            this.BackColor = SystemColors.HotTrack;
        }
        private void UCProdukt_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.last_focused = true;
            this.BackColor = SystemColors.HotTrack;
        }

        private void labelProduktUsluga_MouseEnter(object sender, System.EventArgs e)
        {
            if (this.Focused == false)
            {
                this.last_focused = false;
                this.BackColor = SystemColors.ButtonHighlight;
            }
        }

        private void labelProduktUsluga_MouseLeave(object sender, System.EventArgs e)
        {
            if (this.Focused == false)
            {
                this.last_focused = false;
                this.BackColor = SystemColors.ButtonFace;
            }
            else
            {
                foreach (UCProdukt control in Parent.Controls)
                {
                    if (control is UCProdukt)
                    {
                        control.BackColor = SystemColors.ButtonFace;
                        control.last_focused = false;
                    }
                }
                this.BackColor = SystemColors.ActiveCaption;
                this.last_focused = true;
            }
        }
        private void UCProdukt_MouseEnter(object sender, System.EventArgs e)
        {
            if (this.Focused == false)
            {
                this.BackColor = SystemColors.ButtonHighlight;
                this.last_focused = false;
            }
        }

        private void UCProdukt_MouseLeave(object sender, System.EventArgs e)
        {
            if (this.Focused == false)
            {
                this.BackColor = SystemColors.ButtonFace;
                this.last_focused = false;
            }
            else
            {
                foreach (UCProdukt control in Parent.Controls)
                {
                    if (control is UCProdukt)
                    {
                        control.BackColor = SystemColors.ButtonFace;
                        control.last_focused = false;
                    }
                }
                this.BackColor = SystemColors.ActiveCaption;
                this.last_focused = true;
            }
        }

        private void UCProdukt_DoubleClick(object sender, EventArgs e)
        {
            // TODO: dwuklik na user controlu odpalający metodę DodajDoFaktury() z FormFaktura
            /*
            Form formFakturaFind = this.FindForm();
            formFakturaFind.DodajDoFaktury();
            */
        }
    }
}
