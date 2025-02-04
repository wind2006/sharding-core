using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sample.MySql
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
            // services.AddShardingMySql(o =>
            // {
            //     o.EnsureCreatedWithOutShardingTable = true;
            //     o.CreateShardingTableOnStart = true;
            //     o.UseShardingDbContext<DefaultTableDbContext>( dbConfig =>
            //     {
            //         dbConfig.AddShardingTableRoute<SysUserModVirtualTableRoute>();
            //         dbConfig.AddShardingTableRoute<SysUserLogByMonthRoute>();
            //     });
            //     //o.AddDataSourceVirtualRoute<>();
            //     o.IgnoreCreateTableError = true;
            //
            // });
            // services.AddDbContext<DefaultTableDbContext>(o => o.UseMySql("server=xxx;userid=xxx;password=xxx;database=sharding_db123;Charset=utf8;Allow Zero Datetime=True; Pooling=true; Max Pool Size=512;sslmode=none;Allow User Variables=True;", o =>
            // {
            //     o.ServerVersion("5.7.13");
            // }).UseShardingMySqlUpdateSqlGenerator());
            // services.AddLogging(b => b.AddConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseShardingCore();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.DbSeed();
        }
    }
}
