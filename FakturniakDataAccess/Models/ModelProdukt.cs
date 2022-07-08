using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Models
{
    public class ModelProdukt
    {
        public string nazwa { get; set; }
        public float cena { get; set; }
        public int id_jednostki { get; set; }
        public int id_stawki { get; set; }
    }
}
