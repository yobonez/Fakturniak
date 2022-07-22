using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FakturniakUI.Config;
using FakturniakDataAccess.Models;
using System.Data.SqlClient;
using FakturniakDataAccess.DbAccess;
using System.IO;
using System.Drawing.Imaging;

namespace FakturniakUI
{
    public partial class FakturaViewer : Form
    {
        private ModelFaktura faktura = new ModelFaktura();
        private List<ModelMTMFakturaProdukt> produktyFaktury = new List<ModelMTMFakturaProdukt>();
        private ModelKontrahent sprzedawca;
        private ModelKontrahent nabywca;

        public FakturaViewer(ModelFaktura _faktura, 
            List<ModelMTMFakturaProdukt> _produktyFaktury, 
            ModelKontrahent _sprzedawca, 
            ModelKontrahent _nabywca)
        {
            InitializeComponent();

            faktura = _faktura;
            produktyFaktury = _produktyFaktury;
            sprzedawca = _sprzedawca;
            nabywca = _nabywca;
        }

        private void FakturaViewer_Load(object sender, EventArgs e)
        {
            string image = "";

            // config
            reportViewer1.LocalReport.ReportEmbeddedResource = "FakturniakUI.Report.ReportDefinitions.FakturaDokument.rdlc";
            ReportParameterCollection paramCollection = new ReportParameterCollection();
            paramCollection.Add(new ReportParameter("ReportName", faktura.numer_faktury));
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

            using (var bitmap = new Bitmap(FakturniakConfig.xmlFakturniakConfig.logo_path))
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Bmp);
                    image = Convert.ToBase64String(ms.ToArray());
                }
            }
            paramCollection.Add(new ReportParameter("image", image));

            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.SetParameters(paramCollection);

            /*
            Helper h = new Helper();
            string cnnstr = h.getConnectionString("FakturniakDB");
            */
            // config

            //DataSet1 ds = new DataSet1();

            /*
            using SqlConnection dbConnection = new SqlConnection(cnnstr);
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("exec dbo.spFaktury_GetAll", dbConnection);

                sqlDataAdapter.Fill(ds, ds.Tables[0].TableName);
            }
            */



            /*DataRow row = dt1.NewRow();
            row["numer_faktury"] = faktura.numer_faktury;
            row["data_wystawienia"] = faktura.numer_faktury;
            row["data_sprzedazy"] = faktura.numer_faktury;
            row["miejsce_wystawienia"] = faktura.numer_faktury;
            row["id_sprzedawca"] = faktura.numer_faktury;
            row["id_nabywca"] = faktura.numer_faktury;
            row["id_sposob_platnosci"] = faktura.numer_faktury;
            row["termin_platnosci"] = faktura.numer_faktury;
            row["id_typu_faktury"] = faktura.numer_faktury;
            row["uwagi"] = faktura.numer_faktury;
            row["uwagi_wewnetrzne"] = faktura.numer_faktury;
            /*
                        dt1.Rows.Add(faktura.numer_faktury);
                        dt1.Rows.Add(faktura.data_wystawienia);
                        dt1.Rows.Add(faktura.data_sprzedazy);
                        dt1.Rows.Add(faktura.miejsce_wystawienia);
                        dt1.Rows.Add(faktura.id_sprzedawca);
                        dt1.Rows.Add(faktura.id_nabywca);
                        dt1.Rows.Add(faktura.id_sposob_platnosci);
                        dt1.Rows.Add(faktura.termin_platnosci);
                        dt1.Rows.Add(faktura.id_typu_faktury);
                        dt1.Rows.Add(faktura.uwagi);
                        dt1.Rows.Add(faktura.uwagi_wewnetrzne);
            */



            //SqlDataAdapter da = new SqlDataAdapter("exec dbo.spFaktury_GetByNumer " + "\'" + faktura.numer_faktury + "\'", cnnstr);


            //reportViewer1.LocalReport.DataSources.Clear();
            //ReportDataSource dataSource2 = new ReportDataSource("DataSet_Faktury", ds.Tables[0]);
            //reportViewer1.LocalReport.DataSources.Add(dataSource2);
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
        }
    }
}
