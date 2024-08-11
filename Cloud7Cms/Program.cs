global using Common.Extensions;
using Cloud7Cms.Configuration;
using Cloud7Cms.DapperConfig;

var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<PaymentGrameenphoneDapperContext>();
builder.Services.Configure<DbConnections>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
