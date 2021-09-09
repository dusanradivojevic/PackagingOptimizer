using Biblioteka.Modeli;
using System;
using System.Collections.Generic;

namespace Biblioteka
{
    public static class Generator
    {
        public static Random Random = new Random();
        public static ICollection<Kontejner> GenerisiKontejnere(int minSirina, int maxSirina, int minVisina, int maxVisina, int broj)
        {
            var kontejneri = new List<Kontejner>();

            for (int i = 0; i < broj; i++)
            {
                var k = new Kontejner(Random.Next(minSirina, maxSirina),
                    Random.Next(minVisina, maxVisina));

                kontejneri.Add(k);
            }

            return kontejneri;
        }
        public static ICollection<Panel> GenerisiPanele(int minSirina, int maxSirina, int minVisina, int maxVisina, int broj)
        {
            var paneli = new List<Panel>();

            for (int i = 0; i < broj; i++)
            {
                var k = new Panel(Random.Next(minSirina, maxSirina),
                    Random.Next(minVisina, maxVisina));

                paneli.Add(k);
            }

            return paneli;
        }
    }
}
