using Prometheus;

namespace TestePrometheusGrafana.Boleta;

public class MetricsBoletaPrometheus
{
    private readonly CounterConfiguration Tags;

    private readonly Counter CounterBoletasCriadas;

    private readonly Counter CounterValorBoletasCriadas;

    public MetricsBoletaPrometheus()
    {
        Tags = new()
        {
            LabelNames = ["tipoBoleta", "tipoCanal"],
        };

        CounterBoletasCriadas = Metrics.CreateCounter(name: "BoletasCriadas",
                                       help: "Conta quantidade de boletas criadas",
                                       configuration: Tags);

        CounterValorBoletasCriadas = Metrics.CreateCounter(name: "ValorBoletasCriadas",
                                             help: "Conta o valor movimentado pelas boletas criadas",
                                             configuration: Tags); ;
    }

    public void MetrificarBoletaCriada(TipoBoletaEnum tipoBoleta, CanalEnum tipoCanal, decimal valor)
    {
        CounterBoletasCriadas
            .WithLabels(tipoBoleta.ToString(), tipoCanal.ToString())
            .Inc();

        CounterValorBoletasCriadas
            .WithLabels(tipoBoleta.ToString(), tipoCanal.ToString())
            .Inc((double)valor);
    }
}