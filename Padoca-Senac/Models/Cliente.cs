using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Padoca_Senac.banco;

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

        // Gerenciamento de recursos:

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




}
