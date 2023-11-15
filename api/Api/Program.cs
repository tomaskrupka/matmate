using MatMate.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var sqliteConnectionString = builder.Configuration.GetConnectionString("Sqlite");

builder.Services.AddControllers();
builder.Services.AddDbContext<MatMateDbContext>(options => options.UseSqlite(sqliteConnectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MatMateDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();