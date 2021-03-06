using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Data.Interfaces;
using Bookstore.Data.Models;
using Bookstore.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BookStoreAPI
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
            services.AddTransient<IBookRepository, BookDatabase>();
            services.AddControllers();

            var connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dayob\OneDrive\Documents\Bookstore.mdf;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30";
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connection));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "The Bookstore API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // for dropping and recreating the database
            using(var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BookStoreContext>();
               // context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc), 
            // specifying the Swagger JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                // To serve the Swagger UI at the apps root (http://localhost:<port>/), set the RoutePrefix to an empty string
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
