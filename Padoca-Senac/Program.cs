using backPadaria.Model;
using Padoca_Senac.banco;
using Padoca_Senac.Models;
using Padoca_Senac.Services;
using System.ComponentModel.Design;

internal class Program
{
    static void Main(string[] args)

    {

        LoginFuncionario login = new LoginFuncionario(3, "jp_padoca@padariapaocia.com.br", "deumaito", "Diretor");
        LoginFuncionario.Inserir(login);

     


    }
}
