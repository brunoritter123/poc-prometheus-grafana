using Prometheus;
using TestePrometheusGrafana.Boleta;
using static TestePrometheusGrafana.Efetivacao.EfetivacaoModel;

namespace TestePrometheusGrafana.Efetivacao;

public class MetricsEfetivacaoPrometheus
{
    private readonly CounterConfiguration Tags;

    private readonly Counter CounterResultadoSimulacaoEfetivacao;

    private readonly Counter CounterPendenciasExibidas;

    public MetricsEfetivacaoPrometheus()
    {
        Tags = new()
        {
            LabelNames = ["tipoBoleta", "tipoCanal", "tipo_pendencia", "tipo_origem"],
        };

        CounterResultadoSimulacaoEfetivacao = Metrics.CreateCounter(name: "pendencias_transacionais",
                                       help: "Conta o resultado das simulações e efetivações",
                                       configuration: Tags);


        CounterPendenciasExibidas= Metrics.CreateCounter(name: "pendencias_exibidas",
                                       help: "Conta as pendêcias exibidas para o usuário",
                                       configuration: Tags);
    }

    public void MetrificarEfetivacaoCriada(TipoBoletaEnum tipoEfetivacao,
                                           CanalEnum tipoCanal,
                                           TipoOrigemEnum tipoOrigem,
                                           List<PendenciaModel> pendencias)
    {
        foreach (var pendencia in pendencias)
        {
            CounterPendenciasExibidas
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                pendencia.TipoPendencia.ToString(),
                tipoOrigem.ToString())
            .Inc();
        }

        if (!pendencias.Any())
        {
            CounterResultadoSimulacaoEfetivacao
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                "Ok",
                tipoOrigem.ToString())
            .Inc();

            return;
        }
        
        if (pendencias.Any(x => x.TipoPendencia == TipoPendenciaEnum.Falha))
        {
            CounterResultadoSimulacaoEfetivacao
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                TipoPendenciaEnum.Falha.ToString(),
                tipoOrigem.ToString())
            .Inc();
        }

        if (pendencias.Any(x => x.TipoPendencia == TipoPendenciaEnum.Impeditiva))
        {
            CounterResultadoSimulacaoEfetivacao
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                TipoPendenciaEnum.Impeditiva.ToString(),
                tipoOrigem.ToString())
            .Inc();
        }

        if (pendencias.Any(x => x.TipoPendencia == TipoPendenciaEnum.Informativa))
        {
            CounterResultadoSimulacaoEfetivacao
            .WithLabels(
                tipoEfetivacao.ToString(),
                tipoCanal.ToString(),
                TipoPendenciaEnum.Informativa.ToString(),
                tipoOrigem.ToString())
            .Inc();
        }
    }
}