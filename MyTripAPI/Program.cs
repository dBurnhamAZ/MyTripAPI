using Microsoft.EntityFrameworkCore;
using MyTripAPI.Logging;
using MyTripAPI;
using MyTripAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add SQL Db connection string
builder.Services.AddDbContext<MyTripAPIDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// Add Singleton Pattern here for ILogging
builder.Services.AddSingleton<ILogging, LoggingWFlag>();

// Add AutoMapping
builder.Services.AddAutoMapper(typeof(MappingConfig)); 

builder.Services.AddControllers(option =>
{
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();


// Add services to the container.
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
