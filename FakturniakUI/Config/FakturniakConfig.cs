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

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FakturniakUI.Config
{
    public class FakturniakConfig
    {
        FakturniakConfigModel xmlFakturniakConfig = new FakturniakConfigModel()
        {
            id_zarejestrowany = -1,
            ostatni_zalogowany_uzytkownik = "Administrator",
            logo_path = ""
        };

        public FakturniakConfigModel Load(string filename)
        {
            var xmlSerializer = new XmlSerializer(typeof(FakturniakConfigModel));
            if (File.Exists(filename))
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    xmlFakturniakConfig = (FakturniakConfigModel)xmlSerializer.Deserialize(stream);
                    stream.Close();
                }
            }

            else
            {
                Write(filename, xmlFakturniakConfig);
            }

            return xmlFakturniakConfig;
        }

        public void Write(string filename, FakturniakConfigModel confModel)
        {
            if (!File.Exists(filename))
            {
                var file = File.Create(filename);
                file.Close();
            }

            var xmlSerializer = new XmlSerializer(typeof(FakturniakConfigModel));
            using (var writer = new StreamWriter(filename))
            {
                xmlSerializer.Serialize(writer, confModel);
            }

        }
    }

}
