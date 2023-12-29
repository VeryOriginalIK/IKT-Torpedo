using System.Text;
using System.Linq;
using Torpedo;


Console.OutputEncoding = System.Text.Encoding.UTF8;
Mezo[] mezok = new Mezo[100];

bool HajoLerak(Mezo mezo)
    {
    if (mezo.hajo) { 
        Console.WriteLine("Erre a mezőre már raktál hajót!");
        return false;
        }
    else { 
        mezo.hajo = true;
        return true;
    }
}

void Tippeles(Mezo mezo)
{
    if (mezo.kilove)
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

void Lerakas() {
    Dictionary<string,int> hajok = new Dictionary<string, int> { 
    { "Repülőgéphordozó", 5 }, { "Csatahajó", 4}, { "Cirkáló", 3 } , { "Tengeralattjáró", 3 } ,{ "Torpedóromboló", 2 }};
    foreach (var hajo in hajok.Keys)
    {
    // Lerakás ellenőrzés
    int hajohossz = hajok[hajo]; 
    List<int> oszlopok = new List<int> { };
    List<int> sorok = new List<int> { };
    Console.WriteLine($"Rakd le a {hajo}t!");
        do
        {
;       var hely = Beker();
        int oszlop = hely[0];
        int sor = hely[1];
        Mezo mezo = mezok.First(x => x.oszlop == oszlop && x.sor == sor);
        if(HajoLerak(mezo)) { 
            hajohossz--;
            oszlopok.Add(mezo.oszlop);
            sorok.Add(mezo.sor);
            Ellenorzes(oszlop, sor, oszlopok, sorok);
         }

        Kiiaratas(true);
        }
        while (hajohossz > 0);
    }

    bool Ellenorzes(int oszlop, int sor, List<int> oszlopok,List<int> sorok)
    {
        if (oszlopok.Max() - oszlopok.Min() == 0 && oszlopok[0] == oszlop)
            return true;
        if (sorok.Max() - sorok.Min() == 0 && sorok[0] == sor)
            return true;
        else{
            Console.WriteLine("A hajódnak egyesnek kell lennie!");
            oszlopok.RemoveAt(oszlopok.Count() -1 );
            sorok.RemoveAt(sorok.Count() - 1);
            mezok.First(x => x.oszlop == oszlop && x.sor == sor).hajo = false;
            Thread.Sleep(1500);
            return false;
        }
    }
}

int[] Beker()
{
    int hibas = 0;
    int[] koordinatak = new int[2];
    do
    {
        string[] bekert = new string[] { };
        do
        {
            if (hibas > 0)
                Console.WriteLine("Két külön számot adj meg 1 és 10 között. \nPélda: 5 5");
            Console.WriteLine("Add meg a koordinátákat (oszlop,sor): ");
            bekert = Console.ReadLine().Split(" ");
            hibas++;
        }
        while (bekert.Length != 2 || (!int.TryParse(bekert[0], out int o) || !int.TryParse(bekert[1], out int s)));

        koordinatak[0] = int.Parse(bekert[0]);
        koordinatak[1] = int.Parse(bekert[1]);
    }
    while (koordinatak[0] > 10 || koordinatak[0] < 1 || koordinatak[1] > 10 || koordinatak[1] < 1);
    return koordinatak;
}


void Kiiaratas(bool lerakas)
{
    Console.Clear();
    Console.Write("  ");
    for (int oszlop = 1; oszlop < 11; oszlop++)
    {
        Console.Write( $" {oszlop} ");
    }
    Console.Write("\n1 ");
    for (int i = 0; i < mezok.Length; i++)
    {
        try
        {
            if (mezok[i+1].oszlop != mezok[i].oszlop)
                Console.Write($"\n{(i / 10)+2} ");
        }
        catch { Console.WriteLine(); }
        if (!mezok[i].kilove && !lerakas)
            Console.Write("[]");
        else
        {
            if (mezok[i].hajo)
                Console.Write(" O ");
            else
                Console.Write(" X ");
        }
        
    }
}

Generalas(mezok);
Lerakas();