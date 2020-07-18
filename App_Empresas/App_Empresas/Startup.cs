using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App_Empresas_Common;
using App_Empresas_Common.Profiles;
using App_Empresas_Repository_Impl;
using App_Empresas_Repository_Impl.Context;
using App_Empresas_Repository_Spec;
using App_Empresas_Services_Impl.Services;
using App_Empresas_Services_Spec;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace App_Empresas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppEmpresaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IEmpresaService, EmpresaService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmpresaProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;

                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc();
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Empresas .NET",
                    Version = "v1",
                    Description = "API de teste de busca de empresas",
                    Contact = new OpenApiContact()
                    {
                        Name = "Stefano Bretas",
                        Email = "stefanobretas@ioasys.com.br",
                        Url = new Uri("https://www.linkedin.com/in/stefano-bretas-dev/")
                    },

                });

                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                var caminhoXml = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                swagger.IncludeXmlComments(caminhoXml);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Empresas .NET v1");
                
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
        }
    }
}
