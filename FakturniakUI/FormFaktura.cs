using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.Data;

using Microsoft.Extensions.Configuration;

namespace FakturniakUI
{
    public partial class FormFaktura : Form
    {
        readonly ISqlDataAccess dataAccess = new SqlDataAccess();

        public FormFaktura()
        {
            InitializeComponent();
        }

        private void FormFaktura_Load(object sender, EventArgs e)
        { 
        }

        private async void Wystaw_Click(object sender, EventArgs e)
        {
            // TODO: wyrzuć taski, to było wg tutoriala do minimal api, a my mamy WinForms

            IDataFaktury dataKontrahenci = new DataFaktury(dataAccess);
            Task<ModelFaktura> kontrahent = dataKontrahenci.LoadFaktura("FV 002/07-2022");

            string numer = "";

            await Task.Run(() => { numer = kontrahent.Result.miejsce_wystawienia; });

            textBox1.Text = numer;
        }
    }
}
