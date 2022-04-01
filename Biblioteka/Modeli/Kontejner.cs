using System;
using System.Collections.Generic;

namespace Biblioteka.Modeli
{
    public class Kontejner : Pravougaonik
    {
        public Kontejner(int sirina, int visina) : base(sirina, visina)
        {
            SpakovaniPaneli = new List<Panel>();
            JePun = false;
            GenerisiMatricu();
            SpakovanaMasa = 0;
            VisinaAktivnogNivoa = 0;
            VisinaSledecegNivoa = 0;
            SirinaAktivnogNivoa = 0;
        }

        private void GenerisiMatricu()
        {
            MatricaProstora = new int[Sirina, Visina]; // podrazumevana vrednost je '0'
        }

        public int SpakovanaMasa { get; private set; }
        public int SirinaAktivnogNivoa { get; private set; }
        public int VisinaAktivnogNivoa { get; private set; } // Pocetna visina trenutnog nivoa
        public int VisinaSledecegNivoa { get; private set; } // Pocetna visina sledeceg nivoa (= krajnja (gornja) visina trenutnog nivoa)
        public int[,] MatricaProstora { get; private set; }
        public ICollection<Panel> SpakovaniPaneli { get; set; }
        public bool JePun { get; set; }
        public void SpakujUKontejner(Panel panel)
        {
            if (panel.Sirina > Sirina || 
                panel.Visina > Visina)
            {
                return;
            }

            // Može da se smesti u širinu aktivnog nivoa?
            if (Sirina - SirinaAktivnogNivoa >= panel.Sirina && 
                (VisinaSledecegNivoa - VisinaAktivnogNivoa >= panel.Visina || 
                VisinaSledecegNivoa == 0)
               )
            {
                SpakujPanelNaAktivniNivo(panel);
                return;
            }

            // Ne može da se smesti na aktivni nivo,
            // da li može na sledeći?
            if (Visina - VisinaSledecegNivoa >= panel.Visina)
            {
                SpakujPanelNaSledeciNivo(panel);
                return;
            }
        }        

        private void SpakujPanelNaAktivniNivo(Panel panel)
        {
            AzurirajMatricu(SirinaAktivnogNivoa, panel.Sirina, VisinaAktivnogNivoa, panel.Visina);

            if (VisinaSledecegNivoa == 0)
            {
                // Samo prilikom pakovanja prvog panela
                // jer ce ubudece ova izmena biti radjena
                // prilikom otvaranja novog nivoa
                VisinaSledecegNivoa += panel.Visina;
            }
            SirinaAktivnogNivoa += panel.Sirina;
            SpakovanaMasa += panel.Masa;

            SpakovaniPaneli.Add(panel);
            panel.JeSpakovan = true;
            ProveriIskoriscenostKontejnera();
        }

        private void SpakujPanelNaSledeciNivo(Panel panel)
        {
            // Započni novi nivo i smesti panel tamo.
            VisinaAktivnogNivoa = VisinaSledecegNivoa;
            VisinaSledecegNivoa += panel.Visina;
            SirinaAktivnogNivoa = panel.Sirina;
            SpakovanaMasa += panel.Masa;

            AzurirajMatricu(0, panel.Sirina, VisinaAktivnogNivoa, panel.Visina);

            SpakovaniPaneli.Add(panel);
            panel.JeSpakovan = true;
            ProveriIskoriscenostKontejnera();
        }

        private void AzurirajMatricu(int pocetnaSirina, int sirinaPanela, int pocetnaVisina, int visinaPanela)
        {
            for(int i = pocetnaVisina; i < pocetnaVisina + visinaPanela; i++)
            {
                for (int j = pocetnaSirina; j < pocetnaSirina + sirinaPanela; j++)
                {
                    MatricaProstora[i, j] = SpakovaniPaneli.Count % 9 + 1;
                }
            }
        }

        private void ProveriIskoriscenostKontejnera()
        {
            if (Visina - VisinaSledecegNivoa == 0 &&
                Sirina - SirinaAktivnogNivoa == 0)
            {
                JePun = true;
            }
        }
    }
}
