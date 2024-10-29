using Microsoft.AspNetCore.Mvc;
using TestePrometheusGrafana.Efetivacao;

namespace TestePrometheusGrafana.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EfetivacaoController : ControllerBase
    {
        private readonly ILogger<EfetivacaoController> _logger;
        private readonly IEfetivacaoRepository _efetivacaoRepository;

        public EfetivacaoController(ILogger<EfetivacaoController> logger,
                                IEfetivacaoRepository efetivacaoRepository)
        {
            _logger = logger;
            _efetivacaoRepository = efetivacaoRepository;
        }

        [HttpGet(Name = "GetEfetivacoes")]
        public IEnumerable<EfetivacaoModel> Get([FromQuery] int statuscode = 200)
        {
            Response.StatusCode = statuscode;
            return _efetivacaoRepository.Get();
        }

        [HttpPost(Name = "PostEfetivacao")]
        public EfetivacaoModel Post([FromQuery] int statuscode = 200)
        {
            var body = EfetivacaoModel.CreateFaker();
            _efetivacaoRepository.Inserir(body);

            Response.StatusCode = statuscode;
            return body;
        }
    }
}
