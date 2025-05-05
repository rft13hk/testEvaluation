using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

[ExcludeFromCodeCoverage]
public class WebApiModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {

        builder.Services.AddControllers();
        builder.Services.AddHealthChecks();
    }
}

