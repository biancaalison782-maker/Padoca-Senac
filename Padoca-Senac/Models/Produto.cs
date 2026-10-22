using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class Produto 
{
    private int _id;
    private string _nome;
    private decimal _preco;
    private string _categoria;
    private int _fabricado;
    private int _marcaId;
    private string _nomeMarca;  


    public Produto(string nome, decimal preco, string categoria, int fabricado, int marcaId)

    {
        if (nome == "") throw new Exception("O nome do produto não pode ser vazio");
        this.Nome = nome;
        this.Preco = preco;
        this.Categoria = categoria;
        this.Fabricado = fabricado;
        this.MarcaId = marcaId;

    }

    public Produto(int id, string nome, decimal preco, string categoria, int fabricado, int marcaid)
    {
        this.Id = id;
        this.Nome = nome;
        this.Preco = preco;
        this.Categoria = categoria;
        this.Fabricado = fabricado;
        this.MarcaId = marcaid;

    }

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public decimal Preco { get => _preco; set => _preco = value; }
    public string Categoria { get => _categoria; set => _categoria = value; }
    public int Fabricado { get => _fabricado; set => _fabricado = value; }
    public int MarcaId { get => _marcaId; set => _marcaId = value; }
    public string NomeMarca { get => _nomeMarca; }

    public static int Inserir(Produto produto)
    {

        // Instrução SQL: Define o comando de execução (Insert)
        // O uso de marcações com '@' (ex: @nome) é uma prática de segurança crucial chamda de "parametrização"
        // Ela impede ataques de SQL injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Produto (`Nome`, `Preco`, `Categoria`, `Fabricado`) VALUES (@Nome, `@Preco, @Categoria, @Fabricado)";

        // Gerenciamento de recursos: Cria o objeto de comando passando a instrução SQL.


        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@Preco", produto.Preco);
            cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
            cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
            


            return Banco.executarComando(cmd);

        }


    }
    public static int Atualizar(Produto produto)
    {
        string sql = "UPDATE `Cliente` SET `Nome` = @Nome, `Preco` = @Preco, `Categoria` = @Categoria, `Fabricado` = @Fabricado WHere `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@CPF", produto.Preco);
            cmd.Parameters.AddWithValue("@Telefone", produto.Categoria);
            cmd.Parameters.AddWithValue("@Endereco", produto.Fabricado);
            cmd.Parameters.AddWithValue("@Id", produto.Id);


            return Banco.executarComando(cmd);
        }
    }
    public static int Deletar(Produto produto)
    {
        string sql = "DELETE FROM `Produto` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", produto.Id);
            return Banco.executarComando(cmd);
        }
    }

    public static List<Produto> ListarTodos()
    {
        string sql = "SELECT * FROM `Produto`";
        List<Produto> listaProdutos = new();

        using (var cmd = new MySqlCommand(sql))
        {
            using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
            // Executa a leitura. 0 bloco 'using' aqui e FUNDAMENTAL para garantir que o
            // reader seja fechado no final, o que por sua vez fecha a conexão com o banco.
            {

                while (reader.Read())
                // O while vai rodar uma vez para cada linha que existir na tabela cliente
                {
                    // Extrai os dados da linha atual
                    int id = reader.GetInt32("Id");
                    string nome = validarCampo(reader["Nome"]);
                    decimal preco = reader.GetInt32("Preco");
                    string categoria = validarCampo(reader["Categoria"]);
                    int fabricado = reader.GetInt32("Fabricado");
                    int marcaId = reader.GetInt32("MarcaId");          


                    Produto produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);

                    listaProdutos.Add(produto);

                }
            }


        }


        return listaProdutos;
    }
    public static Produto? listarPorId(int id)
    // ? - Pois o cliente é nulo.
    {
        string sql = "SELECT * FROM `Produto` WHERE `id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

            Produto? produto = null;

            while (reader.Read())
            {
                int idBanco = reader.GetInt32("Id");
                string nome = validarCampo(reader["Nome"]);
                decimal preco = reader.GetInt32("Preco");
                string categoria = validarCampo(reader["Categoria"]);
                int fabricado = reader.GetInt32("Fabricado");
                int marcaId = reader.GetInt32("MarcaId");

                produto = new Produto(idBanco, nome, preco, categoria, fabricado, marcaId);

            }
            return produto;

        }



    }

    public static List<Produto> ListarTodosCMarca()
    {
        string sql = "SELECT p.Id, p.Nome, p.Preco, p.Categoria, p.Fabricado,m.Id as MarcaId, m.Nome AS marcaNome FROM `Produto` as `p` INNER JOIN `Marca` as `m` ON `p`.`MarcaId` = `m`.`Id`;";
        List<Produto> listaProdutos = new();

        using (var cmd = new MySqlCommand(sql))
        {
            using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
            // Executa a leitura. 0 bloco 'using' aqui e FUNDAMENTAL para garantir que o
            // reader seja fechado no final, o que por sua vez fecha a conexão com o banco.
            {

                while (reader.Read())
                // O while vai rodar uma vez para cada linha que existir na tabela cliente
                {
                    // Extrai os dados da linha atual
                    int id = reader.GetInt32("Id");
                    string nome = validarCampo(reader["Nome"]);
                    decimal preco = reader.GetInt32("Preco");
                    string categoria = validarCampo(reader["Categoria"]);
                    int fabricado = reader.GetInt32("Fabricado");
                    int marcaId = reader.GetInt32("MarcaId");
                    string marcaNome = validarCampo(reader["MarcaNome"]);


                    Produto produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);
                    produto._nomeMarca = marcaNome;

                    listaProdutos.Add(produto);

                }
            }

        }

        return listaProdutos;
    }



    private static string validarCampo(object campo)
    {
        if (campo == DBNull.Value || campo == null)
        {
            return "";
        }
        else
        {
            return campo.ToString();
        }
    }
    

}