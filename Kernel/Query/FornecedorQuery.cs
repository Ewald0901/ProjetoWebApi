namespace Kernel.Query
{
    public  class FornecedorQuery
    {
        public int? FornecedorId { get; set; }
        public string? FornecedorNome { get; set; }
        public string? Cnpj { get; set; }
        public int? page { get; set; }
        public int? itensPorPagina { get; set; }
    }
}
