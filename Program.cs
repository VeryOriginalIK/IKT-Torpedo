// See https://aka.ms/new-console-template for more information
using System.Text;
Console.OutputEncoding = Encoding.UTF8;

Mezo[] mezok = new Mezo[100];
int hajoDb = 5;
ClassLetrehozas();


void ClassLetrehozas()
{
    Generalas(mezok);

    Console.WriteLine("Mező koordináta teszt:");
    for (int i = 1; i < mezok.Length; i++)
    {
        Console.WriteLine(mezok[i].oszlop.ToString() + mezok[i].sor.ToString());
    }

    static void HajoLerak(Mezo mezo, bool hajo)
    {
        if (hajo)
            Console.WriteLine("Erre a mezőre már raktál hajót!");
        else
            mezo.hajo = true;
    }

    static void Tippeles(Mezo mezo, bool kilove)
    {
        if (kilove)
            Console.WriteLine("Erre a mezőre már tippeltél!");
        else
            mezo.kilove = true;
    }



    static void Generalas(Mezo[] mezok)
    {
        int sorszam = 0;
        int sor = 1;
        for (int i = 1; i < 11; i++)
        {
            sor = 1;
            while (sor != 11)
            {
                mezok[sorszam] = new Mezo(i, sor, false, false);
                sor++;
                sorszam++;
            }
        }
    }
}





string[,] JatekTer = new string[10, 10];
string[] BekertKordinata = new string[2];
AlapJatekTer();


do
{
    Console.WriteLine($"\t{hajoDb} Hajórészed van még hátra!");
    
    Console.Write("\tAdja meg a hajó darabjának kordinátáját (oszlop sor) : ");
    BekertKordinata = Console.ReadLine().Split(" ");
    
    int y = int.Parse(BekertKordinata[0]);
    int x = int.Parse(BekertKordinata[1]);


    Kiiaratas();

    hajoDb --;
    Console.WriteLine();
} while (hajoDb != 0);


void Kiiaratas()
{
    for (int i = 0; i < mezok.Length; i++)
    {
        if (!mezok[i].kilove)
            Console.Write("[]");
        else
        {
            if (mezok[i].hajo)
                Console.Write('O');
            else
                Console.Write('X');
        }
        try
        {
            if (mezok[i + 1].oszlop != mezok[i].oszlop)
                Console.WriteLine("\t");
        }
        catch { Console.WriteLine(); }
            
    }
}


string AlapJatekTer()
{
    for (int i = 0; i < JatekTer.GetLength(0); i++)
    {
        for (int j = 0; j < JatekTer.GetLength(1); j++)
        {
            JatekTer[i, j] = "0";
        }

    }
    return "";
}








class Mezo
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