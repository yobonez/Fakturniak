//  Copyright (C) 2022 Jacek Gałuszka
/*
    This file is part of Fakturniak.

    Fakturniak is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 3 of the License, or
    (at your option) any later version.

    Fakturniak is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Fakturniak.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using FakturniakDataAccess.Data;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.Status;
using FakturniakUI.Config;

namespace FakturniakUI
{
    public partial class Form1 : Form
    {
        IEnumerable<ModelPrzychody> _przychody;
        IDataPrzychody dataPrzychody;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FakturniakConfig.Load("FakturniakConfig.xml");

            if (FakturniakConfig.xmlFakturniakConfig.id_zarejestrowany > 0)
            {
                using (FormLogowanie flogowanie = new FormLogowanie())
                {
                    flogowanie.ShowDialog();
                }
            }

            else
            {
                using (FormRejestracja frejestracja = new FormRejestracja())
                {
                    frejestracja.ShowDialog();
                }
            }

            this.CenterToScreen();

            FakturniakStatus.refreshMenu = true;
            timer1.Enabled = true;
            timer2.Enabled = true;
            pictureBox1.ImageLocation = FakturniakConfig.xmlFakturniakConfig.logo_path;
            label1.Text = "Witaj, " + FakturniakConfig.xmlFakturniakConfig.ostatni_zalogowany_uzytkownik + "!";
        }

        private void NowaFaktura_Click(object sender, EventArgs e)
        {
            FormFaktura formFaktura = new FormFaktura();
            formFaktura.Show();
        }

        private void produktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNowyProdukt formProdutkt = new FormNowyProdukt();
            formProdutkt.Show();
        }

        private void kontrahentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNowyKontrahent formKontrahent = new FormNowyKontrahent();
            formKontrahent.Show();
        }

        private void sposóbPłatnościToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNowySposobPlatnosci formNowySposobPlatnosci = new FormNowySposobPlatnosci();
            formNowySposobPlatnosci.Show();
        }
        public async void RefreshMenu(SqlDataAccess dataAccess)
        {
            dataPrzychody = new DataPrzychody(dataAccess);

            await Task.Run(() => _przychody = dataPrzychody.Get().Result);

            Decimal decimalPrzychody = 0.0M;
            foreach (ModelPrzychody przychod in _przychody)
            {
                decimalPrzychody = decimalPrzychody + przychod.Suma;
            }

            label2.Text = decimalPrzychody.ToString() + " PLN";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (FakturniakStatus.zapytanie) { status.Text = "Wysyłam zapytanie do bazy...";  timer1.Interval = 350; }
            else { status.Text = "Gotowy"; timer1.Interval = 1;};
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            toolStripStatusBarGodzina.Text = DateTime.Now.ToShortTimeString();
            if (FakturniakStatus.refreshMenu)
            {
                ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);
                RefreshMenu((SqlDataAccess)dataAccess);

                FakturniakStatus.refreshMenu = false;
            }
        }
    }
}
