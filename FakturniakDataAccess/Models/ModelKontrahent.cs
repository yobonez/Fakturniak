using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Models
{
    public class ModelKontrahent
    {
        public int id_kontrahenta { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string nazwa { get; set; }
        public string? nip { get; set; }
        public string? regon { get; set; }
        public string? krs { get; set; }
        public string? pesel { get; set; }
        public string? email { get; set; }
        public string? telefon { get; set; }
        public string adres { get; set; }
        public string kod_pocztowy { get; set; }
        public string miasto { get; set; }
        public string? numer_konta { get; set; }
        public string? swift { get; set; }
    }
}
