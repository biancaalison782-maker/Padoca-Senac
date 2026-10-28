using Padoca_Senac.Models;
using Padoca_Senac.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Services
{
    internal class MarcaServices
    {
            public static void MenuMarca()
            {
                bool sair = false;

                while (!sair)
                {
                    Console.WriteLine(" ----- GESTÃO DE MARCAS ----- ");
                    Console.WriteLine("1 - Cadastrar Marcas");
                    Console.WriteLine("2 - Listar todos as marcas");
                    Console.WriteLine("3 - Buscar Marcas por ID");
                    Console.WriteLine("4 - Atualizar Marca");
                    Console.WriteLine("5 - Excluir Marca");
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

                                cadastrarMarcaMenu();

                                break;

                            case "2": // Listar
                                listarMarcasMenu();

                                break;

                            case "3": //listar por id
                                ListarIdMenu();
                                break;

                            case "4":
                                listarAtualizarmarca(); //listarClientesMenu

                                break;

                            case "5":

                                ListarMarcasDeletadosMenu();

                             break;

                          

                                

                                

                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                    }


                }



            }

            private static void cadastrarMarcaMenu()
            {



                Console.WriteLine("Digite o nome da Marca: ");
                string nomeMarca = Console.ReadLine();
                

                Console.WriteLine("Digite o telefone da marca: ");
                string telefoneMarca = Console.ReadLine();

               

                Marca NovoMarca = new Marca(
                    nome: nomeMarca,
                    telefone: telefoneMarca
                  
                 
                );
                int resultadoInsert = Marca.Inserir(NovoMarca);

                Console.Clear();


                if (resultadoInsert > 0) Console.WriteLine("Marca Cadastrada com Sucesso!!");

            }

            private static void listarMarcasMenu()
            {
                Console.WriteLine("------ LISTA DE Marcas ----- ");

                List<Marca> lista = Marca.ListarTodos();

                foreach (var marca in lista)
                {
                    Console.WriteLine($"ID: {marca.Id} | Nome {marca.Nome} | Telefone: {marca.Telefone} ");
                }

                if (lista.Count == 0)
                {
                    Console.WriteLine("Nenhuma marca cadastrada no sistema");

                }
                else
                {
                    foreach (var marca in lista)
                    {
                        Console.WriteLine($"ID: {marca.Id} | Nome {marca.Nome} | Telefone: {marca.Telefone}");
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

                Marca? marcaBusca = Marca.listarPorId(idConsole);

                if (marcaBusca == null)
                {

                    Console.WriteLine("Cliente não encontrado");

                }
                else
                {

                Console.WriteLine($"ID: {marcaBusca.Id} " +
                    $"\nNome {marcaBusca.Nome} " +
                    $"\nTelefone: {marcaBusca.Telefone} ");


                }

                Console.ReadKey();
                Console.Clear();

            }



            private static void listarAtualizarmarca()
            {
                Console.WriteLine("------ATUALIZAR MARCA ------ ");

                Console.WriteLine("Digite o id da marca que você deseja atualizar");
                int id = 0;
                int.TryParse(Console.ReadLine(), out id);


                Marca? marcasParaAtualizar = Marca.listarPorId(id);

                if (marcasParaAtualizar != null)
                {
                    Console.WriteLine($"Atualizando dados de {marcasParaAtualizar.Nome}");
                    Console.WriteLine("Pressione ENTER sem digitar nada para manter o dado atual");
                    Console.WriteLine($" Telefone Atual: ({marcasParaAtualizar.Telefone})");
                    Console.WriteLine($"Digite o Telefone Novo:    ");

                    string novoTelefone = Console.ReadLine();
                    if (!string.IsNullOrEmpty(novoTelefone)) marcasParaAtualizar.Telefone = novoTelefone;
                    Console.WriteLine($"Novo Endereço Atual: ({marcasParaAtualizar.Telefone})");


                Console.WriteLine("---------------------------------------------");

                    int resultadoAtualizacao = Marca.Atualizar(marcasParaAtualizar);

                    if (resultadoAtualizacao > 0) Console.WriteLine("Marca atualizado com sucesso");

                }
                else
                {

                    Console.WriteLine("Marca não encontrado");

                }


                Console.ReadKey();
                Console.Clear();


            }

        private static void ListarMarcasDeletadosMenu()
        {
            Console.WriteLine(" --------DELETAR MARCAS -------- ");
            Console.WriteLine("digite o id da marca que deseja deletar: ");

            int id = 0;

            int.TryParse(Console.ReadLine(), out id);

            Marca? marcaParaDeletar = Marca.listarPorId(id);

            if (Marca.Deletar(marcaParaDeletar) >= 1)
            {
                Console.WriteLine("Marca excluido com sucesso.");

            }
            else
            {
                Console.WriteLine("Marca não excluido.");
            }

            Console.ReadKey();
            Console.Clear();

        }
        
    }

}

