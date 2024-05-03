using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy", 
        corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod()
                                              .AllowAnyHeader()
                                              .AllowAnyOrigin());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Data_Access_Layer.Data.CMSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CMS"),
builder => builder.EnableRetryOnFailure()));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
