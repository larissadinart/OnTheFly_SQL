using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Venda
    {
        public int Id_Venda { get; set; }
        public DateTime Data_venda { get; set; }
        public float Valor_Total { get; set; }
        public String Id_Passagem { get; set; }
        public string Cpf { get; set; }
        ConexaoBanco b = new ConexaoBanco();
        Passageiro p = new Passageiro();
        public static ConexaoBanco banco;

        public Venda() { }

        //public void (SqlConnection conexaosql)
        //{
        //    //verificar bloqueados
        //    //verificar ID passagem
        //    //id voo
        //    //valor passagem
        //    Data_venda = DateTime.Now;
        //    p.ConsultarPassageiro(conexaosql);
        //    this.Cpf = p.Cpf;
        //}
        public void CadastrarVenda(SqlConnection conexaosql)
        {
            bool retorno;

            String sql = $"SELECT Cpf,Nome,Data_Nasc,Sexo,Data_Cadastro,Data_UltimaCompra,Situção FROM Cpf_Restrito;";
            retorno = p.LocalizarRestritos(conexaosql);

            if(retorno == true)
            {
                Console.Clear();
                Console.WriteLine("Gostaria de iniciar uma venda de passagens?\n\n1- Sim\n2- Não");
                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        this.Id_Venda = RandomCadastroVenda();
                        this.Data_venda = DateTime.Now;
                        p.LocalizarPassageiro(conexaosql);
                        string sqll = $"INSERT INTO VendaPassagem(ID_Venda, Data_Venda, Valor_Total,ID_Passagem, Cpf) VALUES ('{this.Id_Venda}', " +
                        $"'{this.Data_venda}','{this.Valor_Total}','{this.ID_Passagem}','{this.Cpf}';";
                        banco = new ConexaoBanco();
                        banco.InserirBD(sqll, conexaosql);
                        Console.WriteLine("\n>>> Dados da Venda <<<\n\nID");
                        Console.ReadKey();

                        break;
                    case 2:
                        CadastrarVenda(conexaosql);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Venda não pode ser finalizada!Aperte enter para sair.");
                Console.ReadKey();
            }
            


        }
        private static int RandomCadastroVenda()
        {
            Random rand = new Random();
            int[] numero = new int[99999];
            int aux = 0;
            for (int k = 0; k < numero.Length; k++)
            {
                int rnd = 0;
                do
                {
                    rnd = rand.Next(1000, 9999);
                } while (numero.Contains(rnd));
                numero[k] = rnd;
                aux = numero[k];
            }
            return aux;
        }
        public void MenuVenda()
        {
            int op;

            do
            {
                Console.Clear();
                Console.WriteLine("Escolha a opção desejada:\n\n1- Cadastrar\n2- Localizar\n3- Editar\n0- Sair");
                op = int.Parse(Console.ReadLine());
                CompanhiaAerea cia = new CompanhiaAerea();
                SqlConnection conexaosql = new SqlConnection();
                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        CadastrarVenda(conexaosql);
                        Program.Menu();
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    default:
                        break;
                }
            } while (op > 0 && op < 3);
        }
    }
}

