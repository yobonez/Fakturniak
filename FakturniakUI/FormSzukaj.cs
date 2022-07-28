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
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using FakturniakDataAccess.Data;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakUI.Config;

// w tym pliku pewnie możnabyło uniknąć powtarzalności kodu w lepszy sposób,
// ale staż już się kończy, więc nie ma zbytnio czasu
namespace FakturniakUI
{
    public partial class FormSzukaj : Form
    {
        public object returnModel;

        private string tabela;

        readonly ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);

        private IEnumerable<ModelKontrahent> kontrahenci_enumerable;
        private IEnumerable<ModelPrzychod> faktury_enumerable;

        private List<ModelKontrahent> kontrahenci;
        private List<ModelPrzychod> faktury;

        public FormSzukaj(string _tabela)
        {
            InitializeComponent();
            tabela = _tabela;
        }

        private object[] GetDataKontrahent(ModelKontrahent _kontrahent)
        {
            object[] kontrahentToAdd = new object[]
            {
                _kontrahent.id_kontrahenta,
                _kontrahent.nazwa,
                _kontrahent.imie,
                _kontrahent.nazwisko,
                _kontrahent.adres,
                _kontrahent.kod_pocztowy,
                _kontrahent.miasto,
                _kontrahent.email,
                _kontrahent.telefon,
                _kontrahent.pesel,
                _kontrahent.nip,
                _kontrahent.krs,
                _kontrahent.regon
            };

            return kontrahentToAdd;
        }
        private object[] GetDataFaktura(ModelPrzychod _faktura)
        {
            object[] fakturaToAdd = new object[]
            {
                _faktura.numer_faktury,
                _faktura.Suma + " zł",
            };

            return fakturaToAdd;
        }

        private async void FormSzukaj_Load(object sender, EventArgs e)
        {
            this.CenterToParent();

            if (tabela == "Kontrahenci")
            {
                dataGridViewSzukaj.Rows.Clear();
                dataGridViewSzukaj.Columns.Clear();

                dataGridViewSzukaj.Columns.Add("ID", "ID");
                dataGridViewSzukaj.Columns.Add("Nazwa", "Nazwa");
                dataGridViewSzukaj.Columns.Add("Imie", "Imie");
                dataGridViewSzukaj.Columns.Add("Nazwisko", "Nazwisko");
                dataGridViewSzukaj.Columns.Add("Adres", "Adres");
                dataGridViewSzukaj.Columns.Add("KodPocztowy", "Kod pocztowy");
                dataGridViewSzukaj.Columns.Add("Miasto", "Miasto");
                dataGridViewSzukaj.Columns.Add("email", "e-mail");
                dataGridViewSzukaj.Columns.Add("telefon", "Telefon");
                dataGridViewSzukaj.Columns.Add("NIP", "NIP");
                dataGridViewSzukaj.Columns.Add("KRS", "KRS");
                dataGridViewSzukaj.Columns.Add("REGON", "REGON");

                IDataKontrahenci dataKontrahenci = new DataKontrahenci(dataAccess);

                await Task.Run(() => kontrahenci_enumerable = dataKontrahenci.Get().Result);
                kontrahenci = kontrahenci_enumerable.ToList();

                foreach (ModelKontrahent kontrahent in kontrahenci)
                {
                    object[] kontrahentToAdd = GetDataKontrahent(kontrahent);
                    dataGridViewSzukaj.Rows.Add(kontrahentToAdd);
                }
            }

            if (tabela == "Faktury")
            {
                dataGridViewSzukaj.Rows.Clear();
                dataGridViewSzukaj.Columns.Clear();

                dataGridViewSzukaj.Columns.Add("numer_faktury", "Numer faktury");
                dataGridViewSzukaj.Columns.Add("kwota", "Suma z faktury");

                IDataPrzychody dataFaktury = new DataPrzychody(dataAccess);

                await Task.Run(() => faktury_enumerable = dataFaktury.Get().Result);
                faktury = faktury_enumerable.ToList();

                foreach (ModelPrzychod faktura in faktury)
                {
                    object[] fakturaToAdd = GetDataFaktura(faktura);
                    dataGridViewSzukaj.Rows.Add(fakturaToAdd);
                }
            }
        }

        private async void textBoxSzukaj_TextChanged(object sender, EventArgs e)
        {
            dataGridViewSzukaj.Rows.Clear();
            dataGridViewSzukaj.Refresh();

            if (tabela == "Kontrahenci")
            {
                IDataKontrahenci dataKontrahenci = new DataKontrahenci(dataAccess);

                await Task.Run(() => kontrahenci_enumerable = dataKontrahenci.Search(textBoxSzukaj.Text).Result);
                kontrahenci = kontrahenci_enumerable.ToList();

                foreach (ModelKontrahent kontrahent in kontrahenci)
                {
                    object[] kontrahentToAdd = GetDataKontrahent(kontrahent);
                    dataGridViewSzukaj.Rows.Add(kontrahentToAdd);
                }
            }

            if (tabela == "Faktury")
            {
                IDataPrzychody dataFaktury = new DataPrzychody(dataAccess);

                await Task.Run(() => faktury_enumerable = dataFaktury.Search(textBoxSzukaj.Text).Result);
                faktury = faktury_enumerable.ToList();

                foreach (ModelPrzychod faktura in faktury)
                {
                    object[] fakturaToAdd = GetDataFaktura(faktura);
                    dataGridViewSzukaj.Rows.Add(fakturaToAdd);
                }
            }
        }
        private void buttonWybierz_Click(object sender, EventArgs e)
        {
            if (tabela == "Kontrahenci")
            {
                if (dataGridViewSzukaj.SelectedRows.Count > 1)
                    MessageBox.Show(this, "Nie możesz wybrać więcej niż jednego odbiorcy.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    foreach (DataGridViewRow row in dataGridViewSzukaj.Rows)
                    {
                        if (row.Selected)
                        {
                            foreach (ModelKontrahent kontrahent in kontrahenci)
                            {
                                int id_kontrahenta = Int32.Parse(row.Cells["ID"].Value.ToString());
                                if (id_kontrahenta == kontrahent.id_kontrahenta) returnModel = kontrahent;
                            }

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }

            if (tabela == "Faktury")
            {
                if (dataGridViewSzukaj.SelectedRows.Count > 1)
                    MessageBox.Show(this, "Nie możesz wybrać więcej niż jednego odbiorcy.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    foreach (DataGridViewRow row in dataGridViewSzukaj.Rows)
                    {
                        if (row.Selected)
                        {
                            foreach (ModelPrzychod faktura in faktury)
                            {
                                string numer_faktury = row.Cells["numer_faktury"].Value.ToString();
                                if (numer_faktury == faktura.numer_faktury) returnModel = faktura;
                            }

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
        }

        private void dataGridViewKontrahenci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridViewSzukaj.CurrentCell.RowIndex;

            dataGridViewSzukaj.Rows[index].Selected = true;
        }

    }
}
