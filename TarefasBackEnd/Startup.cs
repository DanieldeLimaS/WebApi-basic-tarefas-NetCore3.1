using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();//defini que iremos usar controllers
            var key = Encoding.ASCII.GetBytes("IssoÉUmTokenGrandeParaTestarAChaveUnica_ÉRecomendadoSalvarOTokenEmUmArquivoExternoOuArquivoDeConfiguracao");

            //o meu serviço de autorização serão baseados em JSON
            services.AddAuthentication(options => {
                //definindo o JWT como o scheme de autenticacao do sistema
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;//não estou obrigando a usar o https
                    options.SaveToken = true;//permite salvar o token
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //definindo o  DataContext e criando o banco de dados em memoria
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BDTarefas"));

            //definindo serviço que vai permitir a manipulação do repositorio de tarefas na controller
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
