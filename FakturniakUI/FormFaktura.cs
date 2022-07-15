using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.Data;


namespace FakturniakUI
{
    public partial class FormFaktura : Form
    {
        readonly ISqlDataAccess dataAccess = new SqlDataAccess();

        /// <summary>
        /// Sekcja gotowych danych, które mają być zatwierdzone po kliknięciu "Wystaw"
        /// </summary>

        DateTime data_wystawienia = new DateTime();
        DateTime data_wykonania = new DateTime();
        DateTime termin_platnosci = new DateTime();

        ModelKontrahent nabywca = new ModelKontrahent();

        ModelSposobPlatnosci sposob_platnosci = new ModelSposobPlatnosci();
        List<ModelMTMFakturaProdukt> produktyFaktura = new List<ModelMTMFakturaProdukt>();

        Decimal do_zaplaty = 0.00M;
        /// <summary>
        /// Koniec sekcji danych do zatwierdzenia
        /// </summary>


        List<ModelProdukt> produkty;
        List<ModelSposobPlatnosci> sposoby_platnosci = new List<ModelSposobPlatnosci>();

        public FormFaktura()
        {
            InitializeComponent();
        }

        private async void FormFaktura_Load(object sender, EventArgs e)
        { 
            this.CenterToParent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            IDataProdukty dataProdukty = new DataProdukty(dataAccess);
            await Task.Run(() => produkty = dataProdukty.Get().Result.ToList());

            IDataSposobyPlatnosci dataSposobyPlatnosci = new DataSposobyPlatnosci(dataAccess);
            await Task.Run(() => sposoby_platnosci = dataSposobyPlatnosci.Get().Result.ToList());

            foreach (ModelSposobPlatnosci spPlatnosci in sposoby_platnosci)
            {
                comboBoxMetodyPlatnosci.Items.Add(spPlatnosci.nazwa);
            }

            populateProdukty();
        }

        private void populateProdukty()
        {
            UCProdukt[] ucProdukty = new UCProdukt[produkty.Count];
            
            for(int i = 0; i < produkty.Count; i++)
            {
                UCProdukt tempUcProdukt = new UCProdukt();

                int id_produktu = produkty[i].id_produktu;
                int id_stawki = produkty[i].id_stawki;
                int id_jednostki = produkty[i].id_jednostki;
                string nazwa = produkty[i].nazwa;
                Decimal cena_netto = produkty[i].cena_netto;
                Decimal cena_brutto = produkty[i].cena_brutto;

                tempUcProdukt.id_produktu = id_produktu;
                tempUcProdukt.id_stawki = id_stawki;
                tempUcProdukt.id_jednostki = id_jednostki;
                tempUcProdukt.nazwa = nazwa;
                tempUcProdukt.cena_netto = cena_netto;
                tempUcProdukt.cena_brutto = cena_brutto;


                ucProdukty[i] = tempUcProdukt;

                if (this.flowLayoutPanel1.Controls.Count < 0)
                    this.flowLayoutPanel1.Controls.Clear();
                else
                    this.flowLayoutPanel1.Controls.Add(ucProdukty[i]);
            }
        }

        private async void performSearch(string textInput)
        {
            this.flowLayoutPanel1.Controls.Clear();

            List<ModelProdukt> tempSearchProdukty = new() { };
            IDataProdukty dataProdukty = new DataProdukty(dataAccess);

            await Task.Run(() => tempSearchProdukty = dataProdukty.Search(textInput).Result.ToList());

            UCProdukt[] ucProduktyTemp = new UCProdukt[tempSearchProdukty.Count];

            for (int i = 0; i < tempSearchProdukty.Count; i++)
            {
                UCProdukt ucTempProdukt = new UCProdukt();

                ucTempProdukt.id_produktu = tempSearchProdukty[i].id_produktu;
                ucTempProdukt.id_stawki = tempSearchProdukty[i].id_stawki;
                ucTempProdukt.id_jednostki = tempSearchProdukty[i].id_stawki;
                ucTempProdukt.nazwa = tempSearchProdukty[i].nazwa;
                ucTempProdukt.cena_netto = tempSearchProdukty[i].cena_netto;
                ucTempProdukt.cena_brutto = tempSearchProdukty[i].cena_brutto;

                ucProduktyTemp[i] = ucTempProdukt;

                if (this.flowLayoutPanel1.Controls.Count < 0)
                    this.flowLayoutPanel1.Controls.Clear();
                else
                    this.flowLayoutPanel1.Controls.Add(ucProduktyTemp[i]);

            }
        }

        private async void DodajDoFaktury()
        {
            foreach (UCProdukt child_control in this.flowLayoutPanel1.Controls)
            {
                if (child_control.last_focused == true && child_control is UCProdukt)
                {
                    ModelStawkaVAT tempStawkaVAT = new ModelStawkaVAT();
                    ModelJednostkaMiary tempJednostkaMiary = new ModelJednostkaMiary();

                    IDataStawkiVAT dataStawkiVAT = new DataStawkiVAT(dataAccess);
                    IDataJednostkiMiary dataJednostkiMiary = new DataJednostkiMiary(dataAccess);

                    int _ilosc = 1;

                    await Task.Run(() => tempStawkaVAT = dataStawkiVAT.Load(child_control.id_stawki).Result);
                    await Task.Run(() => tempJednostkaMiary = dataJednostkiMiary.Load(child_control.id_jednostki).Result);

                    bool addRowNeeded = true;

                    foreach (DataGridViewRow row in dataGridViewMTMProdukty.Rows)
                    {
                        if (row.Cells[1].Value?.ToString() == child_control.nazwa)
                        {
                            addRowNeeded = false;

                            _ilosc = Int32.Parse(row.Cells["Ilosc"].Value.ToString());
                            _ilosc++;
                            row.Cells["Ilosc"].Value = _ilosc;
                        }
                    }

                    if (addRowNeeded)
                    {
                        Decimal wartosc_brutto = 0.00M;
                        wartosc_brutto = child_control.cena_brutto * _ilosc;
                        Decimal wartosc_vat = 0.00M;
                        wartosc_vat = wartosc_brutto - child_control.cena_netto;

                        object[] rowArrToAdd = {child_control.id_produktu,
                                                child_control.nazwa,
                                                tempJednostkaMiary.nazwa,
                                                _ilosc,
                                                child_control.cena_netto,
                                                child_control.cena_brutto,
                                                tempStawkaVAT.wartosc,
                                                wartosc_vat,
                                                wartosc_brutto};

                        this.dataGridViewMTMProdukty.Rows.Add(rowArrToAdd);
                    }
                }
            }
        }

