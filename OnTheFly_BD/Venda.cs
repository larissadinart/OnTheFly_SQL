using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheFly_BD;
using OnTheFLy_BD;

namespace OnTheFly_BD
{
    internal class Venda
    {
        public int Id_Venda { get; set; }
        public DateTime Data_venda { get; set; }
        public double Valor_Total { get; set; }
        public String Id_Passagem { get; set; }
        public string Cpf { get; set; }
        public ConexaoBanco banco = new ConexaoBanco();
        public Passageiro passageiro = new Passageiro();
        public PassagemVoo passagem { get; set; }
        public Voo voo = new Voo();
        public Venda() { }
        public void CadastrarVenda(SqlConnection conexaosql)
        {
            int assentosOcupados, capacidade;
            double valorPassagem;
            string sql = $"select cpf from Passageiro", parametro, Inscricao, idPassagem;

            Console.Clear();
            Console.WriteLine(">>> Menu Vendas <<<\n\n");
            int verificarCpf = banco.VerificarExiste(sql);
            if (verificarCpf != 0)
            {
                Console.Write("Informe seu CPF: ");
                this.Cpf = Console.ReadLine();

                while (Passageiro.ValidarCpf(this.Cpf) == false || this.Cpf.Length < 11)
                {
                    Console.Write("\nCpf inválido, digite novamente:");
                    this.Cpf = Console.ReadLine();
                }
                Console.Clear();
                Console.WriteLine("Gostaria de iniciar uma venda de passagens?\n\n1- Sim\n2- Não");
                int op = int.Parse(Console.ReadLine());
                while (op != 1 && op != 2)
                {
                    Console.WriteLine("Valor inválido, tente novamente: ");
                    op = int.Parse(Console.ReadLine());
                }
                switch (op)
                {
                    case 1:
                        this.Data_venda = DateTime.Now;
                        Console.WriteLine("Data da venda: " + this.Data_venda);
                            Console.WriteLine("\nQuantas passagens voce gostaria de comprar? (Maximo 4 por Venda)");
                            int contaPassagem = int.Parse(Console.ReadLine());
                        while (contaPassagem < 1 || contaPassagem > 4)
                        {
                            Console.WriteLine("Quantidade inválida, insira outro valor entre 1 e 4: ");
                            contaPassagem = int.Parse(Console.ReadLine());
                        }
                        sql = $"select Id_Voo, Situacao from Voo where Situacao = 'A'";
                        int verificarVoo = banco.VerificarExiste(sql);
                        if (verificarVoo != 0)
                        {
                            Console.WriteLine("Informe o Id do Vôo desejado (Ex = 'V1234', Caso nao saiba o id do Voo, vá ao menu 'Voo' e consulte o ID!): ");
                            string idVoo = Console.ReadLine();
                            sql = $"select Id_Voo from Voo where Id_Voo = '{idVoo}';";
                            verificarVoo = banco.VerificarExiste(sql);
                            if (verificarVoo != 0)
                            {
                                #region Coletando Dados do BD
                                sql = $"select AssentosOcupados from Voo where Id_Voo = '{idVoo}';";
                                parametro = "AssentosOcupados";
                                assentosOcupados = Convert.ToInt32(ConexaoBanco.LocalizarDado(sql, conexaosql,parametro));

                                sql = $"select Inscricao from Voo where Id_Voo  = '{idVoo}';";
                                parametro = "Inscricao";
                                Inscricao = ConexaoBanco.LocalizarDado(sql, conexaosql,parametro);

                                sql = $"select Capacidade from Aeronave where InscricaoANAC = '{Inscricao}';";
                                parametro = "Capacidade";
                                capacidade = Convert.ToInt32(ConexaoBanco.LocalizarDado(sql, conexaosql,parametro));

                                #endregion            
                                if ((assentosOcupados + contaPassagem) < (capacidade))
                                {
                                    for (int i = 0; i < contaPassagem; i++)
                                    {
                                        this.Id_Venda = RandomCadastroVenda();
                                        Console.Clear();
                                        Console.WriteLine($">>> Cadastro de passagem número {i + 1}:<<<\n");
                                        passagem = new PassagemVoo();
                                        banco = new ConexaoBanco();
                                        passagem.CadastrarPassagem(conexaosql, idVoo);
                                        assentosOcupados = +1;
                                        sql = $"select Valor_Unit from Passagem where Id_Voo = '{idVoo}';";
                                        parametro = "Valor_Unit";
                                        valorPassagem = Convert.ToDouble(ConexaoBanco.LocalizarDado(sql, conexaosql, parametro));
                                        this.Valor_Total = valorPassagem * contaPassagem;
                                        sql = $"select Id_Voo from Passagem where Id_Voo = '{idVoo}';";
                                        parametro = "Id_Voo";
                                        idPassagem = ConexaoBanco.LocalizarDado(sql, conexaosql, parametro);
                                        string sqll = $"insert into VendaPassagem(Id_Venda, Data_Venda, Valor_Total, ID_Passagem,Cpf) values ('{this.Id_Venda}', " +
                                        $"'{this.Data_venda}','{this.Valor_Total}','{idPassagem}','{this.Cpf}');";
                                        banco.InserirBD(sql, conexaosql);
                                        sqll = $"insert VendaPassagem(Data_Venda, Valor_Total, Cpf) values ('{DateTime.Now}','{this.Valor_Total}','{this.Cpf}');";
                                        banco.InserirBD(sql, conexaosql);
                                        //AssentosOcupados
                                        string update = $"Update Voo set AssentosOcupados = {assentosOcupados + 1} where Id_Voo = '{idVoo}'";
                                        banco.InserirBD(update, conexaosql);
                                        //Capacidade do Voo
                                        update = $"Update Aeronave set Capacidade = {capacidade - 1} where InscricaoANAC = '{Inscricao}'";
                                        banco.EditarBD(update, conexaosql);
                                        Console.WriteLine("\nCadastro de Venda efetuado com Sucesso!\n\nPressione uma tecla para prosseguir!");
                                        Console.ReadKey();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Assentos insuficientes, volte ao menu e informe uma quantidade menor!");
                                    Console.WriteLine("\n\nPressione enter para continuar.");
                                    Console.ReadKey();
                                    MenuVenda();
                                }
                            }
                            else
                            {
                                Console.WriteLine("O voo nao foi encontrado, tente novamente!");
                                Console.WriteLine("\n\nPressione enter para continuar.");
                                Console.ReadKey();
                                MenuVenda();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Não existem Voos Cadastrados, impossível realizar venda!!");
                            Console.WriteLine("\n\nPressione enter para continuar.");
                            Console.ReadKey();
                            MenuVenda();
                        }
                        break;
                    case 2:
                        MenuVenda();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Venda não pode ser finalizada, Não existem passageiros cadastrados! \nAperte enter para sair.");
                Console.ReadKey();
            }

        }
        private static int RandomCadastroVenda()
        {
            Random rand = new Random();
            int[] numero = new int[100];
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
            SqlConnection conn = new SqlConnection();
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine(">>> Menu venda de passagens <<<\n\n");
                Console.WriteLine("Escolha a opção desejada:\n\n1- Cadastrar\n2- Localizar\n3- Voltar\n0- Sair");
                op = int.Parse(Console.ReadLine());
                CompanhiaAerea cia = new CompanhiaAerea();
                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        CadastrarVenda(conn);
                        Program.Menu();
                        break;
                    case 2:
                        LocalizarVenda(conn);                        
                        break;
                    case 3:
                        Program.Menu();
                        break;
                    default:
                        break;
                }
            } while (op > 0 && op < 3);
        }
        public string DestinoVoo()
        {
            List<string> destinoVoo = new List<string>();
            destinoVoo.Add("BSB");
            destinoVoo.Add("CGH");
            destinoVoo.Add("GIG");
            Console.WriteLine("Destinos atualmente disponíves: ");
            Console.WriteLine("BSB - Aeroporto Internacional de Brasilia");
            Console.WriteLine("CGH - Aeroporto Internacional de Congonhas/SP");
            Console.WriteLine("GIG - Aeroporto Internacional do Rio de Janeiro");
            Console.WriteLine("");

            Console.Write("Informe a sigla do destino de voo: ");
            String destinoEscolhido = Console.ReadLine().ToUpper();
            return destinoEscolhido;
            //    if (destinoVoo.Contains(destinoEscolhido))
            //    {
            //    }
            //    else
            //    {
            //        Console.WriteLine("Destino inválido, informe novamente!");
            //        Console.WriteLine("");
            //    }
            //return destinoEscolhido;
        }
        public void LocalizarVenda(SqlConnection conexaosql)
        {
            ConexaoBanco banco = new ConexaoBanco();

            Console.Clear();
            Console.WriteLine(">>> Localizar Venda <<<\n\n");
            Console.Write("Digite o ID da venda que deseja buscar: ");
            this.Id_Venda = int.Parse(Console.ReadLine());
            String sql = $"Select Id_Venda,Data_Venda,Valor_Total,Id_Passagem,CPf From VendaPassagem Where Id_Venda='{this.Id_Venda}';";
            Console.WriteLine(sql);
            banco = new ConexaoBanco();
            Console.Clear();
            Console.WriteLine(sql);
            if (!string.IsNullOrEmpty(banco.LocalizarVenda(sql, conexaosql)))
            {
                banco.LocalizarVenda(sql, conexaosql);
                Console.WriteLine("Aperte enter para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Aeronave não encontrada!!!");
            }
            Program.Menu();
        }

    }
}