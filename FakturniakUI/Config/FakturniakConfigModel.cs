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

namespace FakturniakUI.Config
{
    public class FakturniakConfigModel
    {
        public int id_zarejestrowany { get; set; }
        public string ostatni_zalogowany_uzytkownik { get; set; }
        public string ostanie_miasto_wystawiania { get; set; }
        public string ostatni_sposob_platnosci { get; set; }
        public Decimal obecny_przychod { get; set; }
        public string logo_path { get; set; }
    }
}
