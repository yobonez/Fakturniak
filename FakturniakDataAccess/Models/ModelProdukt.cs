using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Models
{
    public class ModelProdukt
    {
        public int id_produktu { get; set; }
        public string nazwa { get; set; }
        public Decimal cena_netto { get; set; }
        public Decimal cena_brutto { get; set; }
        public int id_jednostki { get; set; }
        public int id_stawki { get; set; }
    }
}
