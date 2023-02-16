using Amazon;
using Amazon.DynamoDBv2;
using AppServices.Services;
using AppServices.Services.Interfaces;
using DomainModels;
using DomainServices.Interfaces;
using DomainServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentApi.Configurations;

/// <summary>
/// Classe de configuração dos serviços
/// </summary>
public static class ServicesConfiguration
{
    /// <summary>
    /// Realiza injeção de dependencia para as camadas de aplicação e de domínio
    /// </summary>
    /// <param name="services"></param>
    public static void AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IVendaAppService, VendaAppService>();

        services.AddTransient<IVendaService, VendaService>();

        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.KeyName));

        services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
    }
}