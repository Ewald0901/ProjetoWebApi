using Canducci.Pagination;
using Kernel.DataContext;
using Kernel.Query;
using LinqKit;

namespace Kernel

{
    public class Produto : ProdutoDto
    {
        readonly Persistencia _ctx;
        private readonly string erroNomeObrigatorio = "Nome do produto é obrigatório";
        private readonly string produtoExiste = "O produto informado já existe cadastrado";
        private readonly string produtoNaoEncontrado = "O produto informad não foi encontrado";
        private readonly string produtoVencido = "O produto esta vencido";
        private readonly string produtoDataFabricacaoNofuturo = "O produto informado esta com data de fabricação no futuro";
        private readonly string produtoDataFabricacaoIgualOuMaiorDataValidade = "A data de fabricação não pode ser maior ou igual a data de validade";
        public Produto()
        {
            _ctx = new Persistencia(new Repositorio());
        }
        public Produto(Persistencia persistencia)
        {
            _ctx = persistencia;
        }
        public Produto Salvar()
        {
            this.CriadoEm = DateTime.Now;
            this.Nome = this.Nome.ToUpper().Trim();

            (bool valido, List<string> erros) validacaoProduto = Validar(this);

            if (!validacaoProduto.valido)
            {
                string erro = "";
                foreach (var item in validacaoProduto.erros)
                {
                    erro += item.ToString() + "\n";
                }
                throw new Exception(erro);
            }

            Produto produto = _ctx.Produto.Salvar(this);
            _ctx.Produto.SaveChanges();
            return produto;
        }
        public Produto Obter(ProdutoQuery query)
        {
            ExpressionStarter<Produto> filter = PredicateBuilder.New<Produto>(a => true);
            
            if (query.ProdutoId.HasValue)
                filter.And(a => a.Id == query.ProdutoId);

            if (!string.IsNullOrEmpty(query.CodigoBarras))
                filter.And(a => a.CodigoBarras == query.CodigoBarras.Trim());

            return _ctx.Produto.Obter(filter, "");
        }
        public List<Produto> Listar(ProdutoQuery query)
        {

            ExpressionStarter<Produto> filter = PredicateBuilder.New<Produto>(a => true);

            Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy = null;
            orderBy = new Func<IQueryable<Produto>, IOrderedQueryable<Produto>>(a => a.OrderByDescending(b => b.CriadoEm));

            if (query.ProdutoId.HasValue)
                filter.And(a => a.Id == query.ProdutoId);

            if (!string.IsNullOrEmpty(query.ProdutoNome))
                filter.And(a => a.Nome.ToUpper().Equals(query.ProdutoNome.Trim().ToUpper()));

            if (!string.IsNullOrEmpty(query.CodigoBarras))
                filter.And(a => a.CodigoBarras.ToUpper().Equals(query.CodigoBarras.Trim().ToUpper()));

            return _ctx.Produto.Listar(filter, orderBy, null, null, "").ToList();
        }
        public Produto Editar()
        {
            this.Nome = this.Nome.Trim().ToUpper();

            (bool valido, List<string> erros) validacaoProduto = Validar(this);

            if (!validacaoProduto.valido)
            {
                string erro = "";
                foreach (var item in validacaoProduto.erros)
                {
                    erro += item.ToString() + "\n";
                }
                throw new Exception(erro);
            }
            _ctx.Produto.Alterar(this);
            _ctx.Produto.SaveChanges();

            return this;
        }
        public Produto Excluir(int Id)
        {
            Produto produto = _ctx.Produto.Obter(Id);
            if (produto == null)
                throw new Exception(produtoNaoEncontrado);

            produto.Ativo = false;
            _ctx.Produto.Alterar(produto);
            _ctx.Produto.SaveChanges();
            return produto;
        }
        public (bool valido, List<string> erros) Validar(Produto produto)
        {
            bool valido = true;
            List<string> erros = new List<string>();

            ExpressionStarter<Produto> filter = PredicateBuilder.New<Produto>(a => true);
            filter.And(p => p.CodigoBarras.ToUpper().Equals(produto.CodigoBarras.Trim().ToUpper()));
            filter.And(p => p.Nome.ToUpper().Equals(produto.Nome.Trim().ToUpper()));

            Produto _prod = _ctx.Produto.Obter(filter);

            if (_prod != null)
                erros.Add(produtoExiste);

            if (produto.DataValidade < DateTime.Now)
                erros.Add(produtoVencido);

            if (produto.DataFabricacao >= produto.DataValidade)
                erros.Add(produtoDataFabricacaoIgualOuMaiorDataValidade);

            if (produto.DataFabricacao > DateTime.Now)
                erros.Add(produtoDataFabricacaoNofuturo);

            if (string.IsNullOrEmpty(produto.Nome))
                erros.Add(erroNomeObrigatorio);
            if (erros.Count() > 0)
                valido = false;

            return (valido, erros);
        }
    }
}
