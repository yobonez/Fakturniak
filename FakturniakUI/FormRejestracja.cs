using System;
using System.Windows.Forms;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Data;
using System.Threading.Tasks;

namespace FakturniakUI
{

    // todo: zrob inifile lub jakiś inny config, w którym będzie przechowywany ID kontrahenta, który
    // wystawia faktury.
    public partial class FormRejestracja : Form
    {
        int obecny_krok = 0;
        readonly ISqlDataAccess dataAccess = new SqlDataAccess();

        ModelKontrahent dane;
        public FormRejestracja()
        {
            InitializeComponent();
        }
        private void FormRejestracja_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (obecny_krok == 0)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show(this, "Pola: \"Imię\", \"Nazwisko\", oraz \"PESEL\" muszą być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show(this, "Pole: \"Nazwa\" musi być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                {
                    MessageBox.Show(this, "Jedno z pól: NIP, REGON lub KRS musi być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
                {
                    MessageBox.Show(this, "Pola: \"Adres\", \"Kod pocztowy\" oraz \"Miasto\" muszą być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    NastepnyKrok(obecny_krok);
                }
            }

            else if (obecny_krok > 0)
            {
                if (obecny_krok == 1 && maskedTextBox1.Text == "                            ")
                {
                    MessageBox.Show(this, "Pole numeru konta bankowego jest puste.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NastepnyKrok(obecny_krok);
            }

            //try
            //{
            //}
            //catch (SqlException ex)
            //{
                //MessageBox.Show(this, "Baza danych wykryła nieprawidłowości w podanych danych: " + ex.Message, "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void NastepnyKrok(int krok)
        {
            if (krok == 0)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                button3.Visible = true;
                label1.Text = "W jaki sposób będziesz przyjmował wpłaty?";
                label2.Visible = false;
                button2.Visible = true;
                obecny_krok = ++krok;
            }
            else if (krok == 1)
            {
                panel2.Visible = false;
                button2.Visible = false;
                label1.Text = "Jeszcze tylko logo!";
                label2.Text = "Wymagane wymiary loga: 400x80";
                label2.Visible = true;
                panel3.Visible = true;
                obecny_krok = ++krok;
            }
            else if (krok == 2)
            {
                button2.Visible = false;
                button5.Visible = true;
                panel3.Visible = false;
                button1.Visible = false;
                label1.Text = "Zatwierdź zmiany.";
                label2.Text = "Dziękujemy za korzystanie z Fakturniaka!";
                obecny_krok = ++krok;
            }
        }
        
        private void KrokWstecz(int krok)
        {
            if (krok == 1)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                button3.Visible = false;
                label1.Text = "Witaj w aplikacji do fakturowania  \"Fakturniak\"!";
                label2.Visible = true;
                button2.Visible = false;
                obecny_krok = --krok;
            }
            else if (krok == 2)
            {
                panel2.Visible = true;
                label1.Text = "W jaki sposób będziesz przyjmował wpłaty?";
                button2.Visible = true;

                label2.Visible = false;
                panel3.Visible = false;
                obecny_krok = --krok;
            }
            else if (krok == 3)
            {
                button5.Visible = false;
                button1.Visible = true;
                panel3.Visible = true;
                label1.Text = "Jeszcze tylko logo!";
                label2.Text = "Wymagane wymiary loga: 400x80";
                obecny_krok = --krok;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NastepnyKrok(obecny_krok);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KrokWstecz(obecny_krok);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                label18.Text = openFileDialog1.FileName;
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
            else
                label18.Text = "Brak wybranego pliku";
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            IDataKontrahenci daneDoRejestracji = new DataKontrahenci(dataAccess);

            dane = new ModelKontrahent()
            {
                imie = textBox1.Text,
                nazwisko = textBox2.Text,
                nazwa = textBox3.Text,
                nip = textBox5.Text,
                regon = textBox6.Text,
                krs = textBox7.Text,
                pesel = textBox4.Text,
                email = textBox12.Text,
                telefon = textBox11.Text,
                adres = textBox8.Text,
                kod_pocztowy = textBox9.Text,
                miasto = textBox10.Text,
                numer_konta = maskedTextBox1.Text,
                swift = textBox13.Text
            };

            try
            {
                await Task.Run(() => daneDoRejestracji.Insert(dane));
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(this, "Wystąpił błąd przy walidacji danych przez bazę danych. Sprawdź poprawność wprowadzonych danych.\n" + ex.Message + "\n" + ex.StackTrace, "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

    }
}
