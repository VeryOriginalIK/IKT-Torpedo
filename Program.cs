using System.Text;
using System.Linq;
using Torpedo;


Console.OutputEncoding = System.Text.Encoding.UTF8;
Mezo[] mezok = new Mezo[100];
Mezo[] ellenfel = new Mezo[100];
Console.WriteLine("Szia! Hogy hívnak?");
string name = Console.ReadLine();
Console.WriteLine("Szia, " + name + "!");

bool HajoLerak(Mezo mezo)
    {
    if (mezo.hajo) { 
        Console.WriteLine("Erre a mezőre már raktál hajót!");
        Thread.Sleep(1000);
        return false;
        }
    else { 
        mezo.hajo = true;
        return true;
    }
}

bool Tippeles(Mezo mezo)
{
    if (mezo.kilove) { 
        Console.WriteLine("Erre a mezőre már tippeltél!");
        Thread.Sleep(1000);
        return false;
        }
    else { 
        mezo.kilove = true;
        return true;
    }
}

static void Generalas(Mezo[] mezok, Mezo[] ellenfel)
{
    int sorszam = 0;
    int sor = 1;
    for (int i = 1; i < 11; i++)
    {
        sor = 1;
        while (sor != 11)
        {
            mezok[sorszam] = new Mezo(i, sor, false, false);
            ellenfel[sorszam] = new Mezo(i, sor, false, false);
            sor++;
            sorszam++;
        }
    }
}

void Lerakas() {
    Dictionary<string,int> hajok = new Dictionary<string, int> { 
    { "Repülőgéphordozó", 5 }, { "Csatahajó", 4}, { "Cirkáló", 3 } , { "Tengeralattjáró", 3 } ,{ "Torpedóromboló", 2 }};
    foreach (var hajo in hajok.Keys)
    {
    // Lerakás ellenőrzés
    int hajohossz = hajok[hajo]; 
    List<int> oszlopok = new List<int> { };
    List<int> sorok = new List<int> { };
    Console.WriteLine($"\nRakd le a {hajo}t! ({hajohossz})");
        do
        {
;       var hely = Beker();
        int oszlop = hely[0];
        int sor = hely[1];
        Mezo mezo = mezok.First(x => x.oszlop == oszlop && x.sor == sor);
        if(HajoLerak(mezo) && Ellenorzes(oszlop, sor, oszlopok, sorok)) { 
           oszlopok.Add(mezo.oszlop);
           sorok.Add(mezo.sor);
           hajohossz--;
         }

        Kiiaratas(true);
        }
        while (hajohossz > 0);
        Console.WriteLine("\nKész!");
        Thread.Sleep(1000);
    }

    bool Ellenorzes(int oszlop, int sor, List<int> oszlopok,List<int> sorok)
    {
        bool helyes = false;
        if (oszlopok.Count() == 0) // ha még nem adott meg semmit
            return true;
        if (oszlopok.Max() - oszlopok.Min() == 0 && oszlopok[0] == oszlop) //Azonos-e valamelyik sorral
            helyes = true;
        if (sorok.Max() - sorok.Min() == 0 && sorok[0] == sor)             //,vagy oszloppal
            helyes = true;
        if ((helyes == true) && (sorok.Max() == sor - 1 || sorok.Min() == sor + 1 || oszlopok.Max() == oszlop - 1 || oszlopok.Min() == oszlop + 1))  //Ha már létező mellé rakta
            return helyes;
        else {
            Console.WriteLine("A darabokat sorban rakd le!");
        }
        if (helyes == false)
            Console.WriteLine("A hajódnak egyesnek kell lennie!");
        Thread.Sleep(1500);
        mezok.First(x => x.oszlop == oszlop && x.sor == sor).hajo = false;
        return false;
    }
}

int[] Beker()
{
    int hibas = 0;
    int[] koordinatak = new int[2];
    string[] bekert = new string[] { };
    while (true)
    {
        if (hibas > 0)
            Console.WriteLine("Két külön számot adj meg 1 és 10 között. \nPélda: 5 5");
        Console.WriteLine("\n\nAdd meg a koordinátákat (oszlop,sor): ");
        bekert = Console.ReadLine().Trim().Split(" ");
        hibas++;
        if (bekert.Length != 2 || (!int.TryParse(bekert[0], out int o) || !int.TryParse(bekert[1], out int s)))
            continue;
        koordinatak[0] = int.Parse(bekert[0]);
        koordinatak[1] = int.Parse(bekert[1]);
        if (koordinatak[0] <= 10 && koordinatak[0] >= 1 && koordinatak[1] <= 10 && koordinatak[1] >= 1)
            break;
    }
    return koordinatak;
}


void Kiiaratas(bool lerakas)
{

    Console.Clear();
    Console.Write("\t");
    for (int oszlop = 1; oszlop < 11; oszlop++) //Számok
    {
        Console.Write( $" {oszlop} ");
    }
    Console.Write("\n1\t");
    for (int i = 0; i < mezok.Length; i++)
    {
        try //Oszlop számok
        {
            if (mezok[i-1].sor % 10 == 0)
                Console.Write($"\n{(i / 10)+1}\t");
        }
        catch { 
            if (mezok[i].sor > 10) 
                Console.WriteLine("\t");
        }

        if(lerakas) {
            if (mezok[i].hajo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" O ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" X ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        else
        {
            if (ellenfel[i].kilove)
                Console.Write("[?]");

            else
            {
                if (ellenfel[i].hajo)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" O ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" X ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}

void Jatek()
{
    while (true)
    {

    }

    void Tamadas()
    {

    }

    void Vedekezes()
    {

    }



Network.Csatlakozas();
Generalas(mezok, ellenfel);
Lerakas();
