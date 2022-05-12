using APIGateway.Aggregators;
using CacheManager.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace APIGateway
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
            .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Action<ConfigurationBuilderCachePart> settings = (x) =>
            {
                x.WithMicrosoftLogging(log =>
                {
                    log.AddConsole(LogLevel.Debug);
                }).WithDictionaryHandle();
            };
            addJwtAuth(services);
            
            services.AddCors(options =>
            {
                options.AddPolicy(
                name: "AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddOcelot(Configuration, settings);//.AddSingletonDefinedAggregator<FlightInventoryDetailsAggregator>(); 
        }
        private void addJwtAuth(IServiceCollection services)
        {
            string validIssuer = "http://localhost:9003";
            string validAudience = "http://localhost:9000";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,

                IssuerSigningKey = secretKey,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience
            };
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("KeyNameAuthTokenValidate", options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
                //options.SaveToken = true;
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateIssuer = false,
                //    ValidateAudience = false,
                //    //ValidateLifetime = false,
                //    ValidateIssuerSigningKey = false,

                //    //ValidIssuer = validIssuer,
                //    //ValidAudience = validAudience,
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                //};
            });

            //services.AddAuthentication()
            //    .AddJwtBearer("TestKey", options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = tokenValidationParameters;
            //    });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowOrigin");
            
            if (env.IsDevelopment())
            {   
                app.UseDeveloperExceptionPage();
            }
            //app.UseAuthentication();//for jwt-- Should not be present in Gateway
            await app.UseOcelot();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

        }
    }
}