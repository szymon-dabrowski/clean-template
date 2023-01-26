using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Modules;
using Clean.Web.Api.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddModules();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.DontUseDefaultJwtClaimTypeMap();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseEndpointsModules();

app.UseModules();

app.Run();