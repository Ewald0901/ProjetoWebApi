
using Kernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.DataContext
{
    public class Persistencia
    {
        Repositorio _repositorio;
        public Persistencia(Repositorio repositorio) 
        {
            _repositorio = repositorio; 
        }
        internal ProdutoRepository Produto => new ProdutoRepository(_repositorio);
        internal FornecedorRepository Fornecedor => new FornecedorRepository(_repositorio);
    }
}
