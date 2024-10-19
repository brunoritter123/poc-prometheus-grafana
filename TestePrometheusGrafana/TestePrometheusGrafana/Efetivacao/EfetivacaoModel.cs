using Bogus;
using TestePrometheusGrafana.Boleta;

namespace TestePrometheusGrafana.Efetivacao;

public class EfetivacaoModel
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public TipoBoletaEnum TipoBoleta { get; set; }
    public CanalEnum TipoCanal { get; set; }

    public List<PendenciaModel> Pendencias { get; set; } = [];

    public class PendenciaModel
    {
        public string Mensagem { get; set; } = string.Empty;
        public TipoPendenciaEnum TipoPendencia { get; set; }
        public TipoOrigemEnum TipoOrigem { get; set; }
    }

    public static EfetivacaoModel CreateFaker()
    {
        return new Faker<EfetivacaoModel>("pt_BR")
            .RuleFor(x => x.Id, Guid.NewGuid())
            .RuleFor(x => x.TipoBoleta, f => (TipoBoletaEnum)GeraTipoBoleta())
            .RuleFor(x => x.TipoCanal, f => f.Random.Int(1, 10) <= 3 ? CanalEnum.ION : CanalEnum.IFA)
            .RuleFor(x => x.Data, f => f.Date.Between(DateTime.Now, DateTime.Now.AddDays(1)))
            .RuleFor(x => x.Pendencias, GeraPendencias())
            .Generate();
    }

    private static List<PendenciaModel> GeraPendencias()
    {
        Random random = new();
        int qtdPendencias = random.Next(-100, 3); // uns 50% de chance de não gerar pendências

        List<PendenciaModel> pendencias = [];

        for (int i = 0; i < qtdPendencias; i++)
        {
            var pendencia = new Faker<PendenciaModel>("pt_BR")
            .RuleFor(x => x.Mensagem, f => f.Lorem.Paragraph())
            .RuleFor(x => x.TipoPendencia, f => (TipoPendenciaEnum)GeraTipoPendenciaEnum())
            .RuleFor(x => x.TipoOrigem, f => f.Random.Int(1, 10) <= 3 ? TipoOrigemEnum.Efetivacao : TipoOrigemEnum.Simulacao)
            .Generate();

            pendencias.Add(pendencia);
        }

        return pendencias;
    }

    public static int GeraTipoBoleta()
    {
        Random random = new();
        int randNum = random.Next(1, 11); // Gera um número entre 1 e 100

        if (randNum <= 2) // 20% de chance
        {
            return 1;
        }
        else if (randNum <= 5) // 30% de chance (21-50)
        {
            return 2;
        }
        else // 50% de chance (51-100)
        {
            return 3;
        }
    }
    public static int GeraTipoPendenciaEnum()
    {
        Random random = new();
        int randNum = random.Next(1, 11);

        if (randNum <= 5)   
        {
            return 1;
        }
        else if (randNum <= 8)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}

public enum TipoPendenciaEnum
{
    Informativa = 1,
    Impeditiva,
    Falha
}

public enum TipoOrigemEnum
{
    Simulacao = 1,
    Efetivacao
}
