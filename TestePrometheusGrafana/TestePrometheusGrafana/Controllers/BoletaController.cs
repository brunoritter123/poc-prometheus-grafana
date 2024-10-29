using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using TestePrometheusGrafana.Boleta;

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
        public IEnumerable<BoletaModel> Get([FromQuery] int statuscode = 200)
        {
            Response.StatusCode = statuscode;
            return _boletaRepository.Get();
        }

        [HttpPost(Name = "PostBoleta")]
        public BoletaModel Post([FromQuery] int statuscode = 200)
        {
            var body = BoletaModel.CreateFaker();
            _boletaRepository.Inserir(body);

            Response.StatusCode = statuscode;
            return body;
        }
    }
}
