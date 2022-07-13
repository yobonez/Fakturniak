using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Models
{
    public class ModelMTMFakturaProdukt
    {
        public string numer_faktury { get; set; }
        public int id_produktu { get; set; }
        public int ilosc { get; set; }
    }
}
