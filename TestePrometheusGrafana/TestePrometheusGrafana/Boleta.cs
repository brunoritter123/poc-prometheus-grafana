using Bogus;

namespace TestePrometheusGrafana;

public class Boleta
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public TipoBoletaEnum TipoBoleta { get; set; }
    public CanalEnum TipoCanal { get; set; }

    public static Boleta CreateFaker()
    {
        return new Faker<Boleta>("pt_BR")
            .RuleFor(x => x.Id, Guid.NewGuid())
            .RuleFor(x => x.Data, f => f.Date.Between(DateTime.Now, DateTime.Now.AddDays(1)))
            .RuleFor(x => x.Valor, f => f.Finance.Amount())
            .RuleFor(x => x.TipoBoleta, f => (TipoBoletaEnum)GeraTipoBoleta())
            .RuleFor(x => x.TipoCanal, f => f.Random.Int(1, 10) <= 3 ? CanalEnum.ION : CanalEnum.IFA)
            .Generate();
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
}

public enum TipoBoletaEnum
{
    Fundos=1,
    RfTerceiro,
    Previdencia
}


public enum CanalEnum
{
    IFA = 1,
    ION
}
