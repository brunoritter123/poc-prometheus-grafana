using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace TestePrometheusGrafana.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoletaController : ControllerBase
    {
        private readonly ILogger<BoletaController> _logger;
        private readonly IBoletaRepository _boletaRepository;

        public BoletaController(ILogger<BoletaController> logger, IBoletaRepository boletaRepository)
        {
            _logger = logger;
            _boletaRepository = boletaRepository;
        }

        [HttpGet(Name = "GetBoleta")]
        public IEnumerable<Boleta> Get()
        {
            return _boletaRepository.Get();
        }

        [HttpPost(Name = "PostBoleta")]
        public Boleta Post()
        {
            var body = Boleta.CreateFaker();
            _boletaRepository.Inserir(body);
            return body;
        }
    }
}
