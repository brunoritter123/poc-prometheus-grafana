using TestePrometheusGrafana.Boleta;
using TestePrometheusGrafana.Efetivacao;

namespace TestePrometheusGrafana;

public sealed class Worker : BackgroundService
{
    private readonly IBoletaRepository _repository;
    private readonly IEfetivacaoRepository _repositoryEfetivacao;

    public Worker(IBoletaRepository repository, IEfetivacaoRepository repositoryEfetivacao)
    {
        _repository = repository;
        _repositoryEfetivacao = repositoryEfetivacao;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _repository.Inserir(BoletaModel.CreateFaker());
            _repositoryEfetivacao.Inserir(EfetivacaoModel.CreateFaker());
            _repositoryEfetivacao.Inserir(EfetivacaoModel.CreateFaker());
            _repositoryEfetivacao.Inserir(EfetivacaoModel.CreateFaker());
            _repositoryEfetivacao.Inserir(EfetivacaoModel.CreateFaker());
            _repositoryEfetivacao.Inserir(EfetivacaoModel.CreateFaker());

            await Task.Delay(700, stoppingToken);
        }
    }
}