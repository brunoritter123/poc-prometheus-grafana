using Prometheus;

namespace TestePrometheusGrafana;

public class BoletaRepository: IBoletaRepository
{
    private readonly ICollection<Boleta> _boletas;
    private readonly Counter counter = Metrics.CreateCounter("BoletasCriadas", "Conta quantidade de boletas criadas",
        new CounterConfiguration
        {
            LabelNames = ["tipoBoleta", "tipoCanal"],
        });

    private readonly Counter counterValor = Metrics.CreateCounter("ValorBoletasCriadas", "Conta o valor movimentado pelas boletas criadas",
        new CounterConfiguration
        {
            LabelNames = ["tipoBoleta", "tipoCanal"],
        });

    public BoletaRepository()
    {
        _boletas = [];
    }
    public void Inserir(Boleta boleta)
    {
        counter.WithLabels(boleta.TipoBoleta.ToString(),
                           boleta.TipoCanal.ToString()).Inc();


        counterValor.WithLabels(boleta.TipoBoleta.ToString(),
                           boleta.TipoCanal.ToString()).Inc((double)boleta.Valor);
        _boletas.Add(boleta);
    }

    public IEnumerable<Boleta> Get()
        => _boletas;
}


public interface IBoletaRepository
{
    public void Inserir(Boleta boleta);

    public IEnumerable<Boleta> Get();

}