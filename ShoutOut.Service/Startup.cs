﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoutOut.Data.Stores;
using ShoutOut.Domain.Repositories;
using System;
using System.IO;
using System.Reflection;

namespace ShoutOut
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
			//services.AddMemoryCache();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddSingleton<IPostRepository, PostStore>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
				{
					Title = "ShoutOut - Message board",
					Version = "v1",
					Description = "A simple message board HTTP API.",
					TermsOfService = "The terms of the service"
				});

				// Get xml comments path
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				// Set xml path
				options.IncludeXmlComments(xmlPath);
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger()
					.UseSwaggerUI(c =>
					{
						//c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoutOut API v1");
						c.SwaggerEndpoint(Configuration["SwaggerEndpoint"], Configuration["SwaggerEndpointName"]);
					});
			}

			app.UseMvc();
		}
	}
}