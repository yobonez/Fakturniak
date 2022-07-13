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
            // Memory leak fdfhadfhdafahfdafrdfadsaads

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

                    int ilosc = 1;

                    await Task.Run(() => tempStawkaVAT = dataStawkiVAT.Load(child_control.id_stawki).Result);
                    await Task.Run(() => tempJednostkaMiary = dataJednostkiMiary.Load(child_control.id_jednostki).Result);

                    bool addRowNeeded = true;


                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        // haha, męczyłem się nad tym z 1,5 godziny, wystarczyło
                        // głupi pytajnik postawić
                        if (row.Cells[1].Value?.ToString() == child_control.nazwa)
                        {
                            addRowNeeded = false;

                            ilosc = Int32.Parse(row.Cells["Ilosc"].Value.ToString());
                            ilosc++;
                            row.Cells["Ilosc"].Value = ilosc;
                        }
                    }

                    if (addRowNeeded)
                    {
                        object[] rowArrToAdd = {child_control.id_produktu,
                                                child_control.nazwa,
                                                tempJednostkaMiary.nazwa,
                                                ilosc,
                                                child_control.cena_netto,
                                                child_control.cena_brutto,
                                                tempStawkaVAT.wartosc,
                                                "kwotavat2test",
                                                "wbruttotest"};

                        this.dataGridView1.Rows.Add(rowArrToAdd);
                    }


                }
            }
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
    }
}
