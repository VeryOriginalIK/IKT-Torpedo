// See https://aka.ms/new-console-template for more information
using System.Text;
Console.OutputEncoding = Encoding.UTF8;


int hajoDb = 5;
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


