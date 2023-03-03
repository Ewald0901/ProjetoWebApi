using Kernel.DataContext;

namespace Kernel
{
    internal class FornecedorRepository : Context<Fornecedor>
    {
        public FornecedorRepository(Repositorio rep) : base(rep) { }
       
    }  
}
