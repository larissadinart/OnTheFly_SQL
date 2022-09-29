using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class CompanhiaAerea
    {
        public String Cnpj { get; set; }
        public String RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimoVoo { get; set; }
        public string Situacao { get; set; }

        public ConexaoBanco banco;

        public CompanhiaAerea(String cnpj, String razaoSocial, DateTime dataAbertura, DateTime ultimoVoo, DateTime dataCadastro, string situacao)
        {
            this.Cnpj = cnpj;
            this.RazaoSocial = razaoSocial;
            this.DataAbertura = dataAbertura;
            this.DataCadastro = dataCadastro;
            this.UltimoVoo = ultimoVoo;
            this.Situacao = situacao;
        }
        public CompanhiaAerea()
        {

        }

        #region cadastros normais
        public void CadastrarCia(SqlConnection conexaosql)
        {
            CompanhiaAerea cia = new CompanhiaAerea();
            this.UltimoVoo = DateTime.Now;
            this.DataCadastro = DateTime.Now;

            Console.Clear();
            Console.WriteLine(">>> Cadastro de Companhia Aérea <<<\n\n");
            Console.Write("Digite o CNPJ da Companhia Aérea: ");
            this.Cnpj = Console.ReadLine();
            if (ValidarCnpj(this.Cnpj))
            {
                Console.Write("Digite a data de abertura da Companhia: ");//try catch para data
                this.DataAbertura = DateTime.Parse(Console.ReadLine());
                System.TimeSpan tempoAbertura = DateTime.Now.Subtract(this.DataAbertura);

                if (tempoAbertura.TotalDays > 190)
                {
                    do
                    {
                        Console.Write("Digite a Razão Social (até 50 dígitos) : ");
                        this.RazaoSocial = Console.ReadLine();

                    } while (this.RazaoSocial.Length > 50);

                    string sql = $"INSERT INTO CiaAerea(Cnpj,RazaoSocial,Data_Abertura,Data_Cadastro,Data_UltimoVoo,Situacao) VALUES ('{this.Cnpj}', " +
                        $"'{this.RazaoSocial}','{this.DataAbertura}','{this.DataCadastro}','{this.UltimoVoo}', '{this.Situacao}');";
                    banco = new ConexaoBanco();
                    banco.InserirBD(sql, conexaosql);
                    Console.WriteLine("\nCompanhia Aérea Cadastrada com sucesso!\n\nAperte enter para continuar.");
                    Console.ReadKey();
                    cia.MenuCiaAerea();
                    
                }
                else
                {
                    Console.WriteLine("Impossível cadastrar! Tempo de abertura de empresa menor que 6 meses!\n\nTecle enter para continuar...");
                    Console.ReadKey();
                    cia.MenuCiaAerea();
                }
            }
            else
            {
                Console.WriteLine("CNPJ inválido! Tente novamente.Aperte enter para continuar.");
                Console.ReadKey();
                cia.CadastrarCia(conexaosql);
            }
        }
        public void LocalizarCia(SqlConnection conexaosql)
        {
            CompanhiaAerea cia = new CompanhiaAerea();
            do
            {
                Console.Clear();
                Console.WriteLine(">>> Localizar Companhia Aérea: <<<\n\n");
                Console.Write("Digite o CNPJ buscado: ");
                this.Cnpj = Console.ReadLine();
            } while (ValidarCnpj(this.Cnpj) == false || this.Cnpj.Length < 14);
            Console.Clear();
            string sql = $"SELECT Cnpj,RazaoSocial,Data_Abertura,Data_Cadastro,Data_UltimoVoo,Situacao FROM CiaAerea WHERE CNPJ = '{this.Cnpj}';";
            banco = new ConexaoBanco();
            banco.LocalizarCia(sql, conexaosql);

            Console.WriteLine("Aperte enter para contninuar.");
            Console.ReadKey();
            cia.MenuCiaAerea();


        } //melhorar impressão da cia 
        public void EditarCia(SqlConnection conexaosql)
        {
            int opc = 0;
            String sql;
            CompanhiaAerea cia = new CompanhiaAerea();

            Console.Clear();
            Console.WriteLine(">>> Editar informações da Companhia Aérea: <<<\n");
            Console.Write("Digite o CNPJ da Companhia buscada: ");
            this.Cnpj = Console.ReadLine();

            sql = $"SELECT Cnpj,RazaoSocial,Data_Abertura,Data_Cadastro,Data_UltimoVoo,Situacao FROM CiaAerea WHERE CNPJ = '{this.Cnpj}';";
            banco = new ConexaoBanco();

            if (!string.IsNullOrEmpty(banco.LocalizarCia(sql, conexaosql)))
            {
                Console.WriteLine("Digite a opção que deseja editar:\n\n1-Razão Social\n2-Data de Abertura\n3-Data de Cadastro\n4-Data do Último Vôo\n5-Situação");
                opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Alterar Razão Social:\n");
                        this.RazaoSocial = Console.ReadLine();
                        sql = $"UPDATE CiaAerea SET RazaoSocial = '{this.RazaoSocial}' WHERE CNPJ = '{this.Cnpj}';";
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Alterar Data de Abertura:\n");
                        this.DataAbertura = DateTime.Parse(Console.ReadLine());
                        sql = $"UPDATE CiaAerea SET Data_Abertura = '{this.DataAbertura}' WHERE CNPJ = '{this.Cnpj}';";
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Alterar Data de Cadastro:\n");
                        this.DataCadastro = DateTime.Parse(Console.ReadLine());
                        sql = $"UPDATE CiaAerea SET Data_Cadastro = '{this.DataCadastro}' WHERE CNPJ = '{this.Cnpj}';";
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Alterar Data do Último Vôo:\n");
                        this.UltimoVoo = DateTime.Parse(Console.ReadLine());
                        sql = $"UPDATE CiaAerea SET Data_UltimoVoo = '{this.UltimoVoo}' WHERE CNPJ = '{this.Cnpj}';";
                        break;
                    case 5:
                        Console.Clear();
                        Console.Write("Alterar Situação:\n");
                        this.Situacao = Console.ReadLine();
                        sql = $"UPDATE CiaAerea SET Situacao = '{this.Situacao}' WHERE CNPJ = '{this.Cnpj}';";
                        break;
                    default:
                        Console.WriteLine("Opção não encontrada!");
                        break;
                }
                banco = new ConexaoBanco();
                banco.EditarBD(sql, conexaosql);
                Console.WriteLine("Alteração realizada com sucesso!\n\nAperte enter para continuar.");
                Console.ReadKey();
                cia.MenuCiaAerea();

            }
            else
            {
                Console.WriteLine("CNPJ inválido!Aperte enter para tentar novamente.");
                Console.ReadKey();
                cia.MenuCiaAerea();
            }
        }
        public void DeletarCia(SqlConnection conexaosql)
        {
            Console.WriteLine("\n>>> Deletar Companhia Aérea <<<");
            Console.Write("\nDigite o CNPJ que deseja excluir: ");
            this.Cnpj = Console.ReadLine();
            string sql = $"Delete From CiaAerea Where Cnpj=('{this.Cnpj}');";
            banco = new ConexaoBanco();
            banco.DeletarBD(sql, conexaosql);
        }
        public void MenuCiaAerea()
        {
            int op;

            do
            {
                Console.Clear();
                Console.WriteLine("Escolha a opção desejada:\n\n1- Cadastrar\n2- Localizar\n3- Editar\n4- Bloqueados\n0- Sair");
                op = int.Parse(Console.ReadLine());
                CompanhiaAerea cia = new CompanhiaAerea();
                SqlConnection conexaosql = new SqlConnection();
                switch (op)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        cia.CadastrarCia(conexaosql);
                        break;
                    case 2:
                        cia.LocalizarCia(conexaosql);
                        break;
                    case 3:
                        cia.EditarCia(conexaosql);
                        break;
                    case 4:
                        MenuBloqueadas(conexaosql);
                        break;
                    default:
                        break;
                }
            } while (op > 0 && op < 4);
        }

        private void MenuBloqueadas(SqlConnection conexaosql)
        {
            int op;
            Console.Clear();
            Console.WriteLine(">>> Companhias Bloqueadas <<<\n\n");
            Console.WriteLine("Escolha a opção desejada: \n\n1- Consultar Companhia Bloqueadas\n2- Bloquear Companhias\n3- Desbloquear Companhias\n0- Sair ");
            op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 0:
                    MenuCiaAerea();
                    break;
                case 1:
                    LocalizarBloqueada(conexaosql);
                    break;
                case 2:
                    CadastrarBloqueadas(conexaosql);
                    break;
                case 3:DeletarBloqueadas(conexaosql);
                    break;

            }
        }
        #endregion

        #region Cadastros bloqueadas
        public void CadastrarBloqueadas(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine(">>> Cadastro de Companhia Aérea Bloqueada<<<\n\n");
            Console.WriteLine("Digite o CNPJ da Companhia Aérea que deseja bloquear: ");
            this.Cnpj = Console.ReadLine();
            if (ValidarCnpj(this.Cnpj))
            {
                string sql = $"INSERT INTO CiasBloqueadas(Cnpj) VALUES ('{this.Cnpj}');";
                banco = new ConexaoBanco();
                banco.InserirBD(sql, conexaosql);
                Console.WriteLine("CNPJ Cadastrado com sucesso!Aperte enter para continuar.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("CNPJ inválido! Tente novamente.Aperte enter para continuar.");
                Console.ReadKey();
            }
        }
        public void DeletarBloqueadas(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine("\n>>> Deletar Companhia Aérea <<<\n\n");
            Console.Write("\nDigite o CNPJ que deseja excluir: ");
            this.Cnpj = Console.ReadLine();

            if (ValidarCnpj(this.Cnpj))
            {
                string sql = $"Delete From Cnpj_Restrito Where Cnpj=('{this.Cnpj}');";
                banco = new ConexaoBanco();
                banco.DeletarBD(sql, conexaosql);
                Console.WriteLine("\n\nCNPJ excluido do cadastro de bloqueados!Aperte enter para continuar...");
                Console.ReadKey();
                MenuCiaAerea();
            }
            else
            {
                Console.WriteLine("\n\nCNPJ inválido! Tente novamente.Aperte enter para continuar.");
                Console.ReadKey();
                MenuCiaAerea();
            }
        }
        public void LocalizarBloqueada(SqlConnection conexaosql)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(">>> Localizar Companhia Aérea Bloqueada: <<<\n\n");
                Console.WriteLine("Digite o CNPJ buscado: ");
                this.Cnpj = Console.ReadLine();
            } while (ValidarCnpj(this.Cnpj) == false || this.Cnpj.Length < 14);
            Console.Clear();
            string sql = $"SELECT Cnpj FROM CiasBloqueadas WHERE CNPJ = '{this.Cnpj}';";
            banco = new ConexaoBanco();
            banco.LocalizarBloqueadas(sql, conexaosql);

            Console.WriteLine("Aperte enter para sair.");
            Console.ReadKey();
        }
        #endregion
        public bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;


            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else

                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}


