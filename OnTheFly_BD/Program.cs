using System;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class Program
    {
        static Passageiro p = new Passageiro();
        static CompanhiaAerea cia = new CompanhiaAerea();
        static Venda venda = new Venda();
        static Voo v = new Voo();
        static Aeronave an = new Aeronave();
        static ConexaoBanco conn = new ConexaoBanco();
        static SqlConnection conexaosql = new SqlConnection(conn.Caminho());
        
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
                Console.WriteLine("Escolha a opção desejada:\n\n1- Vender Passagem\n2- Passageiro\n3- Cia.Aérea\n4- Vôos\n5- Aeronaves\n0- Sair");
                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        venda.MenuVenda();
                        break;
                    case 2:
                        p.MenuPassageiro();
                        break;
                    case 3:
                        cia.MenuCiaAerea();
                        break;
                    case 4:
                        v.MenuVoo();
                        break;
                    case 5:
                        an.MenuAeronave();
                        break;
                    default:
                        break;
                }
            } while (op < 0 || op > 5);
        }
    }
}
