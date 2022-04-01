using Biblioteka.Modeli;
using System;
using System.Collections.Generic;

namespace Biblioteka
{
    public static class Generator
    {
        public static Random Random = new Random();
        public static ICollection<Paleta> GenerisiPalete(int sirina, int visina, int nosivost, int brojNivoa, int broj)
        {
            var palete = new List<Paleta>();

            for (int i = 0; i < broj; i++)
            {
                var k = new Paleta(sirina, visina, nosivost, brojNivoa);

                palete.Add(k);
            }

            return palete;
        }
        /*public static ICollection<Kontejner> GenerisiKontejnere(int minSirina, int maxSirina, int minVisina, int maxVisina, int broj)
        {
            var kontejneri = new List<Kontejner>();

            for (int i = 0; i < broj; i++)
            {
                var k = new Kontejner(Random.Next(minSirina, maxSirina),
                                      Random.Next(minVisina, maxVisina));

                kontejneri.Add(k);
            }

            return kontejneri;
        } */
        public static ICollection<Kontejner> GenerisiKontejnere(int sirina, int visina, int broj)
        {
            var kontejneri = new List<Kontejner>();

            for (int i = 0; i < broj; i++)
            {
                var k = new Kontejner(sirina, visina);

                kontejneri.Add(k);
            }

            return kontejneri;
        }
        public static ICollection<Kontejner> GenerisiKontejnere(Paleta paleta)
        {
            return GenerisiKontejnere(paleta.Sirina, paleta.Visina, paleta.BrojNivoa);
        }
        public static ICollection<Panel> GenerisiPanele(int minSirina, int maxSirina, int minVisina, int maxVisina, int minMasa, int maxMasa, int broj)
        {
            var paneli = new List<Panel>();

            for (int i = 0; i < broj; i++)
            {
                var k = new Panel(Random.Next(minSirina, maxSirina),
                                  Random.Next(minVisina, maxVisina),
                                  Random.Next(minMasa, maxMasa));

                paneli.Add(k);
            }

            return paneli;
        }
    }
}
