using Biblioteka;
using Biblioteka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Optimizator
{
    class Program
    {
        static void Main(string[] args)
        {
            var kontejneri = Generator.GenerisiKontejnere(10, 10, 10, 10, 10);
            var paneli = Generator.GenerisiPanele(1, 8, 1, 7, 10);

            paneli = Sortiraj.PoPovrsini(paneli, SmerSortiranja.Opadajuce);

            kontejneri = FiniteFirstFit.Optimizuj(paneli, kontejneri.ToList());

            Console.WriteLine($"Minimalni broj kontejnera za pakovanje ovih panela je: {kontejneri.Count}");
            Console.ReadKey();
        }

        private static void IspisiKontejnere(ICollection<Kontejner> kontejneri)
        {
            foreach(Kontejner k in kontejneri)
            {
                if (k.SpakovaniPaneli.Count == 0) continue;
                for (int i = 9; i >= 0; i--)
                {
                    for (int j = 9; j >= 0; j--)
                    {
                        Console.Write(k.MatricaProstora[j, i] ? "-" : "X");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n");
            }
        }

        private static List<Kontejner> IsprazniKontejnere(List<Kontejner> kontejneri)
        {
            for (int i = 0; i < kontejneri.Count; i++)
            {
                kontejneri[i] = new Kontejner(10, 10);
            }
            return kontejneri;
        }
    }
}
