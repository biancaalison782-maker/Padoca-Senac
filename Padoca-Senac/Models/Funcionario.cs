
using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class Funcionario
{

    private int _id;
    private string _nome;
    private string _cpf;
    private string _cargo;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Cpf { get => _cpf; set => _cpf = value; }
    public string Cargo { get => _cargo; set => _cargo = value; }


    public Funcionario(string nome, string cpf, string cargo)

    {
        if (nome == "") throw new Exception("O nome do Funcionario não pode ser vazio");
        this.Nome = nome;
        this.Cpf = cpf;
        this.Cargo = cargo;
    }

    public Funcionario(int id, string nome, string cpf, string cargo)
    {
        this.Id = id;
        this.Nome = nome;
        this.Cpf = cpf;
        this.Cargo = cargo;
    }
    public void AlterarCargo(string novoCargo)
    {
        this.Cargo = novoCargo;
    }
    public static int Inserir(Funcionario funcionario)
    {

        // Instrução SQL: Define o comando de execução (Insert)
        // O uso de marcações com '@' (ex: @nome) é uma prática de segurança crucial chamda de "parametrização"
        // Ela impede ataques de SQL injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Funcionario (`Nome`, `CPF`, `Cargo`) VALUES (@Nome, @CPF, @Cargo)";

        // Gerenciamento de recursos: Cria o objeto de comando passando a instrução SQL.


        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

            return Banco.executarComando(cmd);

        }


    }
    public static int Atualizar(Funcionario funcionario)
    {
        string sql = "UPDATE `Cliente` SET `Nome` = @Nome, `CPF` = @CPF, `Cargo` = @Cargo WHere `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
            cmd.Parameters.AddWithValue("@Id", funcionario.Id);


            return Banco.executarComando(cmd);
        }
    }
    public static int Deletar(Funcionario funcionario)
    {
        string sql = "DELETE FROM `Funcionario` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", funcionario.Id);
            return Banco.executarComando(cmd);
        }
    }

    public static List<Cliente> ListarTodos()
    {
        string sql = "SELECT * FROM `Funcionario`";
        List<Cliente> listaFuncionario = new();

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
                    string cpf = validarCampo(reader["Cpf"]);
                    string cargo = validarCampo(reader["Cargo"]);


                    Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);

                }
            }

        }
        return listaFuncionario;
    }
    public static Funcionario? listarPorId(int id)
    // ? - Pois o Funcinario é nulo.
    {
        string sql = "SELECT * FROM `Funcionario` WHERE `id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

            Funcionario? funcionario = null;

            while (reader.Read())
            {
                int idBanco = reader.GetInt32("Id");
                string nome = validarCampo(reader["Nome"]);
                string cpf = validarCampo(reader["Cpf"]);
                string cargo = validarCampo(reader["Cargo"]);


                funcionario = new Funcionario(idBanco, nome, cpf, cargo);

            }
            return funcionario;

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
