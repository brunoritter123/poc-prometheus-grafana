namespace TestePrometheusGrafana.Boleta;

public class BoletaRepository : IBoletaRepository
{
    private readonly ICollection<BoletaModel> _boletas;
    private readonly MetricsBoletaPrometheus metricsBoleta = new();
    public BoletaRepository()
    {
        _boletas = [];
    }
    public void Inserir(BoletaModel boleta)
    {
        metricsBoleta.MetrificarBoletaCriada(boleta.TipoBoleta, boleta.TipoCanal, boleta.Valor);
        _boletas.Add(boleta);
    }

    public IEnumerable<BoletaModel> Get()
        => _boletas;
}


public interface IBoletaRepository
{
    public void Inserir(BoletaModel boleta);

    public IEnumerable<BoletaModel> Get();

}