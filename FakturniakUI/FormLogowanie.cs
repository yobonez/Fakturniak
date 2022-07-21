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
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using FakturniakDataAccess.DbAccess;
using FakturniakUI.Config;
using FakturniakUI.Properties;

namespace FakturniakUI
{
    public partial class FormLogowanie : Form
    {
        private bool success = false;

        public FormLogowanie()
        {
            InitializeComponent();
        }

        private void FormLogowanie_Load(object sender, EventArgs e)
        {
            //FakturniakConfigModel modelCfg = new FakturniakConfigModel();
            //FakturniakConfig config = new FakturniakConfig // 19.07

            this.CenterToScreen();
            // 20.07
            FakturniakConfig.Load("FakturniakConfig.xml");

            if (FakturniakConfig.xmlFakturniakConfig.logo_path == "")
                pictureBox1.Image = Resources.braklogo;
            else
                pictureBox1.ImageLocation = FakturniakConfig.xmlFakturniakConfig.logo_path;

            textBox1.Text = FakturniakConfig.xmlFakturniakConfig.ostatni_zalogowany_uzytkownik;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string pass = textBox2.Text;

            string cnnstr = $@"Server=localhost\FAKTURNIAKDB;Database=FakturniakDB;User Id={username};Password={pass};";

            using IDbConnection connection = new SqlConnection(cnnstr);
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                    success = true;
                }
                catch(System.Data.SqlClient.SqlException ex)
                {
                    string msg = "Nieprawidłowy login lub hasło.\n\n" + ex.Message;

                    MessageBox.Show(this, msg, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    success = false;
                }
            }

            if (!success)
                this.Focus();
            else
            {
                //FakturniakConfig config = new FakturniakConfig();
                //FakturniakConfigModel cfgModel = config.Load("FakturniakConfig.xml"); // 19.07

                FakturniakConfig.xmlFakturniakConfig.ostatni_zalogowany_uzytkownik = username;
                FakturniakConfig.Write("FakturniakConfig.xml", FakturniakConfig.xmlFakturniakConfig);

                // 20.07
                FakturniakConfig.username = username;
                FakturniakConfig.pass = pass;

                // 19.07
                //Helper helper = new Helper();
                //helper.setConnectionString("FakturniakDB", cnnstr);

                this.Close();
            }
        }

        private void FormLogowanie_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!success)
                Application.Exit();
        }
    }
}
