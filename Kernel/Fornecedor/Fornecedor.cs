using Canducci.Pagination;
using Kernel.DataContext;
using Kernel.Query;
using Kernel.Response;
using LinqKit;

namespace Kernel
{
    public class Fornecedor : FornecedorDto
    {
        static Persistencia _ctx;
        public static string fornecedorJaExiste = "Esse fornecedor já esta cadastrado";
        public Fornecedor()
        {
            _ctx = new Persistencia(new Repositorio());
        }
        public Fornecedor(Persistencia persistencia)
        {
            _ctx = persistencia;
        }
        public FornecedorResponse Salvar() 
        {
            this.Nome = this.Nome.Trim().ToUpper();

            (bool valido, List<string> erros) validacaoFornecedor = Validar(this);

            if (!validacaoFornecedor.valido)
            {
                string erro = "";
                foreach (var item in validacaoFornecedor.erros)
                {
                    erro += item.ToString() + "\n";
                }
                throw new Exception(erro);
            }

            Fornecedor fornecedor = _ctx.Fornecedor.Salvar(this);

            _ctx.Fornecedor.SaveChanges();
            return Mapear(fornecedor); ;

        }
        public FornecedorResponse Obter(int Id) 
        {
            Fornecedor fornecedor = _ctx.Fornecedor.Obter(Id);

            return Mapear(fornecedor);
        }
        public FornecedorResponse Excluir(int Id) 
        {
            Fornecedor fornecedor = _ctx.Fornecedor.Obter(Id);

            fornecedor.Ativo = false;
            _ctx.Fornecedor.Alterar(fornecedor);
            _ctx.Fornecedor.SaveChanges();

            return Mapear(fornecedor);

        }
        public FornecedorResponse Editar()
        {
            this.Nome = this.Nome.Trim().ToUpper();

            (bool valido, List<string> erros) validacaoFornecedor = Validar(this);

            if (!validacaoFornecedor.valido)
            {
                string erro = "";
                foreach (var item in validacaoFornecedor.erros)
                {
                    erro += item.ToString() + "\n";
                }
                throw new Exception(erro);
            }
            Fornecedor fornecedor = _ctx.Fornecedor.Alterar(this);
            _ctx.Fornecedor.SaveChanges();

            return Mapear(fornecedor);
        }
        public PaginatedRest<FornecedorResponse> Listar(FornecedorQuery query)
        {

            ExpressionStarter<Fornecedor> filter = PredicateBuilder.New<Fornecedor>(a => true);

            Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = null;
            orderBy = new Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>>(a => a.OrderByDescending(b => b.CriadoEm));

            if (query.FornecedorId.HasValue)
                filter.And(a => a.Id == query.FornecedorId);

            if (!string.IsNullOrEmpty(query.FornecedorNome))
                filter.And(a => a.Nome.ToUpper().Equals(query.FornecedorNome.Trim().ToUpper()));

            if (!string.IsNullOrEmpty(query.Cnpj))
                filter.And(a => a.Cnpj.ToUpper().Equals(query.Cnpj.Trim().ToUpper()));

            return _ctx.Fornecedor.Listar(filter, orderBy, null, null, "").Select(f => new FornecedorResponse()
                    {
                        CnpjFornecedor = f.Cnpj,
                        DescricaoFornecedor = f.Descricao,
                        IdFornecedor = f.Id,
                        NomeFornecedor = f.Nome
                    }
            ).ToPaginatedRest(query.page.Value, query.itensPorPagina.Value);
        }
        public (bool valido, List<string> erros) Validar(Fornecedor fornecedor)
        {
            bool valido = true;
            List<string> erros = new List<string>();

            ExpressionStarter<Fornecedor> filter = PredicateBuilder.New<Fornecedor>(a => true);
            filter.And(p => p.Cnpj.ToUpper().Equals(fornecedor.Cnpj.Trim().ToUpper()));
            filter.And(p => p.Nome.ToUpper().Equals(fornecedor.Nome.Trim().ToUpper()));

            Fornecedor _fornecedor = _ctx.Fornecedor.Obter(filter);

            if (_fornecedor != null)
                erros.Add(fornecedorJaExiste);

            if (erros.Count() > 0)
                valido = false;

            return (valido, erros);

        }
        private FornecedorResponse Mapear(Fornecedor fornecedor) 
        {
            FornecedorResponse response = new FornecedorResponse()
            {
                CnpjFornecedor = fornecedor.Cnpj,
                DescricaoFornecedor = fornecedor.Descricao,
                IdFornecedor = fornecedor.Id,
                NomeFornecedor = fornecedor.Nome
            };
            return response;
        }
    }


}
