using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Passageiro
    {
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public ConexaoBanco banco;
        public Passageiro p;

        public Passageiro() { }

        public Passageiro(string nome, string cpf, DateTime dataNascimento, char sexo, DateTime ultimaCompra, DateTime dataCadastro, char situacao)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.UltimaCompra = ultimaCompra;
            this.DataCadastro = dataCadastro;
            this.Situacao = situacao;
        }

    
        public void CadastrarPassageiro(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DE PASSAGEIRO <<<");
            Console.Write("\nNome: ");
            this.Nome = Console.ReadLine();
            while (this.Nome.Length > 50)
            {
                Console.WriteLine("\nDigite um Nome de até 50 digitos!");
                Console.Write("Nome: ");
                this.Nome = Console.ReadLine();
            }
            //validação do tamanho e condição de cpf valido
            Console.Write("\nCPF: ");
            this.Cpf = Console.ReadLine();
            while (ValidarCpf(this.Cpf) == false || this.Cpf.Length < 11)
            {
                Console.WriteLine("\nCpf invalido, digite novamente");
                Console.Write("CPF: ");
                this.Cpf = Console.ReadLine();
            }

            Console.Write("\nData de Nascimento: ");
            this.DataNascimento = DateTime.Parse(Console.ReadLine());

            Console.Write("\nSexo (M/F/N): ");
            this.Sexo = char.Parse(Console.ReadLine().ToUpper());
            while ((this.Sexo.CompareTo('M') != 0) && (this.Sexo.CompareTo('F') != 0) && (this.Sexo.CompareTo('N') != 0))
            {
                Console.WriteLine("\nOpção invalida, digite novamente");
                Console.Write("Sexo (M/F/N): ");
                this.Sexo = char.Parse(Console.ReadLine().ToUpper());
            }
            this.UltimaCompra = DateTime.Now;
            this.DataCadastro = DateTime.Now;
            Console.Write("\nSituação (A-Ativo / I- Inativo): ");
            this.Situacao = char.Parse(Console.ReadLine().ToUpper());
            while ((this.Situacao.CompareTo('A') != 0) && (this.Situacao.CompareTo('I') != 0))
            {
                Console.WriteLine("\nOpção invalida, digite novamente");
                Console.Write("Situação(A - Ativo / I - Inativo): ");
                this.Situacao = char.Parse(Console.ReadLine().ToUpper());
            }
            string sql = $"INSERT INTO Passageiro(Cpf, Nome, Data_Nasc,Sexo,Data_Ultimacompra,Data_Cadastro,Situação) Values ('{this.Cpf}','{this.Nome}','{this.DataNascimento}','{this.Sexo}','{this.UltimaCompra}','{this.DataCadastro}','{this.Situacao}');";
            banco = new ConexaoBanco();
            banco.InserirBD(sql, conexaosql);
            Console.WriteLine("\n\nCadastro efetuado com sucesso!");
            Console.ReadLine();
        }
        public void DeletarPassageiro(SqlConnection conexaosql)
        {
            Console.WriteLine("\n*** Deletar Passageiro ***");
            Console.Write("\nDigite o CPF: ");
            this.Cpf = Console.ReadLine();
            while (ValidarCpf(this.Cpf) == false || this.Cpf.Length < 11)
            {
                Console.WriteLine("Cpf invalido, digite novamente");
                Console.Write("CPF: ");
                this.Cpf = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja Deletar o Cadastro do Passageiro? ");
            Console.WriteLine("1- Sim / 2-Não ");
            Console.Write("\nDigite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                string sql = $"Delete From PASSAGEIRO Where CPF=('{this.Cpf}');";
                banco = new ConexaoBanco();
                banco.InserirBD(sql, conexaosql);
                Console.WriteLine("Cadastro de Passageiro Deletado com sucesso!");

            }
            else
            {
                Console.WriteLine("Cadastro de Pasageiro Não foi Deletado...");

            }
        }
        public void LocalizarPassageiro(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine(">>> Localizar Passageiro <<<");
            Console.Write("\nDigite o cpf: ");
            this.Cpf = Console.ReadLine();

            while (ValidarCpf(this.Cpf) == false || this.Cpf.Length < 11)
            {
                Console.WriteLine("\nCpf invalido, digite novamente.");
                Console.Write("CPF: ");
                this.Cpf = Console.ReadLine();
            }

            Console.Clear();
            String sql = $"SELECT Cpf,Nome,Data_Nasc,Sexo,Data_Cadastro,Data_UltimaCompra,Situação FROM Passageiro WHERE CPF=('{this.Cpf}');";
            banco = new ConexaoBanco();
            banco.LocalizarPassageiro(sql, conexaosql);

            Console.ReadKey();


        }
        public void ConsultarPassageiro(SqlConnection conexaosql)
        {

            Console.WriteLine("\n>>> Lista de Passageiros cadastrados: <<<\n\n");

            Console.Clear();
            String sql = $"SELECT Cpf,Nome,Data_Nasc,Sexo,Data_Cadastro,Data_UltimaCompra,Situação FROM Passageiro";
            banco = new ConexaoBanco();
            banco.LocalizarPassageiro(sql, conexaosql);
            Console.WriteLine("\n\nAperte enter para continuar.");
            Console.ReadKey();
        }
        public void EditarPassageiro(SqlConnection conexaosql)
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine(">>> Editar Informações do Passageiro <<<");
            Console.Write("\nDigite o cpf: ");
            this.Cpf = Console.ReadLine();

            while (ValidarCpf(this.Cpf) == false || this.Cpf.Length < 11)
            {
                Console.WriteLine("\nCpf invalido, digite novamente");
                Console.Write("CPF: ");
                this.Cpf = Console.ReadLine();
            }

            sql = $"Select Cpf,Nome,Data_Nasc,Sexo,Data_Cadastro,Data_UltimaCompra,Situação From PASSAGEIRO Where CPF=('{this.Cpf}');";
            banco = new ConexaoBanco();

            if (!string.IsNullOrEmpty(banco.LocalizarPassageiro(sql, conexaosql)))
            {
                Console.Clear();
                Console.WriteLine("Digite a opção que deseja editar:\n\n1- Nome\n2- Data de Nascimento\n3- Sexo\n4- Data da última compra\n5- Data do Cadastro\n6- Situação");
                opc = int.Parse(Console.ReadLine());
                while (opc < 1 || opc > 6)
                {
                    Console.WriteLine("Digite uma opcao valida:");
                    Console.Write("\nDigite: ");
                    opc = int.Parse(Console.ReadLine());

                }
                switch (opc)
                {
                    case 1:
                        Console.Write("Digite o Nome: ");
                        this.Nome = Console.ReadLine();
                        sql = $"Update PASSAGEIRO Set Nome=('{this.Nome}') Where CPF=('{this.Cpf}');";
                        Console.WriteLine("\n\nAlteração realizada com sucesso!Aperte enter para continuar.");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Digite a data de Nascimento: ");
                        this.DataNascimento = DateTime.Parse(Console.ReadLine());
                        sql = $"Update PASSAGEIRO Set Data_nasc=('{this.DataNascimento}') Where CPF=('{this.Cpf}');";
                        Console.WriteLine("\n\nAlteração realizada com sucesso!Aperte enter para continuar.");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Digite o Sexo (M/F/N): ");
                        this.Sexo = char.Parse(Console.ReadLine());
                        sql = $"Update PASSAGEIRO Set Sexo=('{this.Sexo}') Where CPF=('{this.Cpf}');";
                        Console.WriteLine("\n\nAlteração realizada com sucesso!Aperte enter para continuar.");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Digite a data da Ultima Compra: ");
                        this.UltimaCompra = DateTime.Parse(Console.ReadLine());
                        sql = $"Update PASSAGEIRO Set Data_UltimaCompra=('{this.UltimaCompra}') Where CPF=('{this.Cpf}');";
                        Console.WriteLine("\n\nAlteração realizada com sucesso!Aperte enter para continuar.");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Write("Digite a Data de Cadastro: ");
                        this.DataCadastro = DateTime.Parse(Console.ReadLine());
                        sql = $"Update PASSAGEIRO Set Data_Cadastro=('{this.DataCadastro}') Where CPF=('{this.Cpf}');";
                        Console.WriteLine("\n\nAlteração realizada com sucesso!Aperte enter para continuar.");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.WriteLine("Digite a Situação: ");
                        this.Situacao = char.Parse(Console.ReadLine());
                        sql = $"Update PASSAGEIRO Set Situacao=('{this.Situacao}') Where CPF=('{this.Cpf}');";
                        Console.WriteLine("\n\nAlteração realizada com sucesso!Aperte enter para continuar.");
                        Console.ReadKey();
                        break;
                }
                banco = new ConexaoBanco();
                banco.EditarBD(sql, conexaosql);
            }
            else
            {
                Console.WriteLine("\nPassageiro Não Encontrado!Aperte enter para continuar.");
                Console.ReadKey();
                MenuPassageiro();
            }
        }
        public void MenuPassageiro()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("Escolha a opção desejada:\n\n1- Cadastrar\n2- Lista de Passageiros Cadastrados\n3- Editar\n4- Restritos\n0- Sair");
                op = int.Parse(Console.ReadLine());
                Passageiro p = new Passageiro();
                SqlConnection conexaosql = new SqlConnection();
                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        p.CadastrarPassageiro(conexaosql);
                        break;
                    case 2:
                        p.ConsultarPassageiro(conexaosql);
                        break;
                    case 3:
                        p.EditarPassageiro(conexaosql);
                        break;
                    case 4:
                        p.MenuRestritos();
                        break;
                    default:
                        break;
                }
            } while (op > 0 && op < 4);
        }
        private void MenuRestritos()
        {
            Console.Clear();
            Console.WriteLine(">>> Lista de CPF's Restritos <<<\n\n");
            Console.WriteLine("O que deseja fazer?\n\n1- Cadastrar CPF\n2- Buscar CPF\n3- Remover CPF");
            int op = int.Parse(Console.ReadLine());
            SqlConnection conexaosql = new SqlConnection();
            Passageiro p = new Passageiro();
            switch (op)
            {
                case 1:
                    p.CadastrarRestritos(conexaosql);
                    break;
                case 2:p.LocalizarRestritos(conexaosql);
                    break;
                case 3:p.DeletarRestritos(conexaosql);
                    break;
                default:
                    break;
            }
        }

        #region Restritos
        public bool LocalizarRestritos(SqlConnection conexaosql)
        {
            string retorno;

            do
            {
                Console.Clear();
                Console.WriteLine(">>> Verificar passageiro no cadastro de restritos: <<<\n\n");
                Console.Write("Digite o CPF: ");
                this.Cpf = Console.ReadLine();
            } while (ValidarCpf(this.Cpf) == false || this.Cpf.Length < 11);

            Console.Clear();
            string sql = $"SELECT Cpf FROM Cpf_Restrito WHERE CPF = '{this.Cpf}';";
            banco = new ConexaoBanco();
            banco.LocalizarRestritos(sql, conexaosql);
            retorno = banco.LocalizarRestritos(sql, conexaosql);
            if (retorno == "")
            {
                Console.WriteLine("Cpf sem restrições!\n\n");
                Console.WriteLine("Aperte enter para continuar.");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Passageiro com CPF restrito!\n\n");
                Console.WriteLine("Aperte enter para sair.");
                Console.ReadKey();
                p.MenuPassageiro();
                return false;
            }
        }
        public void CadastrarRestritos(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine(">>> Cadastro CPF restrito<<<\n\n");
            Console.WriteLine("Digite o CPF que deseja bloquear: ");
            this.Cpf = Console.ReadLine();
            if (ValidarCpf(this.Cpf))
            {
                string sql = $"INSERT INTO Cpf_Restrito(Cpf) VALUES ('{this.Cpf}');";
                banco = new ConexaoBanco();
                banco.InserirBD(sql, conexaosql);
                Console.WriteLine("\n\nCPF Cadastrado com sucesso!Aperte enter para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\n\nCpf inválido! Tente novamente.Aperte enter para continuar.");
                Console.ReadKey();
            }
        }
        public void DeletarRestritos(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine("\n>>> Deletar Cpf do cadastro de restritos: <<<\n\n");
            Console.Write("\nDigite o CPF que deseja excluir: ");
            this.Cpf = Console.ReadLine();

            if (ValidarCpf(this.Cpf))
            {
                string sql = $"Delete From Cpf_Restrito Where Cpf=('{this.Cpf}');";
                banco = new ConexaoBanco();
                banco.DeletarBD(sql, conexaosql);
                Console.WriteLine("\nCpf excluido com sucesso.\n\nAperte enter para continuar...");
                Console.ReadKey();
                p.MenuPassageiro();
            }
            else
            {
                Console.WriteLine("\nCPF inválido! Tente novamente.\n\nAperte enter para continuar.");
                Console.ReadKey();
                p.MenuPassageiro();
            }
        }
        #endregion
        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
