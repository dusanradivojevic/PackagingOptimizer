using Biblioteka.Modeli;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteka
{
    public class FiniteFirstFit
    {
        public static ICollection<Kontejner> Optimizuj(ICollection<Panel> paneli, List<Kontejner> kontejneri)
        {
            var listaPanelaKojiNisuStali = new List<Panel>();
            var listaAktivnihKontejnera = new List<Kontejner>();
            int indexPoslednjegDodatogKontejnera = 0;

            foreach(Panel p in paneli)
            {
                bool panelJeSmesten = false;
                foreach(Kontejner k in listaAktivnihKontejnera)
                {
                    panelJeSmesten = k.SmestiPanel(p);
                    if (panelJeSmesten)
                    {
                        break;
                    }
                }

                if (panelJeSmesten) continue;
                
                // Pošto panel nije smešten,
                // dodajemo novi kontejner iz trenutno neaktivnih, raspoloživih kontejnera,
                // dok ne smestimo panel ili dok nam ne ponestane kontejnera.
                for(int i = indexPoslednjegDodatogKontejnera; i < kontejneri.Count; i++)
                {
                    listaAktivnihKontejnera.Add(kontejneri[i]);
                    indexPoslednjegDodatogKontejnera++;
                    panelJeSmesten = listaAktivnihKontejnera.Last().SmestiPanel(p);
                    if (panelJeSmesten)
                    {
                        break;
                    }
                }                    
                
                if (panelJeSmesten == false)
                {
                    listaPanelaKojiNisuStali.Add(p);
                }
            }

            return kontejneri;
        }
    }
}
