using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.Data;

using Microsoft.Extensions.Configuration;

namespace FakturniakUI
{
    public partial class FormFaktura : Form
    {
        readonly ISqlDataAccess dataAccess = new SqlDataAccess();


        //List<ModelKontrahent> sprzedawcy;
        //List<ModelKontrahent> nabywcy;

        List<ModelProdukt> produkty;


        public FormFaktura()
        {
            InitializeComponent();
        }

        private async void FormFaktura_Load(object sender, EventArgs e)
        {
            IDataProdukty dataProdukty = new DataProdukty(dataAccess);
            await Task.Run(() => produkty = dataProdukty.Get().Result.ToList());

            populateProdukty(produkty.Count);

            // poniżej dodawałem produkty do listboxa, ale to źle wygląda, stworzę user control
            /*
            foreach (ModelProdukt produkt in produkty)
            {
                string nazwa = produkt.nazwa;
                float cena_netto = produkt.cena_netto;
                float cena_brutto = produkt.cena_brutto;

                string produktStr = nazwa + " | netto: " + cena_netto + " | brutto: " + cena_netto;

                listBoxProdukty.Items.Add(produktStr);
            }
            */

            /*
            IDataKontrahenci dataKontrahenci = new DataKontrahenci(dataAccess);
            
            
            await Task.Run(() => sprzedawcy = dataKontrahenci.Get().Result.ToList());
            await Task.Run(() => nabywcy = dataKontrahenci.Get().Result.ToList());
            */
        }

        private void populateProdukty(int count)
        {
            UCProdukt[] ucProdukty = new UCProdukt[count];
            
            for(int i = 0; i < count; i++)
            {
                UCProdukt tempUcProdukt = new UCProdukt();

                string nazwa = produkty[i].nazwa;
                float cena_netto = produkty[i].cena_netto;
                float cena_brutto = produkty[i].cena_brutto;

                tempUcProdukt.nazwa = nazwa;
                tempUcProdukt.cena_netto = cena_netto;
                tempUcProdukt.cena_brutto = cena_brutto;


                ucProdukty[i] = tempUcProdukt;

                if (flowLayoutPanel1.Controls.Count < 0)
                    flowLayoutPanel1.Controls.Clear();
                else
                    flowLayoutPanel1.Controls.Add(ucProdukty[i]);
            }
        }

        private void Wystaw_Click(object sender, EventArgs e)
        {

        }
    }
}









/*
IDataFaktury dataKontrahenci = new DataFaktury(dataAccess);
Task<ModelFaktura> kontrahent = dataKontrahenci.Load("FV 002/07-2022");

string numer = "";

await Task.Run(() => { numer = kontrahent.Result.miejsce_wystawienia; });

textBox1.Text = numer;
*/
