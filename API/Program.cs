using API.Middleware;
using Application.Commands.CreateVehicleEntry;
using Application.Commands.UpdateVehicleEntry;
using Application.Validators;
using Domain.Contracts.Repositories;
using FluentValidation;
using Infra.Data.Context;
using Infra.Repositories;
using System.Reflection;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona servi�os ao container de inje��o de depend�ncia.
            builder.Services.AddSingleton<IDbContext, DbContext>();
            builder.Services.AddScoped<IVehicleAccessRepository, VehicleAccessRepository>();

            builder.Services.AddTransient<IValidator<UpdateVehicleEntryCommand>, UpdateVehicleEntryValidator>();
            builder.Services.AddTransient<IValidator<CreateVehicleEntryCommand>, CreateVehicleEntryValidator>();

            // Registra os handlers do MediatR a partir do assembly onde est� CreateVehicleEntryHandler
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateVehicleEntryHandler>());

            builder.Services.AddControllers();

            // Configura��o do Swagger/OpenAPI para documenta��o da API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.EnableAnnotations(); 
            });

            var app = builder.Build();

            // Configura o pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExeptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // Define a cultura padr�o como Portugu�s (Brasil)
            var cultureInfo = new System.Globalization.CultureInfo("pt-BR");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.Run();
        }
    }
}
