using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnTheFly_BD;

internal class Aeronave
{
    public String Inscricao { get; set; }
    public String Capacidade { get; set; }
    public DateTime UltimaVenda = DateTime.Now;
    public DateTime DataCadastro = DateTime.Now;
    public string Situacao { get; set; }
    public ConexaoBanco banco;
    public String CNPJ { get; set; }
    public Aeronave() { }
    public Aeronave(String Inscrição, string Situacao, String Capacidade)
    {
        this.Inscricao = Inscrição;
        this.Capacidade = Capacidade;
        this.Situacao = Situacao;
    }
    public String GeraNumero()
    {
        Random rand = new Random();
        int[] numero = new int[100];
        int aux = 0;
        String convert = "";
        for (int k = 0; k < numero.Length; k++)
        {
            int rnd = 0;
            do
            {
                rnd = rand.Next(100, 999);
            } while (numero.Contains(rnd));
            numero[k] = rnd;
            aux = numero[k];
            convert = aux.ToString();
            break;
        }
        return convert;
    }
    public void CadastroAeronaves(SqlConnection conexaosql)
    {
        Console.WriteLine("\n>>> Cadastro de Aeronave <<<");
        this.CNPJ = Console.ReadLine();
        this.Inscricao = "PR-" + GeraNumero();
        int cap = 0;
        do
        {
            Console.Write("\nInforme a Capacidade da Aeronave: ");
            cap = int.Parse(Console.ReadLine());
            if (cap < 100 || cap > 999)
            {
                Console.Clear();
                Console.WriteLine("\nCapacidade Informada Inválida!" +
                                  "\nInforme Novamente!");
                Thread.Sleep(2000);
                Console.Clear();
            }
            Capacidade = cap.ToString();
        } while (cap < 100 || cap > 999);
        do
        {
            Console.Write("\nInfome a Situação da Aeronave:" +
                          "\nA-Ativo ou I-Inativo\n" +
                          "\nSituação: ");
            Situacao = Console.ReadLine().ToUpper();
        } while (!Situacao.Equals("A") && !Situacao.Equals("I"));
        UltimaVenda = DateTime.Now;
        DataCadastro = DateTime.Now;
        String sql = $"Insert Into Aeronave(InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade) " +
                     $"Values ('{this.Inscricao}','{this.CNPJ}','{this.DataCadastro}','{this.Situacao}','{this.UltimaVenda}','{this.Capacidade}');";
        banco = new ConexaoBanco();
        banco.InserirBD(sql, conexaosql);
        Console.WriteLine("\nCadastro de Aeronave Salvo com Sucesso!");
    }
    public void LocalizarAeronave(SqlConnection conexaosql)
    {
        Console.Clear();
        Console.WriteLine("\n>>> Localizar Aeronaves <<<");
        Console.Write("\n Digite o ID da aeronave: ");
        this.Inscricao = Console.ReadLine();
        while (this.Inscricao.Length < 6)
        {
            Console.WriteLine("\nID Inválido, tente novamente:");
            this.Inscricao = Console.ReadLine();
        }
        ////Ver se existe Companhia Aeronave cadastrada, perguntar pro usuario o CNPJ manda pro banco via select e  localizar e se tiver deletar
        //this.CNPJ = "15086511000145";
        String sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where InscricaoANAC=('{this.Inscricao}') and CNPJ=('{this.CNPJ}');";
        banco = new ConexaoBanco();
        Console.Clear();
        if (!string.IsNullOrEmpty(banco.LocalizarAeronave(sql, conexaosql)))
        {
            Console.WriteLine("\n\tAperte Qualquer enter para encerrar.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("\n\tAeronove não Encontrada!!!");
        }

    }
    public void DeletarAeronave(SqlConnection conexaosql)
    {
        Console.WriteLine("\n>>> Deletar Aeronave <<<");
        Console.Write("\nDigite o ID da aeronave: ");
        this.Inscricao = Console.ReadLine().ToUpper();
        while (this.Inscricao.Length < 6)
        {
            Console.WriteLine("\nID Inválido, tente novamente: ");
            Console.Write("ID_ANAC: ");
            this.Inscricao = Console.ReadLine().ToUpper();
        }
        Console.Clear();
        // String sql = $"Select ID_ANAC,DATA_CADASTRO,SITUACAO,ULTIMA_VENDA,CAPACIDADE From AERONAVE Where ID_ANAC=('{this.Inscricao}') and CNPJ=('{this.CNPJ}');";
        String sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where InscricaoANAC=('{this.Inscricao}');";
        banco = new ConexaoBanco();
        if (!string.IsNullOrEmpty(banco.LocalizarAeronave(sql, conexaosql)))
        {
            Console.WriteLine("Deseja excluir cadastro de Aeronave? ");
            Console.Write("1- Sim / 2- Não ");
            int op = int.Parse(Console.ReadLine());
            if (op == 1)
            {
                //Ver se existe Companhia Aeronave cadastrada, perguntar pro usuario o CNPJ manda pro banco via select e  localizar e se tiver deletar
                // USei Para testa
                this.CNPJ = "15086511000145";
                sql = $"Delete From Aeronave Where InscricaoANAC=('{this.Inscricao}') and CNPJ=('{this.CNPJ}');";
                banco = new ConexaoBanco();
                banco.DeletarBD(sql, conexaosql);
                Console.WriteLine("\nCadastro de Aeronave excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("\nOpção excluir Aeronave não Foi selecionada.");
            }
        }
        else
        {
            Console.WriteLine("\nAeronave não Encontrada!!!");
        }
    }
    public void EditarAeronave(SqlConnection conexaosql)
    {
        ConexaoBanco banco = new ConexaoBanco();
        int opc = 0;
        Console.WriteLine(">>> Editar Aeronave <<<\n\n");
        Console.Write("\nDigite o ID da aeronave: ");
        this.Inscricao = Console.ReadLine().ToUpper();
        while (this.Inscricao.Length < 6)
        {
            Console.WriteLine("\nID Inválido, tente Novamente: ");
            Console.Write("ID_ANAC: ");
            this.Inscricao = Console.ReadLine().ToUpper();
        }
        //pedir o cnpj 
        //this.CNPJ = "15086511000145";
        Console.Clear();
        String sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where InscricaoANAC=('{this.Inscricao}');";
        banco = new ConexaoBanco();
        //switch
        if (!string.IsNullOrEmpty(banco.LocalizarAeronave(sql, conexaosql)))
        {
            Console.WriteLine("Digite a Opção que Deseja Editar\n");
            Console.WriteLine("1- Data do Cadastro");
            Console.WriteLine("2- Data da última venda");
            Console.WriteLine("3- Situação");
            Console.WriteLine("4- Capacidade");
            opc = int.Parse(Console.ReadLine());
            while (opc < 1 || opc > 4)
            {
                Console.WriteLine("\nDigite uma Opcão válida:");
                opc = int.Parse(Console.ReadLine());
            }
            switch (opc)
            {
                case 1:
                    Console.Write("\nAlterar a Data de Cadastro: ");
                    this.DataCadastro = DateTime.Parse(Console.ReadLine());
                    sql = $"Update Aeronave Set DataCadastro=('{this.DataCadastro}') Where InscricaoANAC=('{this.Inscricao}');";
                    Console.WriteLine("\nData de Cadastro alterada Com Sucesso... ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 2:
                    Console.Write("\nAlterar a Data da Ultima Venda: ");
                    this.UltimaVenda = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("\nData da Ultima Venda alterada Com Sucesso... ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    sql = $"Update Aeronave Set UltimaVenda=('{this.UltimaVenda}') Where InscricaoANAC=('{this.Inscricao}');";
                    break;
                case 3:
                    Console.Write("\nAlterar Situação: ");
                    Console.WriteLine("\nA-Ativo ou I-Inativo");
                    Console.Write("Situacao: ");
                    this.Situacao = Console.ReadLine();
                    sql = $"Update Aeronave Set Situacao=('{this.Situacao}') Where InscricaoANAC=('{this.Inscricao}');";
                    Console.WriteLine("\nSituação Editada Com Sucesso. ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 4:
                    Console.Write("\nAlterar a Capacidade: ");
                    this.Capacidade = Console.ReadLine();
                    sql = $"Update Aeronave Set Capacidade=('{this.Capacidade}') Where InscricaoANAC=('{this.Inscricao}');";
                    Console.WriteLine("\nCapacidade Editada Com Sucesso. ");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
            }
            banco = new ConexaoBanco();
            banco.EditarBD(sql, conexaosql);
        }
        Console.WriteLine("\n\tAeronove Não Encontrada!!!");

    }
    public void ConsultarAeronave(SqlConnection conexaosql)
    {
        ConexaoBanco banco = new ConexaoBanco();
        int op = 0;
        String sql = "";
        Console.Write("\nDeseja consultar situação de aeronave" +
                         "\n1-Ativo , 2-Inativo , 3-Geral\n" +
                         "\nConsulta: ");
        op = int.Parse(Console.ReadLine());
        switch (op)
        {
            case 1:
                Console.Clear();
                Console.Write("\n*** Aeronaves Ativas ***\n");
                this.Situacao = "A";
                sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where Situacao = '{this.Situacao}' ;";
                banco = new ConexaoBanco();
                banco.LocalizarAeronave(sql, conexaosql);
                break;
            case 2:
                Console.Clear();
                Console.Write("\n*** Aeronaves Ativas ***\n");
                this.Situacao = "I";
                sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave Where Situacao = '{this.Situacao}');";
                banco = new ConexaoBanco();
                banco.LocalizarAeronave(sql, conexaosql);
                break;
            case 3:
                Console.Clear();
                Console.Write("\n*** Aeronaves Cadastradas ***\n");
                sql = $"Select InscricaoANAC,CNPJ,DataCadastro,Situacao,UltimaVenda,Capacidade From Aeronave;";
                banco = new ConexaoBanco();
                banco.LocalizarAeronave(sql, conexaosql);
                break;
        }
    }
}
