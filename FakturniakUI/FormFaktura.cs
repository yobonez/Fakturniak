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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.Data;
using FakturniakUI.Config;
using FakturniakDataAccess.Status;

namespace FakturniakUI
{
    public partial class FormFaktura : Form
    {
        readonly ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);

        /// <summary>
        /// Sekcja gotowych danych, które mają być zatwierdzone po kliknięciu "Wystaw" (niektóre będą gotowe dopiero po kliknięciu)
        /// </summary>
        
        string __numer_faktury = "";

        ModelFaktura faktura = new ModelFaktura();
        List<ModelMTMFakturaProdukt> produktyFaktury = new List<ModelMTMFakturaProdukt>();

        ModelKontrahent sprzedawca = new ModelKontrahent();
        ModelKontrahent nabywca = new ModelKontrahent();

        ModelSposobPlatnosci sposob_platnosci = new ModelSposobPlatnosci();

        Decimal do_zaplaty = 0.00M;

        /// <summary>
        /// Koniec sekcji danych do zatwierdzenia
        /// </summary>


        List<ModelProdukt> produkty;
        List<ModelSposobPlatnosci> sposoby_platnosci = new List<ModelSposobPlatnosci>();

        List<object[]> kompletne_obiekty_produkty = new List<object[]>();

        public FormFaktura()
        {
            InitializeComponent();
        }

        private async void FormFaktura_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.pictureBox1.ImageLocation = FakturniakConfig.xmlFakturniakConfig.logo_path;

            IDataFaktury dataFaktury = new DataFaktury(dataAccess);
            await Task.Run(() => __numer_faktury = dataFaktury.GetNumerFaktury(null, 0).Result);

            IDataProdukty dataProdukty = new DataProdukty(dataAccess);
            await Task.Run(() => produkty = dataProdukty.Get().Result.ToList());

            IDataSposobyPlatnosci dataSposobyPlatnosci = new DataSposobyPlatnosci(dataAccess);
            await Task.Run(() => sposoby_platnosci = dataSposobyPlatnosci.Get().Result.ToList());

            foreach (ModelSposobPlatnosci spPlatnosci in sposoby_platnosci)
            {
                comboBoxMetodyPlatnosci.Items.Add(spPlatnosci.nazwa);
            }

            loadSprzedawca();
            populateProdukty();

            textBox1.Text = __numer_faktury.Substring(3, 3);
            string toDateTime = __numer_faktury.Substring(7, 7);
            dateTimePicker1.Value = DateTime.Parse(toDateTime);

