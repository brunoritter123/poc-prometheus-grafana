namespace TestePrometheusGrafana.Efetivacao;

public class EfetivacaoRepository : IEfetivacaoRepository
{
    private readonly ICollection<EfetivacaoModel> _efetivacaos;
    private readonly MetricsEfetivacaoPrometheus metricsEfetivacao = new();
    public EfetivacaoRepository()
    {
        _efetivacaos = [];
    }
    public void Inserir(EfetivacaoModel efetivacao)
    {
        Random random = new();
        var tipoOrigem = random.Next(1, 10) <= 3 
            ? TipoOrigemEnum.Efetivacao 
            : TipoOrigemEnum.Simulacao;

        metricsEfetivacao.MetrificarEfetivacaoCriada(efetivacao.TipoBoleta,
                                                     efetivacao.TipoCanal,
                                                     tipoOrigem,
                                                     efetivacao.Pendencias);
        _efetivacaos.Add(efetivacao);
    }

    public IEnumerable<EfetivacaoModel> Get()
        => _efetivacaos;
}


public interface IEfetivacaoRepository
{
    public void Inserir(EfetivacaoModel efetivacao);

    public IEnumerable<EfetivacaoModel> Get();

}