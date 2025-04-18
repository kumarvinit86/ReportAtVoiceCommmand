using Azure;
using Azure.AI.OpenAI;
using Microsoft.EntityFrameworkCore;
using Reports.Application;
using Reports.External;
using Reports.Persistence;
using Reports.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var model = builder.Configuration.GetValue<string>("OpenAIConfig:model"); ;
var deploymentName = builder.Configuration.GetValue<string>("OpenAIConfig:deploymentName");
var apiKey = builder.Configuration.GetValue<string>("OpenAIConfig:apiKey");
var url = builder.Configuration.GetValue<string>("OpenAIConfig:endpoint");

AzureOpenAIClient azureClient = new(
    new Uri(url),
    new AzureKeyCredential(apiKey));

builder.Services.AddSingleton<AzureOpenAIClient>(azureClient);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IQueryExtractor, QueryExtractor>();
builder.Services.AddScoped<IQueryExecutingManager, QueryExecutingManager>();

builder.Services.AddDbContext<ReportsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins")
    .GetChildren()
    .Select(x => x.Value)
    .ToArray();
    options.AddPolicy("Cors",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("Cors");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
