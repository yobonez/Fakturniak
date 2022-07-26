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

using FakturniakDataAccess.Data;
using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakUI.Config;
using System;
using System.Windows.Forms;

namespace FakturniakUI
{
    public partial class FormNowyKontrahent : Form
    {
        ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);

        public FormNowyKontrahent()
        {
            InitializeComponent();
        }
        private void FormNowyKontrahent_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = FakturniakConfig.xmlFakturniakConfig.logo_path;
        }

        private bool ValidateKontrahent()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show(this, "Pola: \"Imię\", \"Nazwisko\", oraz \"PESEL\" muszą być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show(this, "Pole: \"Nazwa\" musi być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (maskedTextBox3.Text == "" && maskedTextBox4.Text == "" && maskedTextBox5.Text == "")
            {
                MessageBox.Show(this, "Jedno z pól: NIP, REGON lub KRS musi być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (textBox8.Text == "" || maskedTextBox6.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show(this, "Pola: \"Adres\", \"Kod pocztowy\" oraz \"Miasto\" muszą być wypełnione.", "Błąd przy wprowadzaniu danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModelKontrahent nowyKontrahent = new ModelKontrahent()
            {
                imie = textBox1.Text,
                nazwisko = textBox2.Text,
                pesel = maskedTextBox1.Text,
                telefon = maskedTextBox2.Text,
                email = textBox12.Text,
                nazwa = textBox3.Text,
                nip = maskedTextBox3.Text,
                regon = maskedTextBox4.Text,
                krs = maskedTextBox5.Text,
                adres = textBox8.Text,
                kod_pocztowy = maskedTextBox6.Text,
                miasto = textBox10.Text
            };

            if (ValidateKontrahent())
            { 
                IDataKontrahenci dataKontrahenci = new DataKontrahenci(dataAccess);
                dataKontrahenci.Insert(nowyKontrahent);
                MessageBox.Show(this, "Pomyślnie wstawiono kontrahenta do bazy.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
