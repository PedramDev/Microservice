using Autofac;
using Autofac.Extensions.DependencyInjection;
using Catalog.Data;
using Catalog.Infrastructure;
using Catalog.Repository;

var builder = WebApplication.CreateBuilder(args);

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title="Catalog.API", Version = "v1" });
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
    containerBuilder.RegisterModule(new RepositoryModule(builder.Environment.EnvironmentName == "Development"));
    containerBuilder.RegisterModule(new DataModule(builder.Environment.EnvironmentName == "Development"));
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Catalog.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
