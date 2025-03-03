using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddHttpClient();
builder.Services.AddHangfireServer();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<CurrencyService>(
    "fetch-currency-rate",
    service => service.FetchCurrencyRate(),
    "*/5 * * * *", // кожні 5 хвилин
    new RecurringJobOptions()
    {
        MisfireHandling = MisfireHandlingMode.Relaxed // За замовчуванням. У цьому режимі, незалежно від кількості пропущених запусків, буде створено лише одне фонове завдання. Параметр “Time” для цього завдання вказуватиме на час, коли завдання було заплановано.
        // MisfireHandlingMode.Strict // У цьому режимі для кожного пропущеного запуску буде створено окреме фонове завдання. Параметр “Time” для кожного завдання буде відповідати часу, на який воно було заплановано.
        // MisfireHandlingMode.Ignorable // У цьому режимі, незалежно від кількості пропущених запусків, жодні фонові завдання не будуть створені.
    }
);

app.Run();
