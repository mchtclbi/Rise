using Hangfire;
using System.Text;
using Hangfire.SqlServer;
using Space.DataAPI.Data.Concretes;
using Microsoft.EntityFrameworkCore;
using Space.DataAPI.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Space.DataAPI.Services.Concretes;
using Space.DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
    };
});

builder.Services.AddHangfire(hangfire =>
{
    hangfire.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
    hangfire.UseSimpleAssemblyNameTypeSerializer();
    hangfire.UseRecommendedSerializerSettings();
    hangfire.UseColouredConsoleLogProvider();
    hangfire.UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    });

    var server = new BackgroundJobServer(new BackgroundJobServerOptions
    {
        ServerName = "DefaultServer",
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IScheduleTask, ScheduleTask>();
builder.Services.AddScoped<IPriceHistoryService, PriceHistoryService>();
builder.Services.AddScoped<IPriceHistoryRepository, PriceHistoryRepository>();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");

BackgroundJob.Enqueue(() => Console.WriteLine("Hangfire started..."));
RecurringJob.AddOrUpdate<IScheduleTask>("GetPrice", q => q.AddPriceHistory(), Cron.Minutely);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();