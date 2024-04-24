using JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();
app.Run();


