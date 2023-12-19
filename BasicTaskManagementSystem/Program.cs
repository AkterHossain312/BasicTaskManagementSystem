using Application.Helper;
using Application.Mapping;


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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMediatR(x=>x.RegisterServicesFromAssembly(typeof(RegisterApplication).Assembly));


builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseCors(CorsPolicy);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
