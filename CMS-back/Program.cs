using CMS_back.Data;
using CMS_back.Mailing;
using CMS_back.Mapper;
using CMS_back.Models;
using CMS_back.GenericRepository;
using CMS_back.IGenericRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using CMS_back.Interfaces;
using CMS_back.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy",
        corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();

// connect with database
builder.Services.AddDbContext<CMSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CMS"),
builder => builder.EnableRetryOnFailure()));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
    AddEntityFrameworkStores<CMSContext>();
builder.Services.AddScoped<IMailingService, MailingService>();
builder.Services.AddScoped<IControlRepository, ControlRepository>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("Mailing"));
builder.Services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);
builder.Services.AddAutoMapper(typeof(MappingProfile));

//[Authoriz] used JWT Token in Chck Authantiaction
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
}
    ).AddJwtBearer(o =>
    {
        o.IncludeErrorDetails = true;
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password Settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 2;
    options.Password.RequireNonAlphanumeric = false;

    //Lockout Settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //User Settings
    options.User.AllowedUserNameCharacters =
     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

});

#region swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS-back", Version = "v1" });
});
builder.Services.AddSwaggerGen(swagger =>
{
    //This?is?to?generate?the?Default?UI?of?Swagger?Documentation????
    swagger.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET?5?Web?API",
        Description = " ITI Projrcy"
    });

    //?To?Enable?authorization?using?Swagger?(JWT)????
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter?'Bearer'?[space]?and?then?your?valid?token?in?the?text?input?below.\r\n\r\nExample:?\"Bearer?eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                });
});
#endregion

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
