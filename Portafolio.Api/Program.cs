using Portafolio.Api.Services;
using Portafolio.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDataBase"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder services
builder.Services.AddScoped<ProyectosServices>();
//CORS RULE
builder.Services.AddCors(options => options.AddPolicy("AnyClient", policy =>
{
    policy.WithOrigins()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AnyClient");

app.UseAuthorization();


app.MapControllers();


app.Run();
