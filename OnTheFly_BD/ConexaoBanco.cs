using System;
using System.Collections.Generic;
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
        public String LocalizarBD(string sql, SqlConnection conexaosql)
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
                        Console.Write(" {0}", reader.GetString(1));
                        Console.Write(" {0}", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" {0}", reader.GetDateTime(3).ToShortDateString());
                        Console.Write(" {0}", reader.GetDateTime(4).ToShortDateString());
                        Console.Write(" {0}", reader.GetString(5));
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

    }

}

