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
            var palete = Generator.GenerisiPalete(10, 10, 50, 5, 6);
            //var paneli = Generator.GenerisiPanele(1, 8, 1, 7, 1, 5, 10);
            var paneli = Generator.GenerisiPaneleIzPDFPrimera();

            //var ispunjeniUslovi = ProveraOsnovnihOgranicenja(paneli, palete);
            //if (ispunjeniUslovi == false)
            //{
            //    Console.WriteLine(Environment.NewLine + 
            //        "Nisu ispunjeni osnovni uslovi za vršenje optimizacije.");
            //    Console.ReadKey();
            //    return;
            //}

            //paneli = Sortiraj.PoPovrsini(paneli, SmerSortiranja.Opadajuce);

            FiniteFirstFit.Spakuj(paneli, palete);
            Prikazi.NepraznePalete(palete);

            Console.WriteLine($"Minimalni broj paleta za pakovanje ovih panela je: {VratiBrojPopunjenihPaleta(palete)}");
            Console.WriteLine($"Ukupan broj iskorišćenih kontejnera (nivoa pakovanja) je: {VratiBrojPopunjenihKontejnera(palete)}");
            Console.WriteLine($"Broj panela za koje nije bilo mesta: {VratiBrojNespakovanihPanela(paneli)}");
            Console.ReadKey();
        }

        private static int VratiBrojNespakovanihPanela(ICollection<Panel> paneli)
        {
            return paneli.Where(p => !p.JeSpakovan).Count();
        }

        private static int VratiBrojPopunjenihKontejnera(ICollection<Paleta> palete)
        {
            return palete.Sum(p => p.Nivoi.Where(k => k.SpakovaniPaneli.Count > 0).Count());
        }

        private static int VratiBrojPopunjenihPaleta(ICollection<Paleta> palete)
        {
            return palete.Where(p => p.Nivoi.Any(k => k.SpakovaniPaneli.Count > 0)).Count();
        }
    }
}
