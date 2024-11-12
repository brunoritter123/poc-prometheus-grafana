namespace TestePrometheusGrafana.Boleta;

public class BoletaRepository : IBoletaRepository
{
    private const string ConnectionString = "Server=localhost:3306;Database=poc-prometheus-grafana;User ID=mysql;Password=123456;";
    private readonly ICollection<BoletaModel> _boletas;
    private readonly MetricsBoletaPrometheus metricsBoleta = new();
    public BoletaRepository()
    {
        _boletas = [];
    }
    public void Inserir(BoletaModel boleta)
    {
        using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
        {
            // Abre a conexão
            dbConnection.Open();

            // Exemplo de uma consulta usando Dapper
            string query = "SELECT * FROM boleta";
            IEnumerable<dynamic> resultado = dbConnection.Query(query);

            // Exibe os resultados
            foreach (var item in resultado)
            {
                Console.WriteLine(item);
            }
        }
        metricsBoleta.MetrificarBoletaCriada(boleta.TipoBoleta, boleta.TipoCanal, boleta.Valor);
        _boletas.Add(boleta);
    }

    public IEnumerable<BoletaModel> Get()
        => _boletas;
}


public interface IBoletaRepository
{
    public void Inserir(BoletaModel boleta);

    public IEnumerable<BoletaModel> Get();

}