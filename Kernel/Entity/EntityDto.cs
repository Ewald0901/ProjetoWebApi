using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kernel.Entity
{
    public abstract class EntityDto
    {
        [Key, Column("id"), JsonPropertyName("id")]
        public virtual int Id { get; set; }

        [Column("criado_em"), JsonPropertyName("criado_em")]
        public virtual DateTime CriadoEm { get; set; } = DateTime.Now;

        [Column("ativo"), JsonPropertyName("ativo")]
        public virtual Boolean Ativo { get; set; } = true;
    }
}
