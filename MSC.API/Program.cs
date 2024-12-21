
using Microsoft.EntityFrameworkCore;
using MSC.Core.IoC;
using MSC.Core.Mapper;
using MSC.Data.DatabseContext;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);


var modelProject = "MSC.Core.xml";
var modelProjectPath = Path.Combine(AppContext.BaseDirectory, modelProject);
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(modelProjectPath);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(MyMapper));
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSCConnectionString"));
});

builder.Services.Injector();
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


