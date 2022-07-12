using System.ComponentModel;
using System.Windows.Forms;

namespace FakturniakUI
{
    public partial class UCProdukt : UserControl
    {
        public UCProdukt()
        {
            InitializeComponent();
        }

        #region Properties

        private string _nazwa;
        private float _cena_netto;
        private float _cena_brutto;

        [Category("Custom Props")]
        public string nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; labelProduktUsluga.Text = value; }
        }

        [Category("Custom Props")]
        public float cena_netto
        {
            get { return _cena_netto; }
            set { _cena_netto = value; labelBrutto.Text = value.ToString(); }
        }

        [Category("Custom Props")]
        public float cena_brutto
        {
            get { return _cena_brutto; }
            set { _cena_brutto = value; labelNetto.Text = value.ToString(); }
        }

        #endregion Properties

        private void labelProduktUsluga_Click(object sender, System.EventArgs e)
        {

        }
    }
}
