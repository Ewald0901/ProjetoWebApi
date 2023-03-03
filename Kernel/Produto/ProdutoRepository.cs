using Kernel.DataContext;

namespace Kernel
{
    internal class ProdutoRepository : Context<Produto>
    {
        public ProdutoRepository(Repositorio rep) : base(rep) { }
    }   
}
