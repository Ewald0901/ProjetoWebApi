using Kernel;
using Kernel.DataContext;
using Kernel.Query;
using Kernel.Request;
using Kernel.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FornecedorController : Controller
    {
        private readonly ILogger<FornecedorController> _logger;
        private readonly IConfiguration _configuration;
      
        private Fornecedor _fornecedor;
        private Persistencia _persistencia;
        public FornecedorController(ILogger<FornecedorController> logger)
        {
            _persistencia = new Persistencia(new Repositorio());
            _logger = logger;
            _fornecedor = new Fornecedor(_persistencia);
            
        }

        [HttpGet("{Id}")]
        public ActionResult<FornecedorResponse> Obter(int Id)
        {
            return Ok(_fornecedor.Obter(Id));
        }


        [HttpPost]
        public ActionResult<FornecedorResponse> Salvar([FromBody] FornecedorRequest request)
        {
            Fornecedor fornecedor = new Fornecedor()
            {
                Cnpj = request.CnpjFornecedor,
                Descricao = request.DescricaoFornecedor.Trim().ToUpper(),
                Nome = request.NomeFornecedor.Trim().ToUpper()
                
            };
            return Ok(fornecedor.Salvar());
        }

        [HttpGet("Listar")]
        public ActionResult<FornecedorResponse> Listar([FromQuery] FornecedorQuery query)
        {

            if (query.page == null || query.page == 0)
                query.page = 1;

            if (query.itensPorPagina == null || query.itensPorPagina == 0)
                query.itensPorPagina = 10;
            return Ok(_fornecedor.Listar(query));
        }

        [HttpPut]
        public ActionResult<FornecedorResponse> Editar(int Id, [FromBody] FornecedorRequest request)
        {
            Fornecedor fornecedor = new Fornecedor()
            {
                Cnpj = request.CnpjFornecedor,
                Descricao = request.DescricaoFornecedor.Trim().ToUpper(),
                Nome = request.NomeFornecedor.Trim().ToUpper()

            };
            fornecedor.Id = Id;
            return Ok(fornecedor.Editar());
        }

        [HttpDelete]
        public ActionResult<FornecedorResponse> Excluir(int Id)
        {
            return Ok(_fornecedor.Excluir(Id));
        }

    }
}
