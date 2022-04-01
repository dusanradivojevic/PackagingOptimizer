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
            var palete = Generator.GenerisiPalete(10, 10, 10, 5, 6);
            //var kontejneri = Generator.GenerisiKontejnere(10, 10, 10, 10, 1, 1, 10);
            //var paneli = Generator.GenerisiPanele(1, 8, 1, 7, 0, 0, 10);
            var paneli = GenerisiPaneleIzPDFPrimera();

            var ispunjeniUslovi = ProveraOsnovnihOgranicenja(paneli, palete);
            if (ispunjeniUslovi == false)
            {
                Console.WriteLine(Environment.NewLine + 
                    "Nisu ispunjeni osnovni uslovi za vršenje optimizacije.");
                Console.ReadKey();
                return;
            }

            //paneli = Sortiraj.PoPovrsini(paneli, SmerSortiranja.Opadajuce);

            var paneliKojiNisuStali = FiniteFirstFit.Spakuj(paneli, palete);
            palete = VratiPopunjenePalete(palete);
            IspisiPalete(palete);

            Console.WriteLine($"Minimalni broj paleta za pakovanje ovih panela je: {palete.Count}");
            Console.WriteLine($"Ukupan broj iskorišćenih kontejnera (nivoa pakovanja) je: {VratiBrojPopunjenihKontejnera(palete)}");
            Console.WriteLine($"Broj panela za koje nije bilo mesta: {paneliKojiNisuStali.Count}");
            Console.ReadKey();
        }

        private static void IspisiPalete(ICollection<Paleta> palete)
        {
            for(int i = 0; i < palete.Count; i++)
            {
                Console.WriteLine($"Paleta {i}:");
                IspisiNeprazneKontejnere(palete.ElementAt(i).Nivoi);
            }
        }

        private static int VratiBrojPopunjenihKontejnera(ICollection<Paleta> palete)
        {
            return palete.Sum(p => p.Nivoi.Where(k => k.SpakovaniPaneli.Count > 0).Count());
        }

        private static ICollection<Paleta> VratiPopunjenePalete(ICollection<Paleta> palete)
        {
            return palete.Where(p => p.Nivoi.Any(k => k.SpakovaniPaneli.Count > 0)).ToList();
        }

        private static bool ProveraOsnovnihOgranicenja(ICollection<Panel> paneli, ICollection<Paleta> palete)
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

        private static ICollection<Panel> GenerisiPaneleIzPDFPrimera()
        {
            return new List<Panel>()
            {
                new Panel(5,6,0),
                new Panel(8,5,0),
                new Panel(5,4,0),
                new Panel(4,3,0),
                new Panel(9,3,0),
                new Panel(1,2,0),
                new Panel(4,1,0),
                new Panel(5,6,0),
                new Panel(8,5,0),
                new Panel(5,4,0),
                new Panel(4,3,0),
                new Panel(9,3,0),
                new Panel(1,2,0),
                new Panel(1,2,0),
                new Panel(1,2,0),
                new Panel(4,1,0),

                //new Panel(5,6,0),
                //new Panel(8,5,0),
                //new Panel(5,4,0),
                //new Panel(4,3,0),
                //new Panel(9,3,0),
                //new Panel(1,2,0),
                //new Panel(4,1,0),
                //new Panel(5,6,0),
                //new Panel(8,5,0),
                //new Panel(5,4,0),
                //new Panel(4,3,0),
                //new Panel(9,3,0),
                //new Panel(1,2,0),
                //new Panel(4,1,0),
                //new Panel(5,6,0),
                //new Panel(8,5,0),
                //new Panel(5,4,0),
                //new Panel(4,3,0),
                //new Panel(9,3,0),
                //new Panel(1,2,0),
                //new Panel(4,1,0),
                //new Panel(5,6,0),
                //new Panel(8,5,0),
                //new Panel(5,4,0),
                //new Panel(4,3,0),
                //new Panel(9,3,0),
                //new Panel(1,2,0),
                //new Panel(4,1,0),
                //new Panel(5,6,0),
                //new Panel(8,5,0),
                //new Panel(5,4,0),
                //new Panel(4,3,0),
                //new Panel(9,3,0),
                //new Panel(1,2,0),
                //new Panel(4,1,0),
            };
        }

        private static ICollection<Kontejner> VratiPopunjeneKontejnere(ICollection<Kontejner> kontejneri)
        {
            return kontejneri.Where(k => k.SpakovaniPaneli.Count > 0).ToList();
        }

        private static void IspisiNeprazneKontejnere(ICollection<Kontejner> kontejneri)
        {
            for(int k = 0; k < kontejneri.Count; k++)
            {
                var kontejner = kontejneri.ElementAt(k);
                if (kontejner.SpakovaniPaneli.Count == 0) continue;

                Console.WriteLine($"Nivo {k}:");
                for (int i = 9; i >= 0; i--)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(kontejner.MatricaProstora[i, j] != 0 ? $"{kontejner.MatricaProstora[i, j]}" : "-");
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
