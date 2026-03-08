using PhoneCompany.API.Extensions;
using PhoneCompany.Application.Extensions;
using PhoneCompany.Application.Options;
using PhoneCompany.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<RefreshTokenOptions>(configuration
    .GetSection(nameof(RefreshTokenOptions)));

services.Configure<AccessTokenOptions>(configuration
    .GetSection(nameof(AccessTokenOptions)));

services.AddJwtAuthentication(configuration);

services.AddApplicationLayer();

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddInfrastructure(configuration);

services.AddCustomCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
