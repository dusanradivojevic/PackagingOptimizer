using Biblioteka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteka
{
    public class FiniteFirstFit
    {
        public static void Spakuj(ICollection<Panel> paneli, ICollection<Paleta> palete)
        {
            foreach (Panel panel in paneli)
            {
                foreach(Paleta paleta in palete)
                {
                    if (paleta.JePuna ||
                        paleta.MozeStati(panel) == false)
                    {
                        continue;
                    }

                    paleta.SpakujNaPaletu(panel);

                    if (panel.JeSpakovan)
                    {
                        break;
                    }
                }
            }
        }
        public static bool ProveraOsnovnihOgranicenja(ICollection<Panel> paneli, ICollection<Paleta> palete)
        {
            var ukupnaPovrsinaPanela = paneli.Sum(p => p.Povrsina);
            var ukupnaPovrsinaZaPakovanje = palete.Sum(p => p.Povrsina * p.BrojNivoa);
            if (ukupnaPovrsinaPanela > ukupnaPovrsinaZaPakovanje)
            {
                Console.WriteLine("Ukupna površina panela ne sme biti veća od ukupne površine za pakovanje!");
                return false;
            }

            var ukupnaMasaPanela = paneli.Sum(p => p.Masa);
            var ukupnaNosivostPaleta = palete.Sum(p => p.Nosivost);
            if (ukupnaMasaPanela > ukupnaNosivostPaleta)
            {
                Console.WriteLine("Ukupna masa panela ne sme biti veća od ukupne nosivosti paleta!");
                return false;
            }

            return true;
        }
    }
}
