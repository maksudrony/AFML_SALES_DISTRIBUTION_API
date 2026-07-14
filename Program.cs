using System.Text;
using AFML_SALES_DISTRIBUTION_API.Interfaces;
using AFML_SALES_DISTRIBUTION_API.Interfaces.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Repositories;
using AFML_SALES_DISTRIBUTION_API.Repositories.Do_LiftingReport;
using AFML_SALES_DISTRIBUTION_API.Services;
using AFML_SALES_DISTRIBUTION_API.Services.Do_LiftingReport;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services controls parameters
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Cross Origin resource sharing policies
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader());
});

// Register Security interceptors JWT Bearer and core Token Validations 
var jwtKey = builder.Configuration["Jwt:Key"]!;
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Register Scoped Abstractions matching interfaces exactly
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICommonParameterRepository, CommonParameterRepository>();
builder.Services.AddScoped<ICommonParameterService, CommonParameterService>();

// IMS Report Menu
builder.Services.AddScoped<ISummaryImsReportRepository, SummaryImsReportRepository>();
builder.Services.AddScoped<ISummaryImsReportService, SummaryImsReportService>();

// DO And Lifting Report Menu
builder.Services.AddScoped<IDayWiseDelRptService, DayWiseDelRptService>();
builder.Services.AddScoped<IDayWiseDelRptRepository, DayWiseDelRptRepository>();

builder.Services.AddScoped<ILiftingAndDoReportRepository, LiftingAndDoReportRepository>();
builder.Services.AddScoped<ILiftingAndDoReportService, LiftingAndDoReportService>();

builder.Services.AddScoped<IAverageRateRptRepository, AverageRateRptRepository>();
builder.Services.AddScoped<IAverageRateRptService, AverageRateRptService>();

builder.Services.AddScoped<IProductWiseDeliveryReportRepository, ProductWiseDeliveryReportRepository>();
builder.Services.AddScoped<IProductWiseDeliveryReportService, ProductWiseDeliveryReportService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

// Execution sequences 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();