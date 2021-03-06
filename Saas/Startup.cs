﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Saas.Entities;

namespace Saas
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
      services.AddControllers();
      services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));

      services.AddSingleton<AppData>();  
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

      app.UseCors("MyPolicy");

      app.UseEndpoints((Action<IEndpointRouteBuilder>)(endpoints =>
      {
        endpoints.MapGrpcService<Services.AppDataService>();
        endpoints.MapGrpcService<Services.RestaurantService>();
        endpoints.MapGrpcService<Services.TableService>();
        endpoints.MapGrpcService<Services.ItemService>();
        endpoints.MapGrpcService<Services.RestaurantMenuService>();
        endpoints.MapGrpcService<Services.MenuItemService>();

        endpoints.MapGet("/", async context =>
        {
          await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });

        endpoints.MapControllers();
      }));
    }
  }
}