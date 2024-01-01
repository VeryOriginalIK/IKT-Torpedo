using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Torpedo;

public class Game
{
    private Random random = new Random();
    private Mezo[] jatekos = new Mezo[100];
    private Mezo[] ellenfel = new Mezo[100];
    private List<int[]> botTalalatok = new List<int[]>();

    public void StartGame()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Szia! Hogy hívnak?");
        string name = Console.ReadLine();
        Console.WriteLine("Szia, " + name + "!");

        Generalas();
        Lerakas();
        Jatek();
    }

    private bool HajoLerak(bool bot, Mezo mezo)
    {
        if (mezo.hajo)
        {
            if (!bot)
            {
                Console.WriteLine("Erre a mezőre már raktál hajót!");
                Thread.Sleep(1500);
            }
            return false;
        }
        else
        {
            mezo.hajo = true;
            return true;
        }
    }

    private void Generalas()
    {
        int sorszam = 0;
        int sor = 1;
        for (int i = 1; i < 11; i++)
        {
            sor = 1;
            while (sor != 11)
            {
                jatekos[sorszam] = new Mezo(i, sor, false, false);
                ellenfel[sorszam] = new Mezo(i, sor, false, false);
                sor++;
                sorszam++;
            }
        }
    }

    private Mezo Mezokeres(Mezo[] tabla, int oszlop, int sor)
    {
        Mezo mezo = tabla.First(x => x.oszlop == oszlop && x.sor == sor);
        return mezo;
    }

    private void ellenfelLetrehoz(int hajohossz, List<int[]> hajokLerakva)
    {
        int[] tipp = new int[2];
        int oszlop = 0;
        int sor = 0;
        List<int> oszlopok = new List<int> { };
        List<int> sorok = new List<int> { };
        do
        {
            oszlop = tipp[0] = random.Next(1, 11);
            sor = tipp[1] = random.Next(1, 11);
            Mezo mezo = Mezokeres(ellenfel, oszlop, sor);
            if (HajoLerak(true, mezo) && Ellenorzes(true, oszlop, sor, oszlopok, sorok))
            {
                oszlopok.Add(mezo.oszlop);
                sorok.Add(mezo.sor);
                hajohossz--;
            }
        }
        while (hajohossz > 0);
    }

    private void Lerakas()
    {
        Dictionary<string, int> hajok = new Dictionary<string, int> {
            { "Repülőgéphordozó", 5 }, { "Csatahajó", 4}, { "Cirkáló", 3 } , { "Tengeralattjáró", 3 } ,{ "Torpedóromboló", 2 }};
        List<int[]> hajokLerakva = new List<int[]>();
        foreach (var hajo in hajok.Keys)
        {
            int hajohossz = hajok[hajo];
            ellenfelLetrehoz(hajohossz, hajokLerakva);
            List<int> oszlopok = new List<int> { };
            List<int> sorok = new List<int> { };
            Console.WriteLine($"\nRakd le a {hajo}t! ({hajohossz})");
            do
            {
                var hely = Beker();
                int oszlop = hely[0];
                int sor = hely[1];
                Mezo mezo = Mezokeres(jatekos, oszlop, sor);
                if (HajoLerak(false, mezo) && Ellenorzes(false, oszlop, sor, oszlopok, sorok))
                {
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
    }

    private bool Ellenorzes(bool bot, int oszlop, int sor, List<int> oszlopok, List<int> sorok)
    {
        bool helyes = false;
        if (oszlopok.Count() == 0)
            return true;
        if (oszlopok.Max() - oszlopok.Min() == 0 && oszlopok[0] == oszlop)
            helyes = true;
        if (sorok.Max() - sorok.Min() == 0 && sorok[0] == sor)
            helyes = true;
        if ((helyes == true) && (sorok.Max() == sor - 1 || sorok.Min() == sor + 1 || oszlopok.Max() == oszlop - 1 || oszlopok.Min() == oszlop + 1))
            return helyes;
        else
        {
            if (!bot)
            {
                Console.WriteLine("A darabokat sorban rakd le!");
                Thread.Sleep(1500);
                Mezokeres(jatekos, oszlop, sor).hajo = false;
            }
            else
                Mezokeres(ellenfel, oszlop, sor).hajo = false;
        }
        if (helyes == false && !bot)
        {
            Console.WriteLine("A hajódnak egyesnek kell lennie!");
            Thread.Sleep(1500);
        }

        return false;
    }

    private int[] Beker()
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

    private void Kiiaratas(bool lerakas)
    {
        Mezo[] tomb;
        if (lerakas)
            tomb = jatekos;
        else
            tomb = ellenfel;

        Console.Clear();
        Console.Write("\t");
        for (int oszlop = 1; oszlop < 11; oszlop++)
        {
            Console.Write($" {oszlop} ");
        }
        Console.Write("\n1\t");
        for (int i = 0; i < tomb.Length; i++)
        {
            try
            {
                if (jatekos[i - 1].sor % 10 == 0)
                    Console.Write($"\n{(i / 10) + 1}\t");
            }
            catch
            {
                if (jatekos[i].sor > 10)
                    Console.WriteLine("\t");
            }

            if (!lerakas && !tomb[i].kilove)
            {
                Console.Write("[?]");
                continue;
            }
            if (tomb[i].hajo)
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

    private int jatekosTalalat = 17;
    private int botTalalat = 17;

    private void Jatek()
    {

        Console.WriteLine("Most támadhatsz!");
        Thread.Sleep(1500);
        while (jatekosTalalat > 0 && botTalalat > 0)
        {
            Tamadas();
            Console.WriteLine("Most az ellenfél jön!");
            Thread.Sleep(1500);
            Bot();
            Console.WriteLine($"{botTalalat} hajód maradt\n Az ellenfélnek {jatekosTalalat} hajója maradt");
            Thread.Sleep(1500);
        }
        if (jatekosTalalat == 0)
        {
            Console.WriteLine("Gratulálunk, nyertél!");
            Thread.Sleep(2500);
        }
        else
        {
            Console.WriteLine("Az ellenfél nyert!");
            Thread.Sleep(2500);
        }

    }

    private void Tamadas()
    {
        while (true)
        {
            Kiiaratas(false);
            var tipp = Beker();
            Mezo meglottMezo = Mezokeres(ellenfel, tipp[0], tipp[1]);
            if (!meglottMezo.kilove)
            {
                if (meglottMezo.hajo)
                {
                    Console.WriteLine("Talált!");
                    jatekosTalalat--;
                    Thread.Sleep(800);
                    meglottMezo.kilove = true;
                    continue; //ha talált jöhet még egyszer
                }
                else
                {
                    Console.WriteLine("Nem talált!");
                    Thread.Sleep(800);
                    break;
                }
            }
            else
            {
                Console.WriteLine("Erre a mezőre már tippeltél!");
                Thread.Sleep(800);
            }
            meglottMezo.kilove = true;
        }
    }

    private void Bot()
    {
        int[] tipp = new int[2];
        while (true)
        {
            if (botTalalatok.Count == 0)
            {
                tipp[0] = random.Next(1, 11);
                tipp[1] = random.Next(1, 11);
            }
            else
            {
                int sorVagyOszlop = random.Next(0, 2);
                var tampont = botTalalatok[random.Next(0, botTalalatok.Count + 1)];
                tipp[sorVagyOszlop] = tampont[sorVagyOszlop] + random.Next(-1, 2);
                tipp[1 - sorVagyOszlop] = tampont[1 - sorVagyOszlop];
            }

            Mezo meglottMezo = Mezokeres(jatekos, tipp[0], tipp[1]);
            if (meglottMezo.kilove)
                continue;
            else
            {
                if (meglottMezo.hajo)
                {
                    Console.WriteLine("Robot talált!");
                    botTalalat--;
                    Thread.Sleep(1200);
                }
                else
                {
                    Console.WriteLine("Robot nem talált!");
                    Thread.Sleep(1200);
                    meglottMezo.kilove = true;
                    break;
                }
            }
            meglottMezo.kilove = true;
            Console.WriteLine(meglottMezo.oszlop + "  " + meglottMezo.sor);
        }
    }
}

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.StartGame();
    }
}
