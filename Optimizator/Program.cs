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
            //var paneli = Generator.GenerisiPanele(1, 8, 1, 7, 10);
            var paneli = GenerisiPaneleIzPDFPrimera();            

            //paneli = Sortiraj.PoPovrsini(paneli, SmerSortiranja.Opadajuce);

            kontejneri = FiniteFirstFit.Optimizuj(paneli, kontejneri.ToList());
            kontejneri = VratiPopunjeneKontejnere(kontejneri);
            IspisiKontejnere(kontejneri);

            Console.WriteLine($"Minimalni broj kontejnera za pakovanje ovih panela je: {kontejneri.Count}");
            Console.ReadKey();

            kontejneri = IsprazniKontejnere(kontejneri.ToList());
        }

        private static ICollection<Panel> GenerisiPaneleIzPDFPrimera()
        {
            return new List<Panel>()
            {
                new Panel(5,6),
                new Panel(8,5),
                new Panel(5,4),
                new Panel(4,3),
                new Panel(9,3),
                new Panel(1,2),
                new Panel(4,1),
            };
        }

        private static ICollection<Kontejner> VratiPopunjeneKontejnere(ICollection<Kontejner> kontejneri)
        {
            return kontejneri.Where(k => k.SpakovaniPaneli.Count > 0).ToList();
        }

        private static void IspisiKontejnere(ICollection<Kontejner> kontejneri)
        {
            foreach(Kontejner k in kontejneri)
            {
                if (k.SpakovaniPaneli.Count == 0) continue;
                for (int i = 9; i >= 0; i--)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(k.MatricaProstora[i, j] ? "X" : "-");
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
