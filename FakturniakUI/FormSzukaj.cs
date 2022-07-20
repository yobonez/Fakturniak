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

namespace FakturniakUI
{
    public partial class FormSzukaj : Form
    {
        public ModelKontrahent returnModelKontrahent = new ModelKontrahent();

        private string tabela;

        readonly ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);

        IEnumerable<ModelKontrahent> kontrahenci_enumerable;
        List<ModelKontrahent> kontrahenci;

        public FormSzukaj(string _tabela)
        {
            InitializeComponent();
            tabela = _tabela;
        }
        private async void FormSzukaj_Load(object sender, EventArgs e)
        {
            this.CenterToParent();

            if (tabela == "Kontrahenci")
            {
                IDataKontrahenci dataKontrahenci = new DataKontrahenci(dataAccess);

                await Task.Run(() => kontrahenci_enumerable = dataKontrahenci.Get().Result);
                kontrahenci = kontrahenci_enumerable.ToList();


                foreach (ModelKontrahent kontrahent in kontrahenci)
                {
                    object[] kontrahentToAdd = new object[]
                    {
                        kontrahent.id_kontrahenta,
                        kontrahent.nazwa,
                        kontrahent.imie,
                        kontrahent.nazwisko,
                        kontrahent.adres,
                        kontrahent.kod_pocztowy,
                        kontrahent.miasto,
                        kontrahent.email,
                        kontrahent.telefon,
                        kontrahent.pesel,
                        kontrahent.nip,
                        kontrahent.krs,
                        kontrahent.regon
                    };

                    dataGridViewKontrahenci.Rows.Add(kontrahentToAdd);
                }
            }
        }

        private void textBoxSzukaj_TextChanged(object sender, EventArgs e)
        {
            // 14.07
            // todo
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void dataGridViewKontrahenci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridViewKontrahenci.CurrentCell.RowIndex;

            dataGridViewKontrahenci.Rows[index].Selected = true;
        }

        private void buttonWybierz_Click(object sender, EventArgs e)
        {
            if (dataGridViewKontrahenci.SelectedRows.Count > 1)
                MessageBox.Show(this, "Nie możesz wybrać więcej niż jednego     odbiorcy.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                foreach (DataGridViewRow rowKontrahent in dataGridViewKontrahenci.Rows)
                {
                    if (rowKontrahent.Selected)
                    {
                        returnModelKontrahent = new ModelKontrahent
                        {
                            id_kontrahenta = Convert.ToInt32(rowKontrahent.Cells["ID"].Value),
                            imie = Convert.ToString(rowKontrahent.Cells["Imie"].Value),
                            nazwisko = Convert.ToString(rowKontrahent.Cells["Nazwisko"].Value),
                            nazwa = Convert.ToString(rowKontrahent.Cells["Nazwa"].Value),
                            pesel = Convert.ToString(rowKontrahent.Cells["PESEL"].Value),
                            nip = Convert.ToString(rowKontrahent.Cells["NIP"].Value),
                            krs = Convert.ToString(rowKontrahent.Cells["KRS"].Value),
                            regon = Convert.ToString(rowKontrahent.Cells["REGON"].Value),
                            adres = Convert.ToString(rowKontrahent.Cells["Adres"].Value),
                            miasto = Convert.ToString(rowKontrahent.Cells["Miasto"].Value),
                            kod_pocztowy = Convert.ToString(rowKontrahent.Cells["KodPocztowy"].Value)
                        };

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
    }
}
