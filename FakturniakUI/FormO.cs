using System;
using System.Windows.Forms;

namespace FakturniakUI
{
    public partial class FormO : Form
    {
        public FormO()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
