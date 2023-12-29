namespace Torpedo
{
    internal class Mezo
    {
            public int sor;
            public int oszlop;
            public bool hajo; //Van rajta hajó?
            public bool kilove; //Tippelték már?

            public Mezo(int oszlop, int sor, bool hajo, bool kilove)
            {
                this.sor = sor;
                this.oszlop = oszlop;
                this.hajo = hajo;
                this.kilove = kilove;
            }
    }
}
