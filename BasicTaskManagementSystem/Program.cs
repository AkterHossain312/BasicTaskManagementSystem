using Application.Helper;
using Application.Mapping;
using BasicTaskManagementSystem.Extensions;
using Dependency;
using System.Configuration;
using Infrastructure.Constant;
using WebApi.Constants;
using WebApi.DependencieRegister;
using WebApi.Extensions;
using WebApi.MIddleware;

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
builder.Services.AddSwaggerConfiguration();
builder.Services.AddIdentityOptions();
builder.Services.AddControllers();
builder.Services.AddAuthorization(opt =>
{
    //opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    //opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
});
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

app.UseMiddleware<ManageUserMiddleware>();
app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseCors(CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
