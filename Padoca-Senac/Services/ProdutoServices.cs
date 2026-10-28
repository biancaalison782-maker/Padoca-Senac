using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Services
{
    internal class ProdutoServices
    {
        public static void MenuProduto()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine(" ----- GESTÃO DE PRODUTOS ----- ");
                Console.WriteLine("1 - Cadastrar Produto");
                Console.WriteLine("2 - Listar todos os Produtos");
                Console.WriteLine("3 - Buscar Produto por Id");
                Console.WriteLine("4 - Atualizar Produto");
                Console.WriteLine("5 - Excluir Produto");
                Console.WriteLine("0 - sair do programa");

                Console.WriteLine("---------------");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();

                Console.Clear();


                try
                {
                    switch (opcao)
                    {
                        case "1": // Cadastrar

                            cadastrarProdutoMenu();

                            break;

                        case "2": // Listar
                            listarProdutosMenu();

                            break;

                        case "3": //listar por id
                            ListarIdMenu();
                            break;

                        case "4":
                            //listarClientesMenu

                            break;

                        case "5":



                            break;

                        case "0":



                            break;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }


            }



        }

        private static void cadastrarProdutoMenu()
        {



            Console.WriteLine("Digite o nome do Produto: ");
            string nomeProduto = Console.ReadLine();

            Console.WriteLine("Digite o endereço do cliente: ");
            string precoProduto = Console.ReadLine();

            Console.WriteLine("Digite o cpf do cliente: ");
            string categoriaProduto = Console.ReadLine();

            Console.WriteLine("Digite o Id do produto: ");
            int idprodutofabricado = int.Parse(Console.ReadLine());





        }

        private static void listarProdutosMenu()
        {
            Console.WriteLine("------ LISTA DE CLIENTES ----- ");

            List<Cliente> lista = Cliente.ListarTodos();

            foreach (var cliente in lista)
            {
                Console.WriteLine($"ID: {cliente.Id} | Nome {cliente.Nome} | CPF: {cliente.Cpf}| Pontos: {cliente.Pontos} ");
            }

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado no sistema");

            }
            else
            {
                foreach (var cliente in lista)
                {
                    Console.WriteLine($"ID: {cliente.Id} | Nome {cliente.Nome} | CPF: {cliente.Cpf}| Pontos: {cliente.Pontos} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ListarIdMenu()


        {

            Console.WriteLine("------ LISTAR POR ID------");
            Console.WriteLine("digite o id:    ");

            int idConsole;

            int.TryParse(Console.ReadLine(), out idConsole);

            Cliente? clienteBusca = Cliente.listarPorId(idConsole);

            if (clienteBusca == null)
            {

                Console.WriteLine("Cliente não encontrado");

            }
            else
            {

                Console.WriteLine($"ID: {clienteBusca.Id} " +
                    $"\nNome {clienteBusca.Nome} " +
                    $"\nCPF: {clienteBusca.Cpf} " +
                    $"\nTelefone: {clienteBusca.Telefone} " +
                    $"\nEndereco: {clienteBusca.Endereco}" +
                    $"\nPontos: {clienteBusca.Pontos} ");



            }

            Console.ReadKey();
            Console.Clear();

        }



        private static void listarAtualizarProduto()
        {
            Console.WriteLine("------ATUALIZAR CLIENTE ------ ");

            Console.WriteLine("Digite o id do cliente que você deseja atualizar");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);


            Cliente? clientesParaAtualizar = Cliente.listarPorId(id);

            if (clientesParaAtualizar != null)
            {
                Console.WriteLine($"Atualizando dados de {clientesParaAtualizar.Nome}");
                Console.WriteLine("Pressione ENTER sem digitar nada para manter o dado atual");
                Console.WriteLine($" Endereço Atual: ({clientesParaAtualizar.Endereco})");
                Console.WriteLine($"Digite o Endereço Novo:    ");

                string novoEndereco = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoEndereco)) clientesParaAtualizar.Endereco = novoEndereco;
                Console.WriteLine($"Novo Endereço Atual: ({clientesParaAtualizar.Endereco})");


                Console.WriteLine("---------------------------------------------");



                Console.WriteLine($"Atualizando numero: {clientesParaAtualizar.Telefone}");
                Console.WriteLine($"Pressione Enter sem digitar nada para mandar o dado atual\n");
                Console.WriteLine($"Telefone Atual: ({clientesParaAtualizar.Telefone})\n");
                Console.WriteLine($"digite o novo numero: ");

                string? novoTelefone = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoTelefone)) clientesParaAtualizar.Telefone = novoTelefone;
                Console.WriteLine($"novo numero atual: ({clientesParaAtualizar.Telefone})");



                int resultadoAtualizacao = Cliente.Atualizar(clientesParaAtualizar);

                if (resultadoAtualizacao > 0) Console.WriteLine("Cliente atualizado com sucesso");

            }
            else
            {

                Console.WriteLine("Cliente não encontrado");

            }


            Console.ReadKey();
            Console.Clear();


        }

        private static void ListarClientesDeletadosMenu()
        {
            Console.WriteLine(" --------DELETAR CLIENTES -------- ");
            Console.WriteLine("digite o id do cliente que deseja deletar: ");

            int id = 0;

            int.TryParse(Console.ReadLine(), out id);

            Cliente? clienteParaDeletar = Cliente.listarPorId(id);

            if (Cliente.Deletar(clienteParaDeletar) >= 1)
            {
                Console.WriteLine("Cliente excluido com sucesso.");

            }
            else
            {
                Console.WriteLine("Cliente não excluido.");
            }

            Console.ReadKey();
            Console.Clear();


        }
        private static void VisualizarPontuação()

        {
            Console.WriteLine("-----------VIZUALIZAR PONTUAÇÃO CLIENTE ------------");
            Console.WriteLine("digite o id do cliente que deseja vizualizar pontuação: ");

            int idConsole;

            int.TryParse(Console.ReadLine(), out idConsole);

            Cliente? clienteBusca = Cliente.listarPorId(idConsole);

            if (clienteBusca == null)
            {

                Console.WriteLine("Cliente não encontrado");

            }
            else
            {

                Console.WriteLine($"ID: {clienteBusca.Id} " +
                    $"\nNome {clienteBusca.Nome} " +
                    $"\nPontos: {clienteBusca.Pontos} ");



            }
            Console.ReadKey();
            Console.Clear();


        }

    }
}


