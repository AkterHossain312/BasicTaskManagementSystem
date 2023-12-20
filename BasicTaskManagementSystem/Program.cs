using Application.Helper;
using Application.Mapping;
using BasicTaskManagementSystem.Extensions;
using Dependency;
using System.Configuration;
using WebApi.Constants;
using WebApi.DependencieRegister;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var CorsPolicy = "CorsPolicy";

builder.Services.AddCors(feature =>
    feature.AddPolicy(
        CorsPolicy,
        apiPolicy => apiPolicy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    ));

builder.Services.AddAllRegisterDependencies(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddIdentityOptions();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMediatR(x=>x.RegisterServicesFromAssembly(typeof(RegisterApplication).Assembly));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();

builder.Services
               .RegisterDbContext(builder.Configuration)
               .AddServices();            
               

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint(ConfigurationConstants.SwaggerUrl, ConfigurationConstants.SwaggerName));
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseCors(CorsPolicy);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
