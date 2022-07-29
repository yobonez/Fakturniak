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
using System.Globalization;
using System.Linq;
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
        IEnumerable<ModelPrzychod> _przychody;
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
            foreach (ModelPrzychod przychod in _przychody)
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
            toolStripStatusBarGodzina.Text = DateTime.Now.ToLongTimeString();
            if (FakturniakStatus.refreshMenu)
            {
                ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);
                RefreshMenu((SqlDataAccess)dataAccess);

                FakturniakStatus.refreshMenu = false;
            }
        }

        private async void buttonPokazFaktury_Click(object sender, EventArgs e)
        {
            ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);

            ModelPrzychod przychod = new ModelPrzychod();
            using (FormSzukaj formSzukaj = new FormSzukaj("Faktury"))
            {
                var result = formSzukaj.ShowDialog();

                if (result == DialogResult.OK)
                {
                    przychod = (ModelPrzychod)formSzukaj.returnModel;
                    formSzukaj.Dispose();

                    #region dataAccess
                    IDataFaktury dataFaktury = new DataFaktury(dataAccess);
                    IDataKontrahenci dataKontrahenci = new DataKontrahenci(dataAccess);
                    IDataJednostkiMiary dataJednostkiMiary = new DataJednostkiMiary(dataAccess);
                    IDataStawkiVAT dataStawkiVAT = new DataStawkiVAT(dataAccess);
                    IDataProdukty dataProdukty = new DataProdukty(dataAccess);
                    IDataMTMFakturaProdukt dataMTMFakturaProdukt = new DataMTMFakturaProdukt(dataAccess);
                    IDataTypyFaktury dataTypyFaktury = new DataTypyFaktury(dataAccess);
                    IDataSposobyPlatnosci dataSposobyPlatnosci = new DataSposobyPlatnosci(dataAccess);
                    #endregion

                    #region models
                    ModelFaktura _faktura = new ModelFaktura();
                    ModelKontrahent _nabywca = new ModelKontrahent();
                    ModelKontrahent _sprzedawca = new ModelKontrahent();
                    List<ModelJednostkaMiary> _jednostkiMiary = new List<ModelJednostkaMiary>();
                    List<ModelStawkaVAT> _stawkiVAT = new List<ModelStawkaVAT>();
                    ModelTypFaktury _typFaktury = new ModelTypFaktury();
                    ModelSposobPlatnosci _sposobPlatnosci = new ModelSposobPlatnosci();
                    List<ModelProdukt> _produkty = new List<ModelProdukt>();
                    List<ModelMTMFakturaProdukt> _produktyFaktury = new List<ModelMTMFakturaProdukt>();
                    List<object[]> _produkty_szczegoly = new List<object[]>();
                    #endregion models

                    #region query
                    await Task.Run(() => _faktura = dataFaktury.Load(przychod.numer_faktury).Result);
                    await Task.Run(() => _sprzedawca = dataKontrahenci.Load(_faktura.id_sprzedawca).Result);
                    await Task.Run(() => _nabywca = dataKontrahenci.Load(_faktura.id_nabywca).Result);
                    await Task.Run(() => _jednostkiMiary = dataJednostkiMiary.Get().Result.ToList());
                    await Task.Run(() => _stawkiVAT = dataStawkiVAT.Get().Result.ToList());
                    await Task.Run(() => _typFaktury = dataTypyFaktury.Load(_faktura.id_typu_faktury).Result);
                    await Task.Run(() => _sposobPlatnosci = dataSposobyPlatnosci.Load(_faktura.id_sposob_platnosci).Result);
                    await Task.Run(() => _produkty = dataProdukty.Get().Result.ToList());
                    await Task.Run(() => _produktyFaktury = dataMTMFakturaProdukt.LoadByNumerFaktury(_faktura.numer_faktury).Result.ToList());
                    #endregion

                    _faktura.data_wystawienia = DateTime.ParseExact(_faktura.data_wystawienia + " AM", "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("MM.dd.yyyy");
                    _faktura.data_sprzedazy = DateTime.ParseExact(_faktura.data_sprzedazy + " AM", "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("MM.dd.yyyy");
                    _faktura.termin_platnosci = DateTime.ParseExact(_faktura.termin_platnosci + " AM", "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("MM.dd.yyyy");

                    foreach (ModelMTMFakturaProdukt produkt in  _produktyFaktury)
                    {
                        string nazwa_produktu = "";
                        int ilosc = produkt.ilosc;
                        string jednostka_miary = "";
                        Decimal cena_netto = 0.0M;
                        Decimal cena_brutto = 0.0M;
                        int stawka_vat = 0;

                        foreach (ModelProdukt modelProdukt in _produkty)
                        {
                            if (modelProdukt.id_produktu == produkt.id_produktu)
                            {
                                nazwa_produktu = modelProdukt.nazwa;
                                cena_netto = modelProdukt.cena_netto;
                                cena_brutto = modelProdukt.cena_brutto;

                                foreach (ModelJednostkaMiary modelJednostkaMiary in _jednostkiMiary)
                                {
                                    if (modelProdukt.id_jednostki == modelJednostkaMiary.id_jednostki)
                                        jednostka_miary = modelJednostkaMiary.nazwa;
                                }
                                foreach (ModelStawkaVAT modelStawkaVAT in _stawkiVAT)
                                {
                                    if (modelProdukt.id_stawki == modelStawkaVAT.id_stawki)
                                        stawka_vat = modelStawkaVAT.wartosc;
                                }
                            }

                        }

                        Decimal wartosc_brutto = cena_brutto * ilosc;
                        Decimal wartosc_vat = wartosc_brutto - cena_netto;

                        _produkty_szczegoly.Add
                        (
                            new object[]
                            {
                                0,
                                nazwa_produktu,
                                jednostka_miary,
                                ilosc,
                                cena_netto,
                                cena_brutto,
                                stawka_vat,
                                wartosc_vat,
                                wartosc_brutto,
                                produkt.id_produktu
                            }
                        );
                    }

                    FakturaViewer fakturaViewer = new FakturaViewer(_faktura, 
                        _produktyFaktury, 
                        _sprzedawca, _nabywca, 
                        _produkty_szczegoly, 
                        _typFaktury.opis, 
                        _sposobPlatnosci.nazwa, 
                        przychod.Suma, 
                        0);

                    fakturaViewer.Show();
                }
            }
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormO formO = new FormO();
            formO.Show();
        }
    }
}
