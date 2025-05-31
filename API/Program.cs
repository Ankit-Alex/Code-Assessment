using Core;
using Core.Configuration;
using DataAccess;


var builder = WebApplication.CreateBuilder(args);

//Adding all controllers.
builder.Services.AddControllers();
//Adding services defined at the Data Access Layer.
builder.Services.AddDataAccessServices(builder.Configuration);
//Adding services defined at the Core Layer.
builder.Services.AddCoreServices();

//Api explorer to expose the API endpoints created in controllers.
builder.Services.AddEndpointsApiExplorer();
//Integrating OpenAPI/Swagger to the application.
builder.Services.AddSwaggerGen();

//Configuring options for Product Id Generator.
builder.Services.Configure<DistributedSystemOptions>(
    builder.Configuration.GetSection(DistributedSystemOptions.ConfigurationKey)
);

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();              
app.MapControllers();          

app.Run();
