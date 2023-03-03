using Kernel.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kernel
{
    public class FornecedorDto : EntityDto
    {
        [Column("nome")]
        public string Nome { get; set; }

        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }
    }
}
