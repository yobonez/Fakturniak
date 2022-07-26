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

using FakturniakDataAccess.Data;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakturniakUI
{
    public partial class FormNowyProdukt : Form
    {
        ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);
        List<ModelJednostkaMiary> jednostki = new List<ModelJednostkaMiary>();
        List<ModelStawkaVAT> stawkiVAT = new List<ModelStawkaVAT>();
        public FormNowyProdukt()
        {
            InitializeComponent();
        }

        private async void FormNowyProdukt_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = FakturniakConfig.xmlFakturniakConfig.logo_path;
            this.CenterToParent();

            IDataJednostkiMiary dataJednostkiMiary = new DataJednostkiMiary(dataAccess);
            IEnumerable<ModelJednostkaMiary> temp_jednostki = new List<ModelJednostkaMiary>();
            await Task.Run(() => temp_jednostki = dataJednostkiMiary.Get().Result);

            IDataStawkiVAT dataStawkiVAT = new DataStawkiVAT(dataAccess);
            IEnumerable<ModelStawkaVAT> temp_stawki = new List<ModelStawkaVAT>();
            await Task.Run(() => temp_stawki = dataStawkiVAT.Get().Result);

            jednostki = temp_jednostki.ToList();
            stawkiVAT = temp_stawki.ToList();

            foreach(ModelJednostkaMiary jednostka in jednostki)
            {
                comboBox1.Items.Add(jednostka.nazwa);
            }

            foreach(ModelStawkaVAT stawkaVAT in stawkiVAT)
            {
                comboBox2.Items.Add(stawkaVAT.wartosc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModelProdukt produkt = new ModelProdukt();

            string nazwa = textBox1.Text;
            int id_jednostka = 0;
            int id_stawkaVAT = 0;
            foreach(ModelJednostkaMiary jednostka in jednostki)
            {
                if (comboBox1.Text == jednostka.nazwa)
                    id_jednostka = jednostka.id_jednostki;
            }

            foreach (ModelStawkaVAT stawkaVAT in stawkiVAT)
            {
                if (comboBox2.Text == stawkaVAT.wartosc.ToString())
                    id_stawkaVAT = stawkaVAT.id_stawki;
            }

            produkt.nazwa = nazwa;
            if (comboBox3.Text == "netto") produkt.cena_netto = Decimal.Parse(textBox2.Text);
            else if (comboBox3.Text == "brutto") produkt.cena_brutto = Decimal.Parse(textBox2.Text);
            else MessageBox.Show(this, "Proszę wybrać, czy produkt dodać do bazy po cenie netto, czy brutto.\nBaza sama obliczy drugą cenę w zależności od wpisanych danych.", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            produkt.id_jednostki = id_jednostka;
            produkt.id_stawki = id_stawkaVAT;

            IDataProdukty dataProdukty = new DataProdukty(dataAccess);
            if (comboBox3.Text == "netto")
            {
                dataProdukty.InsertNetto(produkt);
                MessageBox.Show(this, $"Pomyślnie wstawiono produkt \"{produkt.nazwa}\" do bazy.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox3.Text == "brutto")
            {
                dataProdukty.InsertBrutto(produkt);
                MessageBox.Show(this, $"Pomyślnie wstawiono produkt \"{produkt.nazwa}\" do bazy.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
