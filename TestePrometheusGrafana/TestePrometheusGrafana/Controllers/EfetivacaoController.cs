using Microsoft.AspNetCore.Mvc;
using Prometheus;
using TestePrometheusGrafana.Boleta;
using TestePrometheusGrafana.Efetivacao;

namespace TestePrometheusGrafana.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EfetivacaoController : ControllerBase
    {
        private readonly ILogger<BoletaController> _logger;
        private readonly IEfetivacaoRepository _efetivacaoRepository;

        public EfetivacaoController(ILogger<BoletaController> logger,
                                IEfetivacaoRepository efetivacaoRepository)
        {
            _logger = logger;
            _efetivacaoRepository = efetivacaoRepository;
        }

        [HttpGet(Name = "GetEfetivacoes")]
        public IEnumerable<EfetivacaoModel> Get()
        {
            return _efetivacaoRepository.Get();
        }

        [HttpPost(Name = "PostEfetivacao")]
        public EfetivacaoModel Post()
        {
            var body = EfetivacaoModel.CreateFaker();
            _efetivacaoRepository.Inserir(body);
            return body;
        }
    }
}
