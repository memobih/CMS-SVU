using CMS_back.Data;
using CMS_back.Reposatory.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy",
        corsPolicyBuilder => corsPolicyBuilder
                                              .AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();

// connect with database
builder.Services.AddDbContext<CMSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CMS"),
builder => builder.EnableRetryOnFailure()));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CMSContext>();


//[Authoriz] used JWT Token in Chck Authantiaction
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    string jwtSecret = builder.Configuration.GetSection("JWT:Secret").Value;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSecret))
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS-back", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS-back v1"));
}

app.UseStaticFiles();
app.UseCors("MyPolicy");
app.UseRouting();

app.UseAuthentication();//Check JWT token

app.UseAuthorization();

app.MapControllers();

app.Run();
