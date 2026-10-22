using Padoca_Senac.Models;
using Padoca_Senac.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Services
{
    internal class ClienteServices
    {

        public static void MenuCliente()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine(" ----- GESTÃO DE CLIENTES ----- ");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar todos os clientes");
                Console.WriteLine("3 - Buscar Cliente por ID");
                Console.WriteLine("4 - Atualizar Cliente");
                Console.WriteLine("5 - Excluir Cliente");
                Console.WriteLine("6 - Visualizar pontuação");
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

                            cadastrarClienteMenu();

                            break;

                        case "2": // Listar
                            listarClientesMenu();

                            break;

                        case "3": //listar por id
                            ListarIdMenu();
                            break;

                        case "4":
                            listarAtualizarcliente(); //listarClientesMenu

                            break;

                        case "5":

                            ListarClientesDeletadosMenu();

                            break;

                        case "6":

                            VisualizarPontuação();

                            break;

                    }

                } catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }


            }



        }

        private static void cadastrarClienteMenu()
        {



            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite o cpf do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de pontos");
            int pontosCliente = int.Parse(Console.ReadLine());


            Cliente NovoCliente = new Cliente(
                nome: nomeCliente,
                cpf: cpfCliente,
                telefone: telefoneCliente,
               endereco: enderecoCliente,
               pontos: pontosCliente
            );
            int resultadoInsert = Cliente.Inserir(NovoCliente);

            Console.Clear();


            if (resultadoInsert > 0) Console.WriteLine("Cliente Cadastrado com Sucesso!!");

        }

        private static void listarClientesMenu()
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



        private static void listarAtualizarcliente()
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
