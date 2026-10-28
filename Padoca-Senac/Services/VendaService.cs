using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Services
{
    public static class VendaService
    {
        public static void MenuVendas()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=======PDV - Padaria pão & cia");
                Console.WriteLine("1 - Iniciar nova venda");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.WriteLine("=======================");
                Console.Write("Digite uma opção:  ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            iniciarNovaVenda();

                            break;
                        case "0":
                            sair = true;
                            break;
                        default:
                            Console.WriteLine("Opção Inválida");
                            break;

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro na venda {ex.Message}");
                    Console.WriteLine("\npressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }

        }
        public static void iniciarNovaVenda()
        {
            Console.WriteLine("--------------- NOVA VENDA ---------------");
                
                //1 
        }
    }
}
