using Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Startup
    {
        public static void AddAplicationCore(this IServiceCollection services)
        {
            services.AddScoped<UsuarioService>();
            services.AddScoped<PetService>();
            services.AddScoped<AgendamentoService>();
            services.AddScoped<ProntuarioService>();
            services.AddScoped<ExameService>();
            services.AddScoped<MedicamentoService>();

        }
    }
}
