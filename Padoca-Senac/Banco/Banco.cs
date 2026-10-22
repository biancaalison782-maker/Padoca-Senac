
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Padoca_Senac.banco;

public static class Banco
{
    private static string stringConnection = "Server=10.91.45.59;Port=3306;Database=padaria_bd;Uid=admin;Pwd=12345678";

    public static void connectAndTest()
    {
        using (var connection = new MySqlConnection(stringConnection))
        {
            try
            {
                Console.WriteLine("Tentando conectar ao banco de dados...");
                connection.Open();

                using (var command = new MySqlCommand(
                    "SELECT VERSION();", connection))
                {
                    string? version = command.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Versão do MariaDB: {version}");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao conectar no banco de dados!");
                Console.WriteLine(ex.Message);
            }
        }
    }

    //Declaração do método: público  e estático, retorna um número inteiro (linhas afetadas) e recebe um objeto MySqlCommand com a instrução SQL já configurada como parâmetro
    public static int executarComando(MySqlCommand cmd)
    {
        //O bloco using garante que a coneção será automaticamente fechada e liberada da memória ao final do bloco, mesmo que ocorram erros no meio do processo.

        //Cria a conexão utilizando a variavel de ambiente
        using (var conexao = new MySqlConnection(stringConnection))
        {
           //Vinculação: Diz ao comando SQL qm qual conexão ele deve ser executado.
            cmd.Connection = conexao;

            //Tratamento de erros: Inicia um bloco de tentativa para proteger o programa de falhas abruptas.
            try
            {
                // Abertura da conexão: Estabelece efetivamente a comunicação com o servidor Mysql
                conexao.Open();

                // Executa o comando SQL (Insert, Update ou Delete) no banco de dados.
                // Retorna um número inteiro correspondente ao total de linhas que foram modificadas.
                return cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no Banco de dados: {ex.Message}");
                return -1;
                
            }
        }
    }
    public static int executarComandoComRetornoId(MySqlCommand command)
    {

        using (var conexao = new MySqlConnection(stringConnection))
        {
            command.Connection = conexao;

            try
            {
                conexao.Open();

                /* O ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira 
                 linha do resultado. É perfeito para pegar um único valor (como um ID recém-gerado).
                 Ele retorna um tipo 'object' porque não sabe de antemão qual o tipo do dado no banco.
                */
                object objRetorno = command.ExecuteScalar();

                int retorno = 0;

                /*  Verificação de segurança: Checa se o banco realmente retornou algo.
                    Verifica tanto se o objeto C# não é nulo (null) quanto se
                    não é um nulo do próprio banco de dados (DBNull.Value).
                */
                if (objRetorno != null && objRetorno != DBNull.Value)
                {

                    /* Se passou na verificação, converte o objeto (que veio como texto/objeto)
                        para um número inteiro (int). 
                    */

                    retorno = int.Parse(objRetorno.ToString());
                }

                return retorno;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro de banco de dados{ex.Message}");
                return -1;
            }

        }

    } // Método RetornoComId
    public static MySqlDataReader executarComRetornoDeDados(MySqlCommand command)
    {


        // Cria a conexão com o banco.
        // ATENÇÃO: Aqui NÃO usamos o bloco 'using' para a conexão. Se usássemos, a conexão 
        // seria fechada antes de retornar os dados, e o programa quebraria ao tentar ler a tabela.
        var conexao = new MySqlConnection(stringConnection);

        command.Connection = conexao;

        try
        {
            // Abre a porta de comunicação com o MySQL
            conexao.Open();

            // Executa a consulta e retorna o "leitor" de dados.
            // O CommandBehavior.CloseConnection é a peça-chave deste método: ele avisa ao leitor 
            // que assim que ele (o leitor) for fechado ou descartado lá na tela/classe que o chamou, 
            // a conexão com o banco deve ser fechada automaticamente junto com ele.
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        catch (MySqlException ex)
        {

            // O Dispose() já executa o Close() e libera a memória.
            conexao.Dispose();

            Console.WriteLine($"Erro na leitura do banco de dados: {ex.Message}");

            // Ao invés de retornar null, relança o erro (throw) para que o método que 
            // chamou este ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuário.
            throw;
        }

    }


} // Classe


