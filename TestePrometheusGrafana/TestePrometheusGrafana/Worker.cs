
namespace TestePrometheusGrafana;

public sealed class Worker : BackgroundService
{
    private readonly IBoletaRepository _repository;

    public Worker(IBoletaRepository repository)
    {
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _repository.Inserir(Boleta.CreateFaker());
            await Task.Delay(700, stoppingToken);
        }
    }
}