using Prometheus;
using TestePrometheusGrafana.Boleta;
using static TestePrometheusGrafana.Efetivacao.EfetivacaoModel;

namespace TestePrometheusGrafana.Efetivacao;

public class MetricsEfetivacaoPrometheus
{
    private readonly CounterConfiguration Tags;

    private readonly Counter CounterEfetivacaosCriadas;

    private readonly Counter CounterValorEfetivacaosCriadas;

    public MetricsEfetivacaoPrometheus()
    {
        Tags = new()
        {
            LabelNames = ["tipoBoleta", "tipoCanal", "tipo_pendencia", "tipo_origem"],
        };

        CounterEfetivacaosCriadas = Metrics.CreateCounter(name: "pendencias_transacionais",
                                       help: "Conta o resultado de simulações e efetivações",
                                       configuration: Tags);
    }

    public void MetrificarEfetivacaoCriada(TipoBoletaEnum tipoEfetivacao,
                                           CanalEnum tipoCanal,
                                           TipoOrigemEnum tipoOrigem,
                                           List<PendenciaModel> pendencias)
    {
        if (!pendencias.Any())
        {
            CounterEfetivacaosCriadas
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                "Ok",
                tipoOrigem.ToString())
            .Inc();

            return;
        }
        foreach (var pendencia in pendencias)
        {
            CounterEfetivacaosCriadas
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                pendencia.TipoPendencia.ToString(),
                tipoOrigem.ToString())
            .Inc();
        }
    }
}