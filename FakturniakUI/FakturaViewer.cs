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
using FakturniakUI.DataSets;

namespace FakturniakUI
{
    public partial class FakturaViewer : Form
    {
        private ModelFaktura faktura = new ModelFaktura();
        private List<ModelMTMFakturaProdukt> produkty = new List<ModelMTMFakturaProdukt>();

        public FakturaViewer(ModelFaktura _faktura, List<ModelMTMFakturaProdukt> _produkty)
        {
            InitializeComponent();

            faktura = _faktura;
            produkty = _produkty;
        }

        private void FakturaViewer_Load(object sender, EventArgs e)
        {
            // config
            reportViewer1.LocalReport.ReportEmbeddedResource = "FakturniakUI.ReportDefinitions.FakturaDokument.rdlc";
            ReportParameterCollection paramCollection = new ReportParameterCollection();
            paramCollection.Add(new ReportParameter("ReportName", faktura.numer_faktury));
            paramCollection.Add(new ReportParameter("ImagePath", FakturniakConfig.xmlFakturniakConfig.logo_path));
            reportViewer1.LocalReport.SetParameters(paramCollection);
            Helper h = new Helper();
            string cnnstr = h.getConnectionString("FakturniakDB");
            // config

            DataSet1 ds = new DataSet1();
            DataTable dt1 = new DataTable("faktura");


            using (SqlConnection connection = new SqlConnection(cnnstr))
            {
                using (SqlCommand sqlCommand = new SqlCommand("dbo.spFaktury_GetByNumer", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@numer_faktury", SqlDbType.VarChar).Value = faktura.numer_faktury;

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var da = new SqlDataAdapter(sqlCommand))
                    {
                        da.Fill(dt1);
                        ds.Tables.Add(dt1);
                    }
                }
            }

            //SqlDataAdapter da = new SqlDataAdapter("exec dbo.spFaktury_GetByNumer " + "\'" + faktura.numer_faktury + "\'", cnnstr);


            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource datasource = new ReportDataSource("DataSet_Report", ds.Tables[0]);
            reportViewer1.LocalReport.DataSources.Add(datasource);
            reportViewer1.RefreshReport();

            /*reportViewer1.LocalReport.ReportPath = @"E:\repos\Fakturniak\FakturniakUI\ReportDefinitions\FakturaDokument.rdlc";
            //reportViewer1.LocalReport.ReportEmbeddedResource = "Fakturniak.FakturniakUI.ReportDefinitions.FakturaDokument.rdlc";
            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.EnableExternalImages = true;
            ReportParameter reportParameter = new ReportParameter("ImagePath", FakturniakConfig.xmlFakturniakConfig.logo_path);
            ReportParameterCollection parameterCollection = new ReportParameterCollection();
            parameterCollection.Add(new ReportParameter("ReportName", faktura.numer_faktury));
            reportViewer1.LocalReport.DisplayName = "Podgląd faktury";
            reportViewer1.LocalReport.SetParameters(parameterCollection);*/
        }
    }
}
