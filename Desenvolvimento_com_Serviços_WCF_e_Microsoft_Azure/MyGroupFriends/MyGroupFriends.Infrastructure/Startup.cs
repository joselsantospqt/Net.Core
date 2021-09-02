using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyGroupFriends.Infrastructure.EntityFramework;
using MyGroupFriends.Infrastructure.EntityFramework.Repositorio;
using MyGroupFriends.Domain.IRepositorio;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGroupFriends.Infrastructure
{
    public static class Startup
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString) {
            services.AddDbContext<BancoDeDados>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MyGroupFriends.Infrastructure")));
            //services.AddDbContext<BancoDeDados>(options => options.UseInMemoryDatabase(connectionString));
            services.AddScoped<IRepositorioAmigo, RepositorioAmigo>();
        }
    }
}
