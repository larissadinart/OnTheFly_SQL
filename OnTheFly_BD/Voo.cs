using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnTheFly_BD;

internal class Voo
{
    public static ConexaoBanco conexao = new ConexaoBanco();
    public String Id { get; set; }
    public string Destino { get; set; }
    public Aeronave Aeronave { get; set; }
    public DateTime DataVoo { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Situacao { get; set; }
    public Voo()
    {
    }
    public Voo(string destino, DateTime dataVoo, DateTime dataCadastro, string situacaoVoo)
    {
        int valorId = RandomCadastroVoo();
        this.Id = "V" + valorId.ToString();
        this.Destino = destino;
        this.Aeronave = null;
        this.DataVoo = dataVoo;
        this.DataCadastro = dataCadastro;
        this.Situacao = situacaoVoo;
    }
    public string ImprimirVoo()
    {
        return ">>> Dados do Vôo <<<\n\n" +
            "ID: " + Id +
            "\nDestino: " + Destino +
            "\nAeronave: " + Aeronave +
            "\nDataVôo: " + DataVoo +
            "\nDataCadastro: " + DataCadastro +
            "\nSituacao do Vôo: " + Situacao;
    }
    private static int RandomCadastroVoo()
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
    public void CadastrarVoo(SqlConnection conexaosql)
    {
        Aeronave aeronave = new Aeronave();
        string sql = "Select InscricaoANAC from Aeronave;";
        int verificar = conexao.VerificarExiste(sql);
        if (verificar != 0)
        {
            Console.Clear();
            Console.WriteLine(">>> Cadastro de Vôo <<<");
            int valorId = RandomCadastroVoo();
            this.Id = "V" + valorId.ToString("D4");
            Destino = DestinoVoo();
            Console.Write("Insira o nome da aeronave: ");
            aeronave.Inscricao = Console.ReadLine();
            sql = "select InscricaoANAC from Aeronave where InscricaoANAC = '" + aeronave.Inscricao + "';";
            verificar = conexao.VerificarExiste(sql);
            while (verificar == 0)
            {
                Console.WriteLine("Essa aeronave nao existe, informe outra: ");
                aeronave.Inscricao = Console.ReadLine();
                sql = "select InscricaoANAC from Aeronave where InscricaoANAC = '" + aeronave.Inscricao + "';";
                verificar = conexao.VerificarExiste(sql);
            }
            Console.Write("Informe a data e hora do vôo: (dd/MM/yyyy hh:mm) ");
            this.DataVoo = DateTime.Parse(Console.ReadLine());
            if (DataVoo <= DateTime.Now)
            {
                Console.WriteLine("Data é inválida, informe novamente: ");
                DataVoo = DateTime.Parse(Console.ReadLine());
            }
            this.DataCadastro = DateTime.Now;
            Console.WriteLine("Data de cadastro: " + DataCadastro);
            Console.WriteLine("Informe a situação do Vôo: \n[A] Ativo \n[C] Cancelado");
            Situacao = Console.ReadLine().ToUpper();
            while (Situacao != "A" && Situacao != "C")
            {
                Console.WriteLine("O valor informado é inválido, por favor informe novamente!\n[A] Ativo \n[C] Cancelado");
                Situacao = Console.ReadLine().ToUpper();
            }
            sql = "insert into Voo (Id, Destino, Data_Cadastro, Data_Voo, Situacao, Inscricao) values ('" + this.Id + "', '" + this.Destino + "','" +
            this.DataCadastro + "','" + this.DataVoo + "','" + this.Situacao + "', '" + aeronave.Inscricao + "', );";
            Console.WriteLine(sql);
            Console.ReadKey();
            conexao.InserirBD(sql,conexaosql);
            Console.WriteLine("Inscrição de Vôo realizada com sucesso!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Impossível cadastrar vôo, não existe nenhuma aeronave Ativa Cadastrada");
            Console.WriteLine("Pressione enter para continuar.");
            Console.ReadKey();
        }
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
        do
        {
            Console.Write("Informe a sigla do destino de voo: ");
            String destinoEscolhido = Console.ReadLine().ToUpper();
            if (destinoVoo.Contains(destinoEscolhido))
            {
                return destinoEscolhido;
            }
            else
            {
                Console.WriteLine("Destino inválido, informe novamente!");
                Console.WriteLine("");
            }
        } while (true);
    }
    public void EditarVoo(SqlConnection sqlConnection)
    {
        Console.WriteLine(">>> Atualizar dados Vôo <<<\n\n");
        Console.Write("Insira o id do Vôo que deseja alterar: ");
        string idVoo = Console.ReadLine();
        string sql = "Select Id from Voo where Id = '" + idVoo + "';";
        int verificar = conexao.VerificarExiste(sql);
        if (verificar != 0)
        {
            Console.WriteLine("Qual informação deseja alterar?:\n\n1- Destino \n2- Aeronave \n3- Data de Voo \n4- Situação do Voo");
            int op = int.Parse(Console.ReadLine());
            while (op < 1 || op > 5)
            {
                Console.WriteLine("Opcao inválida, tente novamente: ");
                op = int.Parse(Console.ReadLine());
            }
            switch (op)
            {
                case 1:
                    Console.WriteLine("Informe o destino: ");
                    string destino = DestinoVoo();
                    sql = "update Voo set Destino = '" + destino + "' where Id = '" + idVoo + "';";
                    conexao.EditarBD(sql,sqlConnection);
                    Console.WriteLine("Alteração efetuada com sucesso!");
                    break;
                case 2:
                    Console.WriteLine("informe a aeronave: "); //CLASSE AERONAVE
                    break;
                case 3:
                    Console.WriteLine("Informe a nova data?: ");
                    DateTime dataVoo = DateTime.Parse(Console.ReadLine());
                    if (dataVoo <= DateTime.Now)
                    {
                        Console.WriteLine("Data é inválida, informe novamente: ");
                        dataVoo = DateTime.Parse(Console.ReadLine());
                    }
                    sql = "update Voo set DataVoo = '" + dataVoo + "' where Id = '" + idVoo + "';";
                    conexao.EditarBD(sql,sqlConnection);
                    break;
                case 4:
                    Console.WriteLine("Informe a situação do vôo?: \nA- Ativo \nC- Cancelado ");
                    string situacao = Console.ReadLine().ToUpper();
                    while (situacao != "A" && situacao != "C")
                    {
                        Console.WriteLine("O valor informado é inválido, por favor informe novamente!");
                        situacao = Console.ReadLine().ToUpper();
                    }
                    sql = "update Voo set Situacao = '" + situacao + "' where Id = '" + idVoo + "';";
                    conexao.EditarBD(sql,sqlConnection);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("Valor de ID de voo não encontrado!!");
            Console.WriteLine("Pressione enter para continuar.");
            Console.ReadKey();
        }
    }
    public void LocalizarVoo(SqlConnection sqlConnection)
    {
        Console.WriteLine(">>> Localizar dados Vôo <<<");
        Console.Write("Insira o ID do Vôo que deseja alterar: ");
        string idVoo = Console.ReadLine();
        string sql = "Select Id from Voo where Id = '" + idVoo + "';";
        int verificar = conexao.VerificarExiste(sql);
        if (verificar != 0) 
        {
            sql = "Select Id, InscricaoAeronave, DataVoo, DataCadastro, Destino, Situacao from Voo where Id = '" + idVoo + "';";
            conexao.LocalizarVoo(sql,sqlConnection);
            Console.WriteLine("Pressione enter para continuar.");
            Console.ReadKey();
        }
        else 
        {
            Console.WriteLine("O vôo nao foi encontrado!");
            Console.WriteLine("Pressione enter para continuar.");
            Console.ReadKey();
        }
    }
    public void RegistroPorRegistro(SqlConnection conecta)
    {
        SqlConnection conexaosql = new SqlConnection();
        List<string> voo = new();
        conecta.Open();
        string sql = "Select Id,Destino,Data_Cadastro,Data_Voo,Situacao,Inscricao from Voo";
        SqlCommand cmd = new SqlCommand(sql, conecta);
        SqlDataReader reader = null;
        using (reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\n\t>>> Voo Localizado <<<\n");
            while (reader.Read())
            {
                if (reader.GetString(5) == "A")
                {
                    voo.Add(reader.GetString(0));
                }
            }
        }
        conecta.Close();
        for (int i = 0; i < voo.Count; i++)
        {
            string op;
            do
            {
                Console.Clear();
                Console.WriteLine(">>> Vôos <<<\nDigite para navegar:\n1- Próximo Cadasatro\n2- Cadastro Anterior" +
                    "\n3- Último cadastro\n4- Voltar ao Início\n0- Sair\n");
                Console.WriteLine($"Cadastro [{i + 1}] de [{voo.Count}]");

                string msg = "Select Id, InscricaoAeronave, DataVoo, DataCadastro, Destino, Situacao from Voo where Id = '" + voo[i] + "';";
                conexao.LocalizarVoo(msg, conexaosql);
                Console.Write("Opção: ");
                op = Console.ReadLine();
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "0")
                {
                    Console.WriteLine("Opção inválida!");
                    Thread.Sleep(2000);
                }

                else if (op.Contains("s"))
                    return;
                //Volta no Cadastro Anterior
                else if (op.Contains("2"))
                    if (i == 0)
                        i = 0;
                    else
                        i--;
                //Vai para o fim da lista
                else if (op.Contains("3"))
                    i = voo.Count - 1;
                //Volta para o inicio da lista
                else if (op.Contains("4"))
                    i = 0;
                //Vai para o próximo da lista
            } while (op != "1");
            if (i == voo.Count - 1)
                i--;
        }
    }
    public void MenuVoo()
    {
        Voo v = new Voo();
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
                    v.CadastrarVoo(conexaosql);
                    break;
                case 2:
                    v.LocalizarVoo(conexaosql);
                    break;
                case 3:
                    v.EditarVoo(conexaosql);
                    break;
                default:
                    break;
            }
        } while (op > 0 && op < 3);
    }
}
