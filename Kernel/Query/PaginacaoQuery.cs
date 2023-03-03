using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Query
{
    public class PaginacaoQuery
    {
        public int? pagina { get; set; }
        public int? quantidade { get; set; }
        internal int? Skip()
        {
            if (!this.pagina.HasValue || this.pagina.Value < 0)
                return null;

            if (!this.quantidade.HasValue)
                return null;

            return (this.pagina - 1) * this.quantidade;
        }
    }
}
