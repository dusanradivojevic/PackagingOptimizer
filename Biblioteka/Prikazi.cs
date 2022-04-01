using Biblioteka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public static class Prikazi
    {
        public static void NepraznePalete(ICollection<Paleta> palete)
        {
            for (int i = 0; i < palete.Count; i++)
            {
                if (palete.ElementAt(i).JeNeprazna == false) continue;
                Console.WriteLine($"Paleta {i}:");
                IspisiNeprazneKontejnere(palete.ElementAt(i).Nivoi);
            }
        }
        private static void IspisiNeprazneKontejnere(ICollection<Kontejner> kontejneri)
        {
            for (int k = 0; k < kontejneri.Count; k++)
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
    }
}
