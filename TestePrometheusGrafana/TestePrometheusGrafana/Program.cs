using Prometheus;
using Prometheus.HttpMetrics;
using TestePrometheusGrafana;
using TestePrometheusGrafana.Boleta;
using TestePrometheusGrafana.Efetivacao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseHttpClientMetrics();

builder.Services.AddSingleton<IBoletaRepository, BoletaRepository>();
builder.Services.AddSingleton<IEfetivacaoRepository, EfetivacaoRepository>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer(options => options.EnableOpenMetrics = false);
app.UseHttpMetrics();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
