using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class ProdutoFabricado : Produto
{
    private int produtoId;
    private string descricaoProduto;


    public int ProdutoId { get => produtoId; set => produtoId = value; }
    public string DescricaoProduto { get => descricaoProduto; set => descricaoProduto = value; }

  public ProdutoFabricado(string nome, decimal preco, string categoria, int fabricado,  int marcaId, string descricaoProduto) : base(nome, preco, categoria, fabricado, marcaId)

  {              
        this.DescricaoProduto = descricaoProduto;
  }

    public ProdutoFabricado(int id, string nome, decimal preco, string categoria, int fabricado, int marcaId, string descricaoProduto) : base(id, nome, preco, categoria, fabricado, marcaId)
    {
        this.ProdutoId = produtoId;
        this.DescricaoProduto = descricaoProduto;

  }
}