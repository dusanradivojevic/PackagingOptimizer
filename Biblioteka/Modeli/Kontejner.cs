using System;
using System.Collections.Generic;

namespace Biblioteka.Modeli
{
    public class Kontejner : Pravougaonik
    {
        public Kontejner(int sirina, int visina, int nosivost) : base(sirina, visina)
        {
            Nosivost = nosivost;
            SpakovaniPaneli = new List<Panel>();
            GenerisiMatricu();
            SpakovanaMasa = 0;
            VisinaAktivnogNivoa = 0;
            VisinaSledecegNivoa = 0;
            SirinaAktivnogNivoa = 0;
        }

        private void GenerisiMatricu()
        {
            MatricaProstora = new bool[Sirina, Visina]; // podrazumevana vrednost je 'false'
        }

        public int Nosivost { get; private set; }
        public int SpakovanaMasa { get; private set; }
        public int SirinaAktivnogNivoa { get; private set; }
        public int VisinaAktivnogNivoa { get; private set; } // Pocetna visina trenutnog nivoa
        public int VisinaSledecegNivoa { get; private set; } // Pocetna visina sledeceg nivoa (= krajnja (gornja) visina trenutnog nivoa)
        public bool[,] MatricaProstora { get; private set; }
        public ICollection<Panel> SpakovaniPaneli { get; set; }
        public bool JePun { get; set; }
        public bool SmestiPanel(Panel panel)
        {
            if (panel.Sirina > Sirina || 
                panel.Visina > Visina || 
                panel.Masa > Nosivost)
            {
                return false;
            }

            // Da li kontejner ima dovoljnu raspoloživu nosivost za panel
            if (Nosivost - SpakovanaMasa < panel.Masa)
            {
                return false;
            }

            // Može da se smesti u širinu aktivnog nivoa?
            if (Sirina - SirinaAktivnogNivoa >= panel.Sirina && 
                (VisinaSledecegNivoa >= panel.Visina || VisinaSledecegNivoa == 0))
            {
                return SmestiPanelNaAktivniNivo(panel);
            }

            // Ne može da se smesti na aktivni nivo,
            // da li može na sledeći?
            if (Visina - VisinaSledecegNivoa >= panel.Visina)
            {
                return SmestiPanelNaSledeciNivo(panel);
            }
            
            return false;
        }        

        private bool SmestiPanelNaAktivniNivo(Panel panel)
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
            return true;
        }

        private bool SmestiPanelNaSledeciNivo(Panel panel)
        {
            // Započni novi nivo i smesti panel tamo.
            VisinaAktivnogNivoa = VisinaSledecegNivoa;
            VisinaSledecegNivoa += panel.Visina;
            SirinaAktivnogNivoa = panel.Sirina;
            SpakovanaMasa += panel.Masa;

            AzurirajMatricu(0, panel.Sirina, VisinaAktivnogNivoa, panel.Visina);

            SpakovaniPaneli.Add(panel);
            return true;
        }

        private void AzurirajMatricu(int pocetnaSirina, int sirinaPanela, int pocetnaVisina, int visinaPanela)
        {
            for(int i = pocetnaVisina; i < pocetnaVisina + visinaPanela; i++)
            {
                for (int j = pocetnaSirina; j < pocetnaSirina + sirinaPanela; j++)
                {
                    MatricaProstora[i, j] = true;
                }
            }
        }
    }
}
