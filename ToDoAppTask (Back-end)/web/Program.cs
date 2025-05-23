using AGI.Morn.Application;
using AGI.Morn.Infrastructure;
using Morn_Agi;
using Morn_Agi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Dbcontext

builder.Services.AddInfrastrctureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.AddApplicationServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;

        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter(
            namingPolicy: null, 
            allowIntegerValues: true 
        ));
    });

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter(
        namingPolicy: null,
        allowIntegerValues: true
    ));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomizedSwagger(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseExceptionHandler(options => { });
app.UseCustomizedSwagger(app.Environment);
app.MapEndpoints();
app.UseHttpsRedirection();
app.MapGet("/debug/routes", (HttpContext context) =>
{
    var endpointDataSource = context.RequestServices.GetRequiredService<EndpointDataSource>();
    var routeInfos = endpointDataSource.Endpoints.Select(e => e.DisplayName).ToList();

    return Results.Ok(routeInfos);
});


app.Run();
public partial class Program { }




