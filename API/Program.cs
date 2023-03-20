using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<APIContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext") ?? throw new InvalidOperationException("Connection string 'APIContext' not found.")));

// Add services to the container.
builder.Services.AddMvc();  
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(p => p.AddPolicy("corsapp", corsBuilder =>
//{
//    corsBuilder.SetIsOriginAllowed(url => {
//        var host = new Uri(url).Host;

//        return host.Equals("localhost") || host.Equals("[OMGEVING SEM].hbo-ict.org");
//    })
//    .WithMethods("GET", "POST", "PUT")
//    .AllowAnyHeader();
//}));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader()
        .SetIsOriginAllowed((host) => true));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
