using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FakturniakUI.Config;
using FakturniakDataAccess.Models;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;

namespace FakturniakUI
{
    public partial class FakturaViewer : Form
    {
        private ModelFaktura faktura = new ModelFaktura();
        private List<ModelMTMFakturaProdukt> produktyFaktury = new List<ModelMTMFakturaProdukt>();
        private ModelKontrahent sprzedawca;
        private ModelKontrahent nabywca;
        private List<object[]> kompletne_szczegoly_produkty = new List<object[]>();
        private string typ_faktury;
        private string sposob_platnosci;
        private Decimal do_zaplaty;


        public FakturaViewer(ModelFaktura _faktura, 
            List<ModelMTMFakturaProdukt> _produktyFaktury, 
            ModelKontrahent _sprzedawca, 
            ModelKontrahent _nabywca,
            List<object[]> _kompletne_szczegoly_produkty,
            string _typ_faktury,
            string _sposob_platnosci,
            Decimal _do_zaplaty)
        {
            InitializeComponent();


            faktura = _faktura;
            produktyFaktury = _produktyFaktury;
            sprzedawca = _sprzedawca;
            nabywca = _nabywca;
            kompletne_szczegoly_produkty = _kompletne_szczegoly_produkty;
            sposob_platnosci = _sposob_platnosci;
            do_zaplaty = _do_zaplaty;
            typ_faktury = _typ_faktury;


            faktura.numer_faktury = typ_faktury + faktura.numer_faktury.Remove(0, 2);
            // podmiana MM.dd.yyyy (bo wcześniej był potrzebny do inserta) na dd.MM.yyyy
            faktura.data_wystawienia = DateTime.ParseExact(faktura.data_wystawienia, "MM.dd.yyyy", CultureInfo.InvariantCulture).ToShortDateString();
            faktura.data_sprzedazy = DateTime.ParseExact(faktura.data_sprzedazy, "MM.dd.yyyy", CultureInfo.InvariantCulture).ToShortDateString();
            faktura.termin_platnosci = DateTime.ParseExact(faktura.termin_platnosci, "MM.dd.yyyy", CultureInfo.InvariantCulture).ToShortDateString();
        }

        private void FakturaViewer_Load(object sender, EventArgs e)
        {
            string image = "";

            // config
            reportViewer1.LocalReport.ReportEmbeddedResource = "FakturniakUI.Report.ReportDefinitions.FakturaDokument.rdlc";
            ReportParameterCollection paramCollection = new ReportParameterCollection();
            paramCollection.Add(new ReportParameter("ReportName", faktura.numer_faktury));
            paramCollection.Add(new ReportParameter("MWystawienia", faktura.miejsce_wystawienia));
            paramCollection.Add(new ReportParameter("DWystawienia", faktura.data_wystawienia));
            paramCollection.Add(new ReportParameter("DSprzedazy", faktura.data_sprzedazy));
            paramCollection.Add(new ReportParameter("ImagePath", FakturniakConfig.xmlFakturniakConfig.logo_path));

            paramCollection.Add(new ReportParameter("SNazwa", sprzedawca.nazwa));
            paramCollection.Add(new ReportParameter("SNIP", sprzedawca.nip));
            paramCollection.Add(new ReportParameter("SAdres", sprzedawca.adres));
            paramCollection.Add(new ReportParameter("SKodPocztowy", sprzedawca.kod_pocztowy));
            paramCollection.Add(new ReportParameter("SMiasto", sprzedawca.miasto));

            paramCollection.Add(new ReportParameter("NNazwa", nabywca.nazwa));
            paramCollection.Add(new ReportParameter("NNIP", nabywca.nip));
            paramCollection.Add(new ReportParameter("NAdres", nabywca.adres));
            paramCollection.Add(new ReportParameter("NKodPocztowy", nabywca.kod_pocztowy));
            paramCollection.Add(new ReportParameter("NMiasto", nabywca.miasto));

            paramCollection.Add(new ReportParameter("SposobPlatnosci", sposob_platnosci));
            paramCollection.Add(new ReportParameter("NumerKonta", sprzedawca.numer_konta));
            paramCollection.Add(new ReportParameter("Termin", faktura.termin_platnosci));
            paramCollection.Add(new ReportParameter("DoZaplaty", do_zaplaty.ToString()));
            paramCollection.Add(new ReportParameter("Uwagi", faktura.uwagi));
            paramCollection.Add(new ReportParameter("SprzedawcaImieNazwisko", sprzedawca.imie + " " + sprzedawca.nazwisko));


            using (var bitmap = new Bitmap(FakturniakConfig.xmlFakturniakConfig.logo_path))
            {
                using var ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Bmp);
                image = Convert.ToBase64String(ms.ToArray());
            }
            paramCollection.Add(new ReportParameter("image", image));

            FakturniakDBDataSet ds = new FakturniakDBDataSet();
            DataTable dt = ds.Tables["ProduktyFaktury"];

            int lp = 0;
            foreach (object[] produkt in kompletne_szczegoly_produkty)
            {
                ++lp;
                var row = dt.NewRow();

                row["lp"] = lp;
                row["nazwa"] = produkt[1];
                row["jednostka"] = produkt[2];
                row["ilosc"] = Int32.Parse(produkt[3].ToString());
                row["netto"] = produkt[4];
                row["brutto"] = produkt[5];
                row["STVAT"] = produkt[6];
                row["KWVAT"] = produkt[7];
                row["WBrutto"] = produkt[8];

                dt.Rows.Add(row);
            }

            ReportDataSource reportDataSource = new ReportDataSource("DataSetFaktura", dt);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.SetParameters(paramCollection);

            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
        }
    }
}
