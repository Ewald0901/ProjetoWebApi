using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kernel.Request
{
    public  class ProdutoRequest
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }       
        public string Nome { get; set; }  
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }     
        public DateTime DataFabricacao { get; set; }        
        public DateTime DataValidade { get; set; }      
        public int FornecedorId { get; set; }
    }
}
