using System;

namespace FakturniakDataAccess.Models
{
    public class ModelFaktura
    {
        public string numer_faktury { get; set; }
        public DateTime data_wystawienia { get; set; }
        public DateTime data_sprzedazy { get; set; }
        public string miejsce_wystawienia { get; set; }
        public int id_sprzedawca { get; set; }
        public int id_nabywca { get; set; }
        public int id_sposob_platnosci { get; set; }
        public DateTime termin_platnosci { get; set; }
        public int id_typu_faktury { get; set; }
        public string uwagi { get; set; }
        public string uwagi_wewnetrzne { get; set; }
    }
}
