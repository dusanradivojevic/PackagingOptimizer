using Biblioteka.Modeli;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteka
{
    public static class Sortiraj
    {
        public static ICollection<Panel> PoPovrsini(ICollection<Panel> paneli, SmerSortiranja smer = SmerSortiranja.Rastuce)
        {
            if (smer == SmerSortiranja.Rastuce)
            {
                return paneli.OrderBy(panel => panel.Povrsina).ToList();
            }
            
            if (smer == SmerSortiranja.Opadajuce)
            {                
                return paneli.OrderByDescending(panel => panel.Povrsina).ToList();
            }

            return paneli;
        }
    }
}
