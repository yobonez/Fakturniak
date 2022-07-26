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
    public partial class FormNowySposobPlatnosci : Form
    {
        ISqlDataAccess dataAccess = new SqlDataAccess(FakturniakConfig.username, FakturniakConfig.pass);

        public FormNowySposobPlatnosci()
        {
            InitializeComponent();
        }
        private void NowySposobPlatnosci_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = FakturniakConfig.xmlFakturniakConfig.logo_path;
            this.CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModelSposobPlatnosci sposobPlatnosci = new ModelSposobPlatnosci() { nazwa = textBox1.Text };

            if (textBox1.Text != "")
            {
                IDataSposobyPlatnosci sposobyPlatnosci = new DataSposobyPlatnosci(dataAccess);
                sposobyPlatnosci.Insert(sposobPlatnosci);
                MessageBox.Show(this, $"Pomyślnie dodano sposób \"{sposobPlatnosci.nazwa}\".", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(this, "Wpisz coś w polu nazwy.", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
