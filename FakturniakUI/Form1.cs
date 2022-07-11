using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakturniakUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void NowaFaktura_Click(object sender, EventArgs e)
        {
            FormFaktura formFaktura = new FormFaktura();
            formFaktura.Show();
        }
    }
}
