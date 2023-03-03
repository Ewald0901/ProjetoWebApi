using Kernel.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kernel
{
    public class ProdutoDto : EntityDto
    {
        [Column("nome")]       
        public string Nome { get; set; }
        
        [Column("codigo_barras")]
        public string CodigoBarras { get; set; }
        
        [Column("descricao")]
        public string Descricao { get; set; }
        
        [Column("data_fabricacao")]
        public DateTime DataFabricacao { get; set; }
        
        [Column("data_validade")]
        public DateTime DataValidade { get; set; }

        [Column("fornecedor_id"), ForeignKey("Fornecedor")]
        public virtual int FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
