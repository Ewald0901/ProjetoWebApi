using AutoMapper;
using Canducci.Pagination;
using Kernel;
using Kernel.DataContext;
using Kernel.Mapper;
using Kernel.Query;
using Kernel.Request;
using Kernel.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger; 
        private Produto _produto;
        private Persistencia _persistencia;
        public AutoMapper.IMapper MapperResponse;
        public AutoMapper.IMapper MapperRequest;

        public ProdutoController(ILogger<ProdutoController> logger)
        {

            _persistencia = new Persistencia(new Repositorio());
            _logger = logger;
            _produto = new Produto(_persistencia);
            MapperResponse = AutoMapping.mapperResponse;
            MapperRequest = AutoMapping.mapperRequest;
        }

        [HttpGet]
        public ActionResult<ProdutoResponse> Obter([FromQuery] ProdutoQuery query)
        {           
            return Ok(MapperResponse.Map<ProdutoResponse>(_produto.Obter(query)));
        }

        [HttpGet("Listar")]
        public virtual IActionResult Listar([FromQuery] ProdutoQuery query)
        {
            if (query.page == null || query.page == 0)
                query.page = 1;

            if (query.itensPorPagina == null || query.itensPorPagina == 0)
                query.itensPorPagina = 10;

            var result = MapperResponse.Map<List<ProdutoResponse>>(_produto.Listar(query));

            return Ok(result.ToPaginatedRest(query.page.Value, query.itensPorPagina.Value));
        }

        [HttpPost]
        public ActionResult<ProdutoResponse> Salvar([FromBody] ProdutoRequest request)
        {
            Produto produto = MapperRequest.Map<Produto>(request);
            return Ok(MapperResponse.Map<ProdutoResponse>(produto.Salvar()));
        }       

        [HttpPut]
        public ActionResult<ProdutoResponse> Editar(int Id,[FromBody] ProdutoRequest request)
        {
           Produto produto = MapperRequest.Map<Produto>(request);
            produto.Id = Id;
            return Ok(MapperResponse.Map<ProdutoResponse>(produto.Editar()));
        }

        [HttpDelete]
        public ActionResult<ProdutoResponse> Excluir(int Id)
        {   
            return Ok(MapperResponse.Map<ProdutoResponse>(_produto.Excluir(Id)));
        }

    }
}
