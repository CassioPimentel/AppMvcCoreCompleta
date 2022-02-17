using Dev.Business.Models;
using System;
using System.Threading.Tasks;

namespace Dev.Business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task AtualizarEndereco(Endereco endereco);
        Task Remover(Guid id);
    }
}