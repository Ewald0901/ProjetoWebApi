using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Request
{
    public class FornecedorRequest
    {       
        public string NomeFornecedor { get; set; }                
        public string CnpjFornecedor { get; set; }        
        public string DescricaoFornecedor { get; set; }
    }
}
