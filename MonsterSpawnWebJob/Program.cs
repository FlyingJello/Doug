﻿using System;
using Doug;
using Doug.Models;
using Doug.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MonsterSpawnWebJob
{
    class Program
    {
        static void Main()
        {
            var connectionString = Environment.GetEnvironmentVariable("dougbotdb");

            var services = new ServiceCollection();
            services.AddDbContext<DougContext>(options =>
                options.UseSqlServer(connectionString ?? throw new InvalidOperationException()));

            Startup.RegisterDougServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var monsterService = serviceProvider.GetService<IMonsterService>();

            monsterService.RollMonsterSpawn();
        }
    }
}