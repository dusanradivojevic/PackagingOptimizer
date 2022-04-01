using Biblioteka.Modeli;
using System;
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
                    if (k.JePun) continue;

                    panelJeSmesten = k.SmestiPanel(p);
                    if (panelJeSmesten)
                    {
                        ProveriIskoriscenostKontejnera(k);
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
                    var poslednjiDodatKontejner = listaAktivnihKontejnera.Last();
                    panelJeSmesten = poslednjiDodatKontejner.SmestiPanel(p);
                    if (panelJeSmesten)
                    {
                        ProveriIskoriscenostKontejnera(poslednjiDodatKontejner);
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
        private static void ProveriIskoriscenostKontejnera(Kontejner k)
        {
            if (k.Visina - k.VisinaSledecegNivoa == 0 &&
                k.Sirina - k.SirinaAktivnogNivoa == 0)
            {
                k.JePun = true;
            } 
        }

        public static ICollection<Panel> Spakuj(ICollection<Panel> paneli, ICollection<Paleta> palete)
        {
            var listaPanelaKojiNisuStali = new List<Panel>();
            // lista aktivnih paleta ?

            foreach (Panel panel in paneli)
            {
                bool panelJeSmesten = false;
                foreach(Paleta paleta in palete)
                {
                    if (paleta.JePuna ||
                        paleta.MozeStati(panel) == false)
                    {
                        continue;
                    }

                    foreach (Kontejner kontejner in paleta.Nivoi)
                    {
                        if (kontejner.JePun) continue;

                        panelJeSmesten = kontejner.SmestiPanel(panel);
                        if (panelJeSmesten)
                        {
                            ProveriIskoriscenostKontejnera(kontejner);
                            break;
                        }
                    }

                    if (panelJeSmesten)
                    {
                        break;
                    }
                }

                if (panelJeSmesten == false) 
                {
                    listaPanelaKojiNisuStali.Add(panel);
                }
            }

            return listaPanelaKojiNisuStali;
        }

    }
}
