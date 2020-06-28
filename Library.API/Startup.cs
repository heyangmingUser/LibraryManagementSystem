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
                    Title = $"ͼ�����ϵͳ",
                    Version = "v2",
                    Description = "ͼ�����ϵͳ",
                });
                //��ȡ�����xml�ļ�
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //��ȡ·���������������ĵ�·��
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //�����ĵ�
                action.IncludeXmlComments(xmlPath);
                action.IgnoreObsoleteActions();
                action.IgnoreObsoleteProperties();
                //�Ƿ������ĵ�ע��
                action.EnableAnnotations(true);
                //��������Լ�֧�ֵ�����
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
            //swagger�м����·��֮ǰ���,��swaggerǰһ���ӷ�б��
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
