using Mango.Services.EmailAPI.Data;
using Mango.Services.EmailAPI.Extension;
using Mango.Services.EmailAPI.Messaging;
using Mango.Services.EmailAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IEmailService, EmailService>();

//var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
//optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//builder.Services.AddSingleton(new EmailService(optionBuilder.Options));

//builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
builder.Services.AddHostedService<AzureServiceBusConsumer>();


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
//app.UseAzureServiceBusConsumer();
ApplyMigration(app);

//jStartServiceBusConsumer(app);

app.Run();


void ApplyMigration(WebApplication app)
{
    using(var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }        
    }
}

void StartServiceBusConsumer(WebApplication app)
{
    var consumer = app.Services.GetRequiredService<IAzureServiceBusConsumer>();

    // chạy nền, không block app
    Task.Run(async () =>
    {
        await consumer.Start();
    });
}