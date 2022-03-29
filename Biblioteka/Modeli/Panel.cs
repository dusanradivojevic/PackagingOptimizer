namespace Biblioteka.Modeli
{
    public class Panel : Pravougaonik
    {
        public Panel(int sirina, int visina, int masa) : base(sirina, visina)
        {
            Masa = masa;
        }

        public int Masa { get; private set; }

        public override string ToString()
        {
            return $"{Sirina} x {Visina}";
        }
    }
}
