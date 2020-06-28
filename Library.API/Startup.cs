using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Library.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.API
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
            services.AddControllers();
            services.AddScoped<IAuthorRepository, AuthorMockRepository>();
            services.AddScoped<IBookRepository, BookMockRepository>();
            services.AddSwaggerGen(action =>
            {
                action.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"图书管理系统",
                    Version = "v2",
                    Description = "图书管理系统",
                });
                //获取编译的xml文件
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //获取路径解析器解析的文档路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //解析文档
                action.IncludeXmlComments(xmlPath);
                action.IgnoreObsoleteActions();
                action.IgnoreObsoleteProperties();
                //是否启用文档注释
                action.EnableAnnotations(true);
                //可以添加自己支持的数据
                //action.DocumentFilter<>();
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
            //swagger中间件在路由之前添加,在swagger前一定加反斜杠
            app.UseSwagger();
            app.UseSwaggerUI(action =>
            {
                action.SwaggerEndpoint($"/swagger/v2/swagger.json", "v2");
                action.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
