using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Modeli
{
    public class Paleta : Pravougaonik
    {
        public Paleta(int sirina, int visina, int nosivost, int brojNivoa) : base(sirina, visina)
        {
            Nosivost = nosivost;
            BrojNivoa = brojNivoa;
            Kontejneri = Generator.GenerisiKontejnere(this);
        }

        public ICollection<Kontejner> Kontejneri { get; set; } // Nivoi
        public int Nosivost { get; private set; }
        public int UkupnaSpakovanaMasa => Kontejneri.Sum(k => k.SpakovanaMasa);
        public bool JePuna => UkupnaSpakovanaMasa >= 0.95 * Nosivost;
        public int BrojNivoa { get; private set; }
        public bool ImaSveNivoe => BrojNivoa == Kontejneri.Count;

        public bool MozeStati(Panel panel)
        {
            return Nosivost - UkupnaSpakovanaMasa >= panel.Masa;
        }
    }
}
