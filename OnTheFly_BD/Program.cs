using System;

namespace OnTheFly_BD
{
    internal class Program
    {
        //static Voo voo = new Voo();
        //static Passageiro p = new Passageiro();
        static CompanhiaAerea cia = new CompanhiaAerea();
        //static Venda venda = new Venda();
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>>> BEM VINDO AO AEROPORTO ON THE FLY! <<<<<\n\n");
                Console.WriteLine("Escolha a opção desejada:\n\n1- Vender Passagem\n2- Passageiro\n3- Cia.Aérea\n4- Destinos\n5- Vôos\n6- Aviões\n0- Sair");
                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        break;
                    case 2:
                        
                        break;
                    case 3:
                        cia.MenuCiaAerea();
                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    default:
                        break;
                }
            } while (op < 0 || op > 6);
        }
    }
}
