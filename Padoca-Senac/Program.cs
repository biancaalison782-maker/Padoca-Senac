using Padoca_Senac.banco;
using Padoca_Senac.Models;

internal class Program
{
    static void Main(string[] args)
    {
        Cliente cliente1 = new Cliente(
            nome: "bianca",
            cpf: "48207698754",
            telefone:"11999999999",
            endereco: "Rua das Ruas",
            pontos: 10
            );

        Cliente.Inserir(cliente1);
    }
}
