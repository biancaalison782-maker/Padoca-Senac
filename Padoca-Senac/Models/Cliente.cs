using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Padoca_Senac.banco;
using System.Diagnostics;

namespace Padoca_Senac.Models;

internal class Cliente
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _telefone;
    private string _endereco;
    private int _pontos;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Cpf { get => _cpf; set => _cpf = value; }
    public string Telefone { get => _telefone; set => _telefone = value; }
    public string Endereco { get => _endereco; set => _endereco = value; }
    public int Pontos { get => _pontos; set => _pontos = value; }

    public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)
    {
        if (nome == "") throw new Exception("O nome do cliente não pode ser vazio!");

        this.Nome = nome;
        this.Cpf = cpf;
        this.Telefone = telefone;
        this.Endereco = endereco;
        this.Pontos = pontos;
    }

    public Cliente(int id, string nome, string cpf, string telefone, string endereco, int pontos)
    {
        this.Id = id;
        this.Nome = nome;
        this.Cpf = cpf;
        this.Telefone = telefone;
        this.Endereco = endereco;
        this.Pontos = pontos;
    }

  

    public void AdicionarPontos(int pontos)
    {
        this.Pontos += pontos;
    }
    public static int Inserir(Cliente cliente)
    {

        // Instrução SQL: Define o comando de execução (Insert)
        // O uso de marcações com '@' (ex: @nome) é uma prática de segurança crucial chamda de "parametrização"
        // Ela impede ataques de SQL injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Cliente (`Nome`, `CPF`, `Telefone`, `Endereco`, `Pontos`) VALUES (@Nome, @CPF, @Telefone, @Endereco, @Pontos)";

        // Gerenciamento de recursos: Cria o objeto de comando passando a instrução SQL.


        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);


            return Banco.executarComando(cmd);

        }


    }
    public static int Atualizar(Cliente cliente)
    {
        string sql = "UPDATE `Cliente` SET `Nome` = @Nome, `CPF` = @CPF, `Telefone` = @Telefone, `Endereco` = @Endereco, `Pontos` = @Pontos WHere `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);
            cmd.Parameters.AddWithValue("@Id", cliente.Id);


            return Banco.executarComando(cmd);
        }
    }
    public static int Deletar(Cliente cliente)
    {
        string sql = "DELETE FROM `Cliente` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", cliente.Id);
            return Banco.executarComando(cmd);
        }
    }

    public static List<Cliente> ListarTodos()
    {
        string sql = "SELECT * FROM `Cliente`";
        List<Cliente> listaClientes = new();

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
                    string telefone = validarCampo(reader["Telefone"]);
                    string endereco = validarCampo(reader["Endereco"]);
                    int pontos = reader.GetInt32("Pontos");

                    Cliente cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);

                    listaClientes.Add(cliente);

                }
            }       


        }       


        return listaClientes;
    }
    public static Cliente? listarPorId(int id)
    // ? - Pois o cliente é nulo.
    {
        string sql = "SELECT * FROM `Cliente` WHERE `id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

            Cliente? cliente = null;

            while (reader.Read())
            {
                int idBanco = reader.GetInt32("Id");
                string nome = validarCampo(reader["Nome"]);
                string cpf = validarCampo(reader["Cpf"]);
                string telefone = validarCampo(reader["Telefone"]);
                string endereco = validarCampo(reader["Endereco"]);
                int pontos = reader.GetInt32("Pontos");

                cliente = new Cliente(idBanco, nome, cpf, telefone, endereco, pontos);

            }
            return cliente;

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