        void UsunZFaktury()
        {
            bool anySelected = false;
            foreach (DataGridViewRow Row in dataGridViewMTMProdukty.SelectedRows)
            {
                if (Row.Cells[0].Value == null)
                    continue;

                anySelected = true;
                dataGridViewMTMProdukty.Rows.Remove(Row);
            }

            if(!anySelected)
            {
                MessageBox.Show(this, "Najpierw wybierz produkty zaznaczając je poprzez kliknięcie w puste pole bez kolumny po lewej stronie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void UpdateProdukty()
        {
            foreach (DataGridViewRow Row in dataGridViewMTMProdukty.Rows)
            {
                if (Row.Cells[0].Value == null)
                    continue;

                int ilosc = Int32.Parse(Row.Cells["Ilosc"].Value.ToString());
                Decimal kwota_jeden_brutto = Decimal.Parse(Row.Cells["Brutto"].Value.ToString());
                Decimal kwota_jeden_netto = Decimal.Parse(Row.Cells["Netto"].Value.ToString());

                Decimal kwota_xilosc_brutto = kwota_jeden_brutto * ilosc;
                Decimal kwota_xilosc_netto = kwota_jeden_netto * ilosc;


                Row.Cells["KVAT"].Value = kwota_xilosc_brutto - kwota_xilosc_netto;
                Row.Cells["WBrutto"].Value = kwota_xilosc_brutto;
            }

            Decimal kwota_do_zaplaty = 0.00M;
            foreach (DataGridViewRow Row in dataGridViewMTMProdukty.Rows)
            {
                if (Row.Cells[0].Value == null)
                    continue;

                Decimal temp_jeden_wbrutto = Decimal.Parse(Row.Cells["WBrutto"].Value.ToString());
                kwota_do_zaplaty += temp_jeden_wbrutto;
            }
            labelKwota.Text = kwota_do_zaplaty.ToString();
        }


        private void Wystaw_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSzukaj_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.textBoxSzukaj.Text == "Szukaj...")
                this.textBoxSzukaj.Text = "";
        }

        private void textBoxSzukaj_MouseLeave(object sender, EventArgs e)
        {
            if (this.textBoxSzukaj.Text == "" && this.textBoxSzukaj.Focused == false)
                this.textBoxSzukaj.Text = "Szukaj...";
        }

        private void textBoxSzukaj_TextChanged(object sender, EventArgs e) => performSearch(textBoxSzukaj.Text);

        private void buttonDodajProduktUsluge_Click(object sender, EventArgs e) => DodajDoFaktury();
        private void buttonUsunProduktUsluge_Click(object sender, EventArgs e) => UsunZFaktury();

        private void buttonWybierz_Click(object sender, EventArgs e)
        {
            using (FormSzukaj formSzukaj = new FormSzukaj("Kontrahenci"))
            {
                var result = formSzukaj.ShowDialog();

                if(result == DialogResult.OK)
                {
                    nabywca = formSzukaj.returnModelKontrahent;
                    formSzukaj.Dispose();
                }
            }

            textBoxNIMIE.Text = nabywca.imie;
            textBoxNNAZWISKO.Text = nabywca.nazwisko;
            textBoxNNAZWA.Text = nabywca.nazwa;
            textBoxNNumery1.Text = nabywca.pesel;
            textBoxNNumery2.Text = nabywca.nip;
            textBoxNNumery3.Text = nabywca.krs;
            textBoxNNumery4.Text = nabywca.regon;
            textBoxNAdres.Text = nabywca.adres;
            textBoxNMiasto.Text = nabywca.miasto;
            maskedTextBoxKodPocztowy.Text = nabywca.kod_pocztowy;

            panelNabywca1.Enabled = false;
            panelNabywca2.Enabled = false;
            panelNabywca3.Enabled = false;
        }

        private void labelKwota_SizeChanged(object sender, EventArgs e)
        {
            labelPLN.Left = labelKwota.Right + 1;
        }

        //private void dataGridViewMTMProdukty_CellEndEdit(object sender, DataGridViewCellEventArgs e) => UpdateProdukty();

        private void dataGridViewMTMProdukty_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => UpdateProdukty();

        private void dataGridViewMTMProdukty_CellValueChanged(object sender, DataGridViewCellEventArgs e) => UpdateProdukty();

        private void dataGridViewMTMProdukty_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => UpdateProdukty();

        private void comboBoxMetodyPlatnosci_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ModelSposobPlatnosci spPlatnosci in sposoby_platnosci)
            {
                if (comboBoxMetodyPlatnosci.SelectedItem == spPlatnosci.nazwa)
                {
                    sposob_platnosci = new ModelSposobPlatnosci
                    {
                        id_sposob_platnosci = spPlatnosci.id_sposob_platnosci,
                        nazwa = spPlatnosci.nazwa
                    };
                }
            }
        }
    }
}
