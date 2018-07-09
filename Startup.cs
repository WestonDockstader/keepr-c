﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using keepr_c.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;


namespace keepr_c
{
  public class Startup
  {
		private readonly string _connectionString;
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
			_connectionString=configuration.GetSection("DB").GetValue<string>("MySQLConnectionString");
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
			{
				options.LoginPath="/Account/Login/";
				options.Events.OnRedirectToLogin=(context)=>
				{
					context.Response.StatusCode=401;
					return Task.CompletedTask;
				};
			});
			services.AddCors(Options=>
			{
				Options.AddPolicy("ANYORIGIN", builder=>
				{
					builder
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials();
				});
			});
			services.AddMvc();
			services.AddTransient<IDbConnection>(x=>CreateDBContext());
			services.AddTransient<UserRepository>();
    }

		private IDbConnection CreateDBContext()
		{
			var connection = new MySqlConnection(_connectionString);
			connection.Open();
			return connection;
		}

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseMvc();
    }
  }
}