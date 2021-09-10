namespace Biblioteka.Modeli
{
    public class Panel : Pravougaonik
    {
        public Panel(int sirina, int visina) : base(sirina, visina)
        {

        }

        public override string ToString()
        {
            return $"{Sirina} x {Visina}";
        }
    }
}
