
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VHS_Tarefas.Data;
using VHS_Tarefas.Migrations;
using VHS_Tarefas.Repositories;
using VHS_Tarefas.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VHS_Tarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connection = builder.Configuration.GetConnectionString("DataBase");
            builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connection));

            // Configuração do serviço
            var serviceProvider = CreateServices();

            // Realiza a migração
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            builder.Services.AddScoped<IChannelService, ChannelService>();
            builder.Services.AddScoped<MessageService>();
            builder.Services.AddScoped<ChatContextService>();
            builder.Services.AddScoped<ContactChannelService>();

            builder.Services.AddScoped<ChannelRepository>();
            builder.Services.AddScoped<MessageRepository>();
            builder.Services.AddScoped<ChatContextRepository>();
            builder.Services.AddScoped<ContactChannelRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static IServiceProvider CreateServices()
        {
            //AddFluentMigratorCore()
                //.AddSingleton<IConventionSet>(new DefaultConventionSet("chat", null))
                //.ConfigureRunner(rb => rb
                 //   .AddPostgres11_0()
                 //   .WithGlobalConnectionString(connection)
                //    .ScanIn(typeof(Migration_202303301830).Assembly).For.Migrations())
               // .AddLogging(lb => lb.AddFluentMigratorConsole());

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString("Server=localhost;Port=5432;Database=vhs_tasks;User Id=postgres;Password=123456;")
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}