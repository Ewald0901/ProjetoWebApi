using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Query
{
    public class ProdutoQuery
    {
        public int? ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public string? CodigoBarras { get; set; }
        public int? page { get; set; }
        public int? itensPorPagina { get; set; }

    }
}
