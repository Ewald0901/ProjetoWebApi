using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.DataContext.Model
{
    public class PaginacaoModel
    {

        public int total_paginas { get; set; }  
        public int total_elementos { get; set; }   
        public int pagina_corrente { get; set; }     
        public int tamanho_pagina { get; set; }       
        public bool ultimo { get; set; }      
        public bool primeiro { get; set; }
        public object conteudo { get; set; }
    }
}
