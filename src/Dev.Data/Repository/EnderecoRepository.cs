using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Dev.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                                     .FirstOrDefaultAsync(p => p.FornecedorId == fornecedorId);
        }
    }
}
