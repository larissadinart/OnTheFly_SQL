using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class ConexaoBanco
    {
        string Conexao = "Data Source=localhost;Initial Catalog=OnTheFly;User Id=sa;Password=Lari123*;";
        SqlConnection conn;

        public ConexaoBanco()
        {
        }

        public string Caminho()
        {
            return Conexao;
        }
        public void InserirBD(string sql, SqlConnection conexaosql)
        {
            //ConexaoBanco conn = new ConexaoBanco();
            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();
                SqlCommand cmd = new SqlCommand(sql, conexaosql);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeletarBD(string sql, SqlConnection conexaosql)
        {
            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();
                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void EditarBD(string sql, SqlConnection conexaosql)
        {
            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();
                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();
                conexaosql.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public String LocalizarCia(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    Console.WriteLine(">>> Companhia Aérea Localizada <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" CPF: {0}", reader.GetString(0));
                        Console.Write(" Razão Social: {0}", reader.GetString(1));
                        Console.Write(" Data de abertura: {0}", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" Data de cadastro: {0}", reader.GetDateTime(3).ToShortDateString());
                        Console.Write(" Data do último vôo: {0}", reader.GetDateTime(4).ToShortDateString());
                        Console.Write(" Situação: {0}", reader.GetString(5));
                        Console.WriteLine("\n");
                    }
                }
                conexao.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

            return recebe;

        }
        public String LocalizarBloqueadas(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    Console.WriteLine(">>> Companhia Aérea Localizada <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" {0}", reader.GetString(0));
                        Console.WriteLine("\n");
                    }
                }
                conexao.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

            return recebe;

        }
        public String LocalizarPassageiro(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    Console.WriteLine(">>> Passageiro localizado: <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" CPF: {0}", reader.GetString(0));
                        Console.Write(" Nome: {0}", reader.GetString(1));
                        Console.Write(" Data de nascimento:{0}", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" Sexo: {0}", reader.GetString(3));
                        Console.Write(" Data de cadastro: {0}", reader.GetDateTime(4).ToShortDateString());
                        Console.Write(" Data da última compra:{0}", reader.GetDateTime(5).ToShortDateString());
                        Console.Write(" Situação: {0}", reader.GetString(6));
                        Console.WriteLine("\n");

                    }
                    Console.WriteLine("Aperte enter para continuar.");
                    Console.ReadKey();

                }
                conexao.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

            return recebe;

        }
        public String LocalizarRestritos(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();

                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" {0}", reader.GetString(0));
                        Console.WriteLine("\n");
                    }
                }
                conexao.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

            return recebe;

        }
        public String LocalizarVoo(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();

                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" ID: {0}", reader.GetString(0));
                        Console.Write(" Destino: {0}", reader.GetString(1));
                        Console.Write(" Data vôo:{0}", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" Data de cadastro: :{0}", reader.GetDateTime(3).ToShortDateString());
                        Console.Write(" Situação: {0}", reader.GetString(4));
                        Console.WriteLine("\n");
                    }
                }
                conexao.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

            return recebe;

        }
        public String LocalizarAeronave(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();

                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" ID: {0}", reader.GetString(0));
                        Console.Write(" Destino: {0}", reader.GetString(1));
                        Console.Write(" Data de Cadastro:{0}", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" Situação: {0}", reader.GetString(3));
                        Console.Write(" Data da ultima venda: :{0}", reader.GetDateTime(4).ToShortDateString());
                        Console.Write(" Capacidade: {0}", reader.GetInt32(5));
                    }
                }
                conexao.Close();
                Console.WriteLine("\n\nAperte enter para continuar.");
                Console.ReadKey();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return recebe;
        }
        public String LocalizarVenda(string sql, SqlConnection conexaosql)
        {
            String recebe = "";

            try
            {
                SqlConnection conexao = new SqlConnection(Caminho());
                conexao.Open();

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    Console.Clear();

                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write(" ID Venda: {0}", reader.GetString(0));
                        Console.Write(" Data da Venda:{0}", reader.GetDateTime(1).ToShortDateString());
                        Console.Write(" Valor Total da venda: {0}", reader.GetFloat(2));
                        Console.Write(" ID Passagem: {0}", reader.GetString(3));
                        Console.Write(" Cpf: {0}", reader.GetString(4));

                        //VendaPassagem(Id_Venda, Data_Venda, Valor_Total, ID_Passagem, Cpf)
                    }
                }
                conexao.Close();
                Console.WriteLine("\n\nAperte enter para continuar.");
                Console.ReadKey();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return recebe;
        }
        public int VerificarExiste(string sql)
        {
            SqlConnection conexao = new SqlConnection(Caminho());
            conexao.Open();
            int count = 0;
            SqlCommand sqlVerify = conexao.CreateCommand();
            sqlVerify.CommandText = sql;
            sqlVerify.Connection = conexao;
            using (SqlDataReader reader = sqlVerify.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }
            if (count != 0)
            {
                conexao.Close();
                return 1;
            }
            conexao.Close();
            return 0;
        }
        public static string LocalizarDado(string sql, SqlConnection conexao,string parametro)
        {
            var situacao = "";
            ConexaoBanco caminho = new();
            conexao = new(caminho.Caminho());
            conexao.Open();
            SqlCommand cmd = new(sql, conexao);
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        situacao = reader[$"{parametro}"].ToString();
                    }
                }
            }
            conexao.Close();
            return situacao;
        }

    }

}



