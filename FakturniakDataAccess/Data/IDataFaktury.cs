﻿//  Copyright (C) 2022 Jacek Gałuszka
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

using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataFaktury
    {
        Task Delete(string _numer_faktury);
        Task<IEnumerable<ModelFaktura>> Get();
        Task<string> GetNumerFaktury(int? _numer, int _id_typu_faktury);
        Task Insert(ModelFaktura f);
        Task<ModelFaktura> Load(string _numer_faktury);
        Task<IEnumerable<ModelFaktura>> Search(string _input);
    }
}