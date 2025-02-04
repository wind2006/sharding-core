using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShardingCore.DbContexts.VirtualDbContexts;
using ShardingCore.Extensions;
using ShardingCore.Test50.MySql.Domain.Entities;


namespace ShardingCore.Test50.MySql
{
/*
* @Author: xjm
* @Description:
* @Date: Friday, 15 January 2021 15:37:46
* @Email: 326308290@qq.com
*/
    public class Startup
    {
        // // 自定义 host 构建
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("Configs/DbConfig.json");
                });
        }

        // 支持的形式：
        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
           
            // services.AddShardingMySql(o =>
            // {
            //     o.EnsureCreatedWithOutShardingTable = false;
            //     o.CreateShardingTableOnStart = false;
            //     // o.AddShardingTable<DefaultDbContext>("conn1", "server=xxx;userid=xxx;password=xxx;database=sharding_db123;Charset=utf8;Allow Zero Datetime=True; Pooling=true; Max Pool Size=512;sslmode=none;Allow User Variables=True;", dbConfig =>
            //     o.UseShardingDbContext<DefaultDbContext>( dbConfig =>
            //     {
            //         dbConfig.AddShardingTableRoute<SysUserModVirtualTableRoute>();
            //         dbConfig.AddShardingTableRoute<SysUserSalaryVirtualTableRoute>();
            //     });
            //     //o.AddDataSourceVirtualRoute<>();
            //     o.IgnoreCreateTableError = true;
            //     o.ServerVersion = new MySqlServerVersion("5.7.15");
            // });
            // services.AddDbContext<DefaultDbContext>(o=>
            //     o.UseMySql(hostBuilderContext.Configuration.GetSection("MySql")["ConnectionString"],new MySqlServerVersion("5.7.15"))
            //         .UseShardingMySqlUpdateSqlGenerator());
        }

        // 可以添加要用到的方法参数，会自动从注册的服务中获取服务实例，类似于 asp.net core 里 Configure 方法
        public void Configure(IServiceProvider serviceProvider)
        {
            var shardingBootstrapper = serviceProvider.GetService<IShardingBootstrapper>();
            shardingBootstrapper.Start();
            // 有一些测试数据要初始化可以放在这里
           InitData(serviceProvider).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private  async Task InitData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var virtualDbContext = scope.ServiceProvider.GetService<DefaultDbContext>();
                
                if (!await virtualDbContext.Set<SysUserMod>().AnyAsync(o => true))
                {
                    var ids = Enumerable.Range(1, 1000);
                    var userMods = new List<SysUserMod>();
                    var userSalaries = new List<SysUserSalary>();
                    var beginTime = new DateTime(2020, 1, 1);
                    var endTime = new DateTime(2021, 12, 1);
                    foreach (var id in ids)
                    {
                        userMods.Add(new SysUserMod()
                        {
                            Id = id.ToString(),
                            Age = id,
                            Name = $"name_{id}",
                            AgeGroup=Math.Abs(id%10)
                        });
                        
                        var tempTime = beginTime;
                        var i = 0;
                        while (tempTime<=endTime)
                        {
                            var dateOfMonth = $@"{tempTime:yyyyMM}";
                            userSalaries.Add(new SysUserSalary()
                            {
                                Id = $@"{id}{dateOfMonth}",
                                UserId = id.ToString(),
                                DateOfMonth = int.Parse(dateOfMonth),
                                Salary = 700000+id*100*i,
                                SalaryLong = 700000+id*100*i,
                                SalaryDecimal = (700000+id*100*i)/100m,
                                SalaryDouble = (700000+id*100*i)/100d,
                                SalaryFloat = (700000+id*100*i)/100f
                            });
                            tempTime=tempTime.AddMonths(1);
                            i++;
                        }
                    }
                
                    await virtualDbContext.AddRangeAsync(userMods);
                    await virtualDbContext.AddRangeAsync(userSalaries);


                    try
                    {

                        await virtualDbContext.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
}