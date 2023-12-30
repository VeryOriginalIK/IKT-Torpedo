// See https://aka.ms/new-console-template for more information
using System.Text;
Console.OutputEncoding = Encoding.UTF8;


int hajoDb = 5;
string[,] JatekTer = new string[10, 10];
string[] BekertKordinata = new string[2];
AlapJatekTer();
bool isGameOver = false;


do
{
    Console.WriteLine($"\t{hajoDb} Hajórészed van még hátra!");
    
    Console.Write("\tAdja meg a hajó darabjának kordinátáját (oszlop sor) : ");
    BekertKordinata = Console.ReadLine().Split(" ");
    
    int y = int.Parse(BekertKordinata[0]);
    int x = int.Parse(BekertKordinata[1]);

    
    JatekTerKiir(x, y);

    hajoDb --;
    Console.WriteLine();
} while (hajoDb != 0);



string JatekTerKiir(int x, int y)
{
    JatekTer[x-1, y-1] = "X";
    Console.WriteLine();
    for (int i = 0; i < JatekTer.GetLength(0); i++)
    {
        Console.Write("\t");
        for (int j = 0;j < JatekTer.GetLength(1); j++)
        {
            Console.Write(JatekTer[i,j]);
        }
        Console.WriteLine("\t");
    }
    return "";
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


void Tamadas()
{
    int lepes = 0;
    int talalt = 0;
    Console.Write("Hova szeretnél támadni (oszlop sor): ");
    BekertKordinata = Console.ReadLine().Split(" ");

    int y = int.Parse(BekertKordinata[0]);
    int x = int.Parse(BekertKordinata[1]);
    string[,] TamadasJatekTer = JatekTer;
    StreamWriter sw = new StreamWriter("player1tamadas.txt");
    /*JatekTer[x - 1, y - 1] = "X";*/
    if (TamadasJatekTer[x - 1, y - 1] == "X")
    {
        sw.WriteLine($"{lepes+ 1}. lépés: talált({y} {x})");
        talalt++;
        lepes++;
    }
    else
    {
        sw.WriteLine($"{lepes+ 1}. lépés: nem talált({y} {x})");
        lepes++;
    }
    sw.Close();
}

void Bot()
{
    Random random = new Random();
    int y = random.Next(0, 9);
    int x = random.Next(0, 9);
    StreamWriter sw = new StreamWriter("bottamadas.txt");
    /*JatekTer[x - 1, y - 1] = "X";*/
    sw.WriteLine($"{y} {x}");
    sw.Close();
    isGameOver = true;
}


while (!isGameOver)
{
    Tamadas();
    Bot();
}





