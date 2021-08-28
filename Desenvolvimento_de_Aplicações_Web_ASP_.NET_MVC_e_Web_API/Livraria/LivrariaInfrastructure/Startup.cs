using LivrariaCore.Repositorio;
using LivrariaInfrastructure.EntityFramework;
using LivrariaInfrastructure.EntityFramework.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaInfrastructure
{
    public static class Startup
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BancoDeDados>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepositorioAutor, RepositorioAutor>();
            services.AddScoped<IRepositorioLivro, RepositorioLivro>();
        }
    }
}
