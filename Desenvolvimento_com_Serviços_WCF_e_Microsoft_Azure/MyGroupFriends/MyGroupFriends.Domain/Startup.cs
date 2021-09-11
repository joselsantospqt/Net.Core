using Microsoft.Extensions.DependencyInjection;
using MyGroupFriends.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGroupFriends.Domain
{
    public static class Startup
    {
        public static void AddAplicationDomain(this IServiceCollection services) {
            services.AddScoped<AmigoService>();
        }
    }
}