            textBoxWystawienie.Text = FakturniakConfig.xmlFakturniakConfig.ostanie_miasto_wystawiania;
            comboBoxMetodyPlatnosci.Text = FakturniakConfig.xmlFakturniakConfig.ostatni_sposob_platnosci;
        }


        private async void loadSprzedawca()
        {
            int id_sprzedawcy_do_zaladowania = FakturniakConfig.xmlFakturniakConfig.id_zarejestrowany;

            IDataKontrahenci kontrahenci = new DataKontrahenci(dataAccess);
            ModelKontrahent _sprzedawca = new ModelKontrahent();

            await Task.Run(() => _sprzedawca = kontrahenci.Load(id_sprzedawcy_do_zaladowania).Result);

            textBoxSIMIE.Text = _sprzedawca.imie;
            textBoxSNAZWISKO.Text = _sprzedawca.nazwisko;
            textBoxSNAZWA.Text = _sprzedawca.nazwa;

            textBoxSNumery1.Text = _sprzedawca.pesel;
            textBoxSNumery2.Text = _sprzedawca.nip;
            textBoxSNumery3.Text = _sprzedawca.krs;
            textBoxSNumery4.Text = _sprzedawca.regon;

            textBoxSAdres.Text = _sprzedawca.adres;
            textBoxSMiasto.Text = _sprzedawca.miasto;
            maskedTextBoxSKodPocztowy.Text = _sprzedawca.kod_pocztowy;

            maskedTextBox3.Text = _sprzedawca.numer_konta;

            sprzedawca = _sprzedawca;
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

                        object[] rowArrToAdd = {0,
                                                child_control.nazwa,
                                                tempJednostkaMiary.nazwa,
                                                _ilosc,
                                                child_control.cena_netto,
                                                child_control.cena_brutto,
                                                tempStawkaVAT.wartosc,
                                                wartosc_vat,
                                                wartosc_brutto,
                                                child_control.id_produktu};

                        kompletne_obiekty_produkty.Add(rowArrToAdd);
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

                int rowLP = Int32.Parse(Row.Cells[0].Value.ToString());
                anySelected = true;

                dataGridViewMTMProdukty.Rows.Remove(Row);

                if (kompletne_obiekty_produkty[rowLP - 1][9] == Row.Cells[9].Value)
                {
                    kompletne_obiekty_produkty.Remove(kompletne_obiekty_produkty[rowLP - 1]);
                }
            }


            if(!anySelected)
            {
                MessageBox.Show(this, "Najpierw wybierz produkty zaznaczając je poprzez kliknięcie w puste pole bez kolumny po lewej stronie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void UpdateProdukty()
        {
            int lp_produkt = 0;

            foreach (DataGridViewRow Row in dataGridViewMTMProdukty.Rows)
            {
                if (Row.Cells[0].Value == null)
                    continue;

                Row.Cells["lp"].Value = ++lp_produkt;

                int ilosc = Int32.Parse(Row.Cells["Ilosc"].Value.ToString());
                Decimal kwota_jeden_brutto = Decimal.Parse(Row.Cells["Brutto"].Value.ToString());
                Decimal kwota_jeden_netto = Decimal.Parse(Row.Cells["Netto"].Value.ToString());

                Decimal kwota_xilosc_brutto = kwota_jeden_brutto * ilosc;
                Decimal kwota_xilosc_netto = kwota_jeden_netto * ilosc;



                Row.Cells["KVAT"].Value = kwota_xilosc_brutto - kwota_xilosc_netto;
                Row.Cells["WBrutto"].Value = kwota_xilosc_brutto;

                kompletne_obiekty_produkty[lp_produkt - 1][3] = ilosc.ToString(); // NIECZYTELNOŚĆ 100
                kompletne_obiekty_produkty[lp_produkt - 1][7] = (kwota_xilosc_brutto - kwota_xilosc_netto).ToString();
                kompletne_obiekty_produkty[lp_produkt - 1][8] = kwota_xilosc_brutto.ToString();
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
            do_zaplaty = kwota_do_zaplaty;
        }

        void ZaladujDaneZFaktury()
        {
            produktyFaktury = new List<ModelMTMFakturaProdukt>(); // Bug z duplikowaniem produktów z każdym razem, gdy się kliknie podgląd 
                                                                  // został własnie naprawiony :)

            DateTime _data_wystawienia = dateTimePickerWystawienie.Value;
            DateTime _data_sprzedazy = dateTimePickerSprzedaz.Value;
            DateTime _termin_platnosci = dateTimePickerTermin.Value;

            // do inserta do tabeli FAKTUR

            faktura.id_sprzedawca = FakturniakConfig.xmlFakturniakConfig.id_zarejestrowany;
            faktura.id_nabywca = nabywca.id_kontrahenta;

            faktura.numer_faktury = __numer_faktury;
            faktura.data_wystawienia = _data_wystawienia.ToString("MM.dd.yyyy");
            faktura.data_sprzedazy = _data_sprzedazy.ToString("MM.dd.yyyy");
            faktura.miejsce_wystawienia = textBoxWystawienie.Text;
            faktura.id_sposob_platnosci = sposob_platnosci.id_sposob_platnosci;
            faktura.termin_platnosci = _termin_platnosci.ToString("MM.dd.yyyy");

            faktura.uwagi = richTextBoxUwagi.Text;
            faktura.uwagi_wewnetrzne = richTextBoxUwagiWewnetrzne.Text;

            if (faktura.uwagi == "")
                faktura.uwagi = "Brak";
            if (faktura.uwagi_wewnetrzne == "")
                faktura.uwagi_wewnetrzne = "Brak";

            // do inserta do tabeli PRODUKTÓW FAKURY
            foreach (DataGridViewRow Row in this.dataGridViewMTMProdukty.Rows)
            {
                if (Row.Cells[0].Value == null)
                    continue;

                ModelMTMFakturaProdukt temp_produkt = new ModelMTMFakturaProdukt
                {
                    numer_faktury = __numer_faktury,
                    id_produktu = Int32.Parse(Row.Cells["ID"].Value.ToString()),
                    ilosc = Int32.Parse(Row.Cells["Ilosc"].Value.ToString())
                };

                produktyFaktury.Add(temp_produkt);
            }
        }

        void PodgladDruk(int podglad)
        {
            FakturaViewer fakturaViewer = new FakturaViewer(faktura, 
                produktyFaktury, 
                sprzedawca, nabywca, 
                kompletne_obiekty_produkty,
                LabelTypFaktury.Text,
                sposob_platnosci.nazwa,
                do_zaplaty,
                podglad);
            fakturaViewer.ShowDialog();
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
                    nabywca = (ModelKontrahent)formSzukaj.returnModel;
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
            maskedTextBoxNKodPocztowy.Text = nabywca.kod_pocztowy;

            panelNabywca1.Enabled = false;
            panelNabywca2.Enabled = false;
            panelNabywca3.Enabled = false;
        }

        private void labelKwota_SizeChanged(object sender, EventArgs e)
        {
            labelPLN.Left = labelKwota.Right + 1;
        }

        private void dataGridViewMTMProdukty_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => UpdateProdukty();

        private void dataGridViewMTMProdukty_CellValueChanged(object sender, DataGridViewCellEventArgs e) => UpdateProdukty();

        private void dataGridViewMTMProdukty_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => UpdateProdukty();
        private void comboBoxMetodyPlatnosci_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ModelSposobPlatnosci spPlatnosci in sposoby_platnosci)
            {
                if (comboBoxMetodyPlatnosci.SelectedItem.ToString()/*CS0252*/ == spPlatnosci.nazwa)
                {
                    sposob_platnosci = new ModelSposobPlatnosci
                    {
                        id_sposob_platnosci = spPlatnosci.id_sposob_platnosci,
                        nazwa = spPlatnosci.nazwa
                    };
                }
            }
        }
        private void Wystaw_Click(object sender, EventArgs e)
        {
            // tak wiem, ten config by mozna bylo inaczej dac zeby po prostu jedna rzecz dawal, a nie ze wszystko z powodu
            // jednej lub dwóch rzeczy, moze potem to zrobie
            FakturniakConfigModel fakturniakConfigModel = new FakturniakConfigModel()
            {
                ostanie_miasto_wystawiania = textBoxWystawienie.Text,
                ostatni_sposob_platnosci = comboBoxMetodyPlatnosci.Text,

                id_zarejestrowany = FakturniakConfig.xmlFakturniakConfig.id_zarejestrowany,
                logo_path = FakturniakConfig.xmlFakturniakConfig.logo_path,
                ostatni_zalogowany_uzytkownik = FakturniakConfig.xmlFakturniakConfig.ostatni_zalogowany_uzytkownik
            };
            FakturniakConfig.Write("FakturniakConfig.xml", fakturniakConfigModel);

            DialogResult dialogResult = MessageBox.Show(this, "Jesteś pewien, że chcesz wystawić tą fakturę? \nPrzed wystawieniem upewnij się, czy wszystkie wpisane dane są poprawne.", "Ostrzeżenie", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult != DialogResult.OK)
                this.Focus();
            else
            {
                IDataFaktury dataFaktury = new DataFaktury(dataAccess);
                IDataMTMFakturaProdukt dataMTMFakturaProdukt = new DataMTMFakturaProdukt(dataAccess);

                ZaladujDaneZFaktury();

                // INSERT
                dataFaktury.Insert(faktura);
                foreach (ModelMTMFakturaProdukt faktura_produkt in produktyFaktury)
                {
                    dataMTMFakturaProdukt.Insert(faktura_produkt);
                }
                // KONIEC INSERTA

                FakturniakStatus.refreshMenu = true;
                MessageBox.Show(this, "Pomyślnie wystawiono fakturę.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult drukResult = MessageBox.Show(this, "Czy chcesz zobaczyć podgląd/wydrukować tę fakturę?", "Opcja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drukResult != DialogResult.Yes)
                    this.Focus();
                else
                    PodgladDruk(0);
            }

        }

        private void Podglad_Click(object sender, EventArgs e)
        {
            FakturniakConfigModel fakturniakConfigModel = new FakturniakConfigModel()
            {
                ostanie_miasto_wystawiania = textBoxWystawienie.Text,
                ostatni_sposob_platnosci = comboBoxMetodyPlatnosci.Text,

                id_zarejestrowany = FakturniakConfig.xmlFakturniakConfig.id_zarejestrowany,
                logo_path = FakturniakConfig.xmlFakturniakConfig.logo_path,
                ostatni_zalogowany_uzytkownik = FakturniakConfig.xmlFakturniakConfig.ostatni_zalogowany_uzytkownik
            };
            FakturniakConfig.Write("FakturniakConfig.xml", fakturniakConfigModel);

            ZaladujDaneZFaktury();
            PodgladDruk(1);
        }
    }
}
