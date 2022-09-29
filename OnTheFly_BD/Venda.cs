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
        public String Id_Venda { get; set; }
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
                        Data_venda = DateTime.Now;
                        p.LocalizarPassageiro(conexaosql);

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

    }
}

