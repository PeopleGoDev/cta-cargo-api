using System;
using System.Reflection;
using AutoMapper;
using CtaCargo.CctImportacao.Api.Infrastructure.Extensions;
using CtaCargo.CctImportacao.Api.Infrastructure.Middlewares;
using CtaCargo.CctImportacao.Application.Services;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Application.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CtaCargo.CctImportacao.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddServicesInAssembly(Configuration);

        services.AddMvc()
            .AddJsonOptions(x =>
           {
               x.JsonSerializerOptions.IgnoreNullValues = true;
               x.JsonSerializerOptions.PropertyNamingPolicy = null;
           });

        services.AddControllers()
            .AddJsonOptions(options => { 
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICiaAereaService, CiaAereaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IVooService, VooService>();
        services.AddScoped<IMasterService, MasterService>();
        services.AddScoped<IHouseService, HouseService>();
        services.AddScoped<IPortoIataService, PortoIATAService>();
        services.AddScoped<IUploadService, UploadService>();
        services.AddScoped<ISubmeterReceitaService, SubmeterReceitaService>();
        services.AddScoped<IUldMasterService, UldMasterService>();
        services.AddScoped<ICertificadoDigitalService, CertificadoDigitalService>();
        services.AddScoped<INaturezaCargaService, NaturezaCargaService>();
        services.AddScoped<IAgenteDeCargaService, AgenteDeCargaService>();
        services.AddScoped<INcmService, NcmService>();

        // Repositorios
        services.AddScoped<IUsuarioRepository, SQLUsuarioRepository>();
        services.AddScoped<ICiaAereaRepository, SQLCiaAereaRepository>();
        services.AddScoped<IVooRepository, SQLVooRepository>();
        services.AddScoped<IPortoIATARepository, SQLPortoIATARepository>();
        services.AddScoped<IMasterRepository, SqlMasterRepository>();
        services.AddScoped<IHouseRepository, SqlHouseRepository>();
        services.AddScoped<ICertificadoDigitalRepository, SQLCertificadoDigitalRepository>();
        services.AddScoped<IUldMasterRepository, SQLUldMasterRepository>();
        services.AddScoped<IErroMasterRepository, SQLErroMasterRepository>();
        services.AddScoped<INaturezaCargaRepository, SQLNaturezaCargaRepository>();
        services.AddScoped<IAgenteDeCargaRepository, SQLAgenteDeCargaRepository>();
        services.AddScoped<INcmRepository, SQLNcmRepository>();
        services.AddScoped<IMasterHouseAssociacaoRepository, SQLMasterHouseAssociacaoRepository>();

        services.AddScoped<ICertitificadoDigitalSupport, CertitificadoDigitalSupport>();
        services.AddScoped<IDownloadArquivoCertificado, DownloadArquivoCertificadoAzureStorage>();
        services.AddScoped<IAutenticaReceitaFederal, AutenticaReceitaFederal>();
        services.AddScoped<IUploadReceitaFederal, FlightUploadReceitaFederal>();
        services.AddScoped<IMotorIata, MotorIata>();
        services.AddScoped<ISendEmail, SendEmail>();
        services.AddScoped<IValidadorMaster, ValidadorMaster>(); 
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment != "Production")
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CTA Cargo - CCT Importacao API v1");
            });
        };

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
