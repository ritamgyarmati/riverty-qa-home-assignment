using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CardValidation;

namespace CardValidation.IntegrationTests.Helpers
{
    internal class WebAppFactory : WebApplicationFactory<Program>
    {
    }
}
