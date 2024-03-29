using SpyDuh_Timber_Wolves.Repositories;

namespace SpyDuh_Timber_Wolves
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<ISpyRepository, SpyRepository>();
            builder.Services.AddTransient<ISpyServicesRepository, SpyServicesRepository>();
            builder.Services.AddTransient<ISpySkillsRepository, SpySkillsRepository>();
            builder.Services.AddTransient<IEnemyRepository, EnemyRepository>();
            builder.Services.AddTransient<IFriendRepository, FriendRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
    }
}