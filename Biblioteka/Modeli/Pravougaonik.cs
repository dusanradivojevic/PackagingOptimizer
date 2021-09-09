using System;

namespace Biblioteka.Modeli
{
    public class Pravougaonik
    {
        public Pravougaonik(int sirina, int visina)
        {
            Id = Guid.NewGuid();
            Sirina = sirina;
            Visina = visina;
        }

        public Guid Id { get; set; }
        public int Sirina { get; set; }
        public int Visina { get; set; }
        public int Povrsina => Sirina * Visina;
    }
}