using MySql.Data.MySqlClient;
using ProjetoEcommerce.Models;
using System.Data;

namespace ProjetoEcommerce.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        // Cadastrar novo produto
        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO produto (Nome, Descricao, Preco, quantidade) VALUES (@nome, @descricao, @preco, @quantidade)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao ?? string.Empty;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                cmd.ExecuteNonQuery();
            }
        }

        // Atualizar produto existente
        public bool Atualizar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Produto SET Nome=@nome, Descricao=@descricao, Preco=@preco, quantidade=@quantidade WHERE IdPRod=@id", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = produto.IdPRod;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao ?? string.Empty;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false;
            }
        }

        // Listar todos os produtos
        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> lista = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lista.Add(new Produto
                    {
                        IdPRod = Convert.ToInt32(dr["IdPRod"]),
                        Nome = dr["Nome"].ToString(),
                        Descricao = dr["Descricao"].ToString(),
                        Preco = Convert.ToDecimal(dr["Preco"]),
                        quantidade = Convert.ToInt32(dr["quantidade"])
                    });
                }
            }
            return lista;
        }

        // Obter produto por ID
        public Produto ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto WHERE IdPRod=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                using var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Produto produto = new Produto();
                if (dr.Read())
                {
                    produto.IdPRod = Convert.ToInt32(dr["IdPRod"]);
                    produto.Nome = dr["Nome"].ToString();
                    produto.Descricao = dr["Descricao"].ToString();
                    produto.Preco = Convert.ToDecimal(dr["Preco"]);
                    produto.quantidade = Convert.ToInt32(dr["quantidade"]);
                }
                return produto;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Produto WHERE IdPRod=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}