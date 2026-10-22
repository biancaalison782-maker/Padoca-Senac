using Padoca_Senac.banco;
using Padoca_Senac.Models;
using Padoca_Senac.Services;
using System.ComponentModel.Design;

internal class Program
{
    static void Main(string[] args)

    {
    
        //ClienteServices.MenuCliente();

        Produto prod1 = new Produto("Pedra", 1, "Rochas", 1, 1);


        prod1.NomeMarca = "Minha tia";
        Console.WriteLine(prod1.NomeMarca);


        List<Produto> naoAguentoMais = Produto.ListarTodosCMarca();

        foreach (Produto produto in naoAguentoMais)
        {
            foreach (var propriedade in produto.GetType().GetProperties())
            {
                Console.WriteLine($"{propriedade.Name}: {propriedade.GetValue(produto)}");
            }

            Console.WriteLine("--------------------");
        }





    }
}
