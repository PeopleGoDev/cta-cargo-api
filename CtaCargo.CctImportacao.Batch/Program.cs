using CtaCargo.CctImportacao.Batch.Services;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<FunctionTimerService>();
        services.AddScoped<IConfiguraRepository, SQLConfiguraRepository>();
        services.AddScoped<IVooRepository, SQLVooRepository>();
        services.AddScoped<IPortoIATARepository, SQLPortoIATARepository>();
        services.AddScoped<ImportFlightXMLService>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("SQLConnectionString"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });
    })
    .Build();

host.Run();
