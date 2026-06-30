
using TransactionProcessor.Application.Interfaces;
using TransactionProcessor.Application.Services;
using TransactionProcessor.Infrastructure.Repositories;

namespace TransactionProcessor.Api
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
            builder.Services.AddSingleton<IAccountRepository, AccountRepository>();

            builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

            builder.Services.AddScoped<ITransactionProcessor, TransactionProcessorService>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var accountRepository = scope.ServiceProvider
                    .GetRequiredService<IAccountRepository>();

                var acc001 = accountRepository.Create("ACC-001");
                acc001.Credit(1000m);
                accountRepository.Update(acc001);

                var accVip001 = accountRepository.Create("ACC-VIP-001");
                accVip001.Credit(1000m);
                accountRepository.Update(accVip001);

                var acc002 = accountRepository.Create("ACC-002");
                acc002.Credit(500m);
                accountRepository.Update(acc002);

                var accVip002 = accountRepository.Create("ACC-VIP-002");
                accVip002.Credit(1000m);
                accountRepository.Update(accVip002);

                var acc003 = accountRepository.Create("ACC-003");
                acc003.Credit(0m);
                accountRepository.Update(acc003);
            }

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
