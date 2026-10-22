using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class Marca
{
    private int _id;
    private string _nome;
    private string _telefone;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Telefone { get => _telefone; set => _telefone = value; }

    public Marca(string nome, string telefone)

    {
        if (nome == "") throw new Exception("O nome da marca não pode ser vazio");
        this.Nome = nome;
        this.Telefone = telefone;
        
    }

    public Marca(int id, string nome, string telefone)
    {
        this.Id = id;
        this.Nome = nome;
        this.Telefone = telefone;
       
    }
  
    public static int Inserir(Marca marca)
    {

        // Instrução SQL: Define o comando de execução (Insert)
        // O uso de marcações com '@' (ex: @nome) é uma prática de segurança crucial chamda de "parametrização"
        // Ela impede ataques de SQL injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Marca (`Nome`, `Telefone`) VALUES (@Nome, @Telefone)";

        // Gerenciamento de recursos: Cria o objeto de comando passando a instrução SQL.


        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", marca.Nome);
            cmd.Parameters.AddWithValue("@Telefone", marca.Telefone);
            

            return Banco.executarComando(cmd);

        }


    }
    public static int Atualizar(Marca marca)
    {
        string sql = "UPDATE `Marca` SET `Nome` = @Nome, `Telefone` = @telefone WHere `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", marca.Nome);
            cmd.Parameters.AddWithValue("@Telefone", marca.Telefone);
            
           
            return Banco.executarComando(cmd);
        }
    }
    public static int Deletar(Marca marca)
    {
        string sql = "DELETE FROM `Marca` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", marca.Id);
            return Banco.executarComando(cmd);
        }
    }

    public static List<Marca> ListarTodos()
    {
        string sql = "SELECT * FROM `Marca`";
        List<Marca> listaMarca = new();

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
                    string telefone = validarCampo(reader["telefone"]);


                    Marca marca = new Marca(id, nome, telefone);

                }
            }

        }
        return listaMarca;
    }
    public static Marca? listarPorId(int id)
    // ? - Pois a Marca é nulo.
    {
        string sql = "SELECT * FROM `Marca` WHERE `id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

            Marca? marca = null;

            while (reader.Read())
            {
                int idBanco = reader.GetInt32("Id");
                string nome = validarCampo(reader["Nome"]);
                string telefone = validarCampo(reader["Telefone"]);
               


                marca = new Marca(idBanco, nome, telefone);

            }
            return marca;

        }



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

