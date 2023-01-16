using Clean.Web.Api.Common.Controllers.Errors;
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

app.UseExceptionHandler(ErrorsController.Route);

app.UseHttpsRedirection();

app.DontUseDefaultJwtClaimTypeMap();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseModules();

app.Run();