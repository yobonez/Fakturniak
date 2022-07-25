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
using System.Windows.Forms;
using FakturniakUI.Config;

namespace FakturniakUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //FakturniakConfig cfg = new FakturniakConfig();
            //FakturniakConfigModel cfgModel = new FakturniakConfigModel(); // 19.07

            // 20.07 config jako klasa statyczna
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
        }

        private void NowaFaktura_Click(object sender, EventArgs e)
        {
            // TODO: focus na Form1, jak zamkniesz okno faktur
            FormFaktura formFaktura = new FormFaktura();
            formFaktura.Show();
        }

        private void produktToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
