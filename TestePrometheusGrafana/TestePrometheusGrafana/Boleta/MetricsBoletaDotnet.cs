using System.Diagnostics.Metrics;

namespace TestePrometheusGrafana.Boleta;

public class MetricsBoletaDotnet
{
    private static readonly IEnumerable<KeyValuePair<string, object?>>? Tags = new List<KeyValuePair<string, object?>>()
    {
        new ("tipoBoleta", null),
        new ("tipoCanal", null)
    };

    private static readonly Meter MyMeter = new Meter("Boletas");
    private static readonly Counter<int> CounterBoletasCriadas = MyMeter.CreateCounter<int>(
        name: "BoletasCriadas",
        unit: "boleta",
        description: "Conta quantidade de boletas criadas",
        tags: Tags
    );

    private static readonly Counter<double> CounterValorBoletasCriadas = MyMeter.CreateCounter<double>(
        name: "ValorBoletasCriadas",
        unit: "Reais",
        description: "Conta valor das boletas criadas",
        tags: Tags
    );

    public void MetrificarBoletaCriada(TipoBoletaEnum tipoBoleta, CanalEnum tipoCanal, decimal valor)
    {
        CounterBoletasCriadas.Add(1, new KeyValuePair<string, object?>[]
        {
            new("tipoBoleta", tipoBoleta),
            new("tipoCanal", tipoCanal)
        });

        CounterValorBoletasCriadas.Add((double)valor, new KeyValuePair<string, object?>[]
        {
            new("tipoBoleta", tipoBoleta),
            new("tipoCanal", tipoCanal)
        });
    }
}