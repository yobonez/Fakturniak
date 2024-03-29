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
    public interface IDataProdukty
    {
        Task Delete(int _id_produktu);
        Task<IEnumerable<ModelProdukt>> Get();
        Task Insert(ModelProdukt p);
        Task InsertBrutto(ModelProdukt p);
        Task InsertNetto(ModelProdukt p);
        Task<IEnumerable<ModelProdukt>> Search(string _input);
        Task<ModelProdukt> Load(int _id_produktu);
    }
}