using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Neofilia.DAL;

namespace Neofilia.BOT
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NeofiliaDbContext>(options =>
            {
                //Could move it to firebase
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NeofiliaDbContext;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("Neofilia.DAL.Migrations"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //services.AddScoped<IQuestionService, IQuestionService>();

            var serviceProvider = services.BuildServiceProvider();

            var bot = new NeofiliaBot(serviceProvider);
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
