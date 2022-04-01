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
            Nivoi = Generator.GenerisiKontejnere(this);
        }

        public ICollection<Kontejner> Nivoi { get; set; } 
        public int Nosivost { get; private set; }
        public int UkupnaSpakovanaMasa => Nivoi.Sum(k => k.SpakovanaMasa);
        public bool JePuna => UkupnaSpakovanaMasa >= 0.95 * Nosivost;
        public bool JeNeprazna => Nivoi.Any(k => k.SpakovaniPaneli.Count > 0);
        public int BrojNivoa { get; private set; }        
        public bool MozeStati(Panel panel)
        {
            return Nosivost - UkupnaSpakovanaMasa >= panel.Masa;
        }
        public void SpakujNaPaletu(Panel panel)
        {
            foreach (Kontejner kontejner in Nivoi)
            {
                if (kontejner.JePun) continue;

                kontejner.SpakujUKontejner(panel);

                if (panel.JeSpakovan) return;
            }
        }
    }
}
