﻿using System;
using System.Collections.Generic;

namespace Biblioteka.Modeli
{
    public class Kontejner : Pravougaonik
    {
        public Kontejner(int sirina, int visina) : base(sirina, visina)
        {
            SpakovaniPaneli = new List<Panel>();
            GenerisiMatricu();
            VisinaAktivnogNivoa = 0;
            VisinaSledecegNivoa = 0;
            SirinaAktivnogNivoa = 0;
        }

        private void GenerisiMatricu()
        {
            MatricaProstora = new bool[Sirina, Visina]; // podrazumevana vrednost je 'false'
        }

        public int SirinaAktivnogNivoa { get; private set; }
        public int VisinaAktivnogNivoa { get; private set; } // Pocetna visina trenutnog nivoa
        public int VisinaSledecegNivoa { get; private set; } // Pocetna visina sledeceg nivoa (= krajnja (gornja) visina trenutnog nivoa)
        public bool[,] MatricaProstora { get; private set; }
        public ICollection<Panel> SpakovaniPaneli { get; set; }
        public bool SmestiPanel(Panel panel)
        {
            if (panel.Sirina > Sirina || panel.Visina > Visina) return false;

            if (Sirina - SirinaAktivnogNivoa >= panel.Sirina && 
                (VisinaSledecegNivoa >= panel.Visina || VisinaSledecegNivoa == 0))
            {
                // moze da se smesti u sirinu istog nivoa
                SmestiPanelUMatricu(SirinaAktivnogNivoa, panel.Sirina, VisinaAktivnogNivoa, panel.Visina);

                VisinaSledecegNivoa += panel.Visina;
                SirinaAktivnogNivoa += panel.Sirina;

                SpakovaniPaneli.Add(panel);
                return true;
            }

            // ne moze da se smesti na trenutni nivo, probaj na sledeci
            if (Visina - VisinaSledecegNivoa >= panel.Visina)
            {
                // zapocni novi nivo i smesti ga tamo
                VisinaAktivnogNivoa = VisinaSledecegNivoa;
                VisinaSledecegNivoa += panel.Visina;
                SirinaAktivnogNivoa = panel.Sirina;

                SmestiPanelUMatricu(0, panel.Sirina, VisinaAktivnogNivoa, panel.Visina);

                SpakovaniPaneli.Add(panel);
                return true;
            }
            
            return false;
        }
        private void SmestiPanelUMatricu(int pocetnaSirina, int sirinaPanela, int pocetnaVisina, int visinaPanela)
        {
            for(int i = pocetnaVisina; i < visinaPanela; i++)
            {
                for (int j = pocetnaSirina; j < sirinaPanela; j++)
                {
                    MatricaProstora[i, j] = true;
                }
            }
        }
    }
}