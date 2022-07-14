using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using FakturniakDataAccess.Data;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;

namespace FakturniakUI
{
    public partial class FormSzukaj : Form
    {
        public ModelKontrahent returnModelKontrahent = new ModelKontrahent();

        private string tabela;

        readonly ISqlDataAccess dataAccess = new SqlDataAccess();

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
