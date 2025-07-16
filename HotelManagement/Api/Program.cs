using Api.Interfaces;
using Api.Repositories;
using Api.Services;
using Api.Utilities;
using Api.Utilities.Middlewares;
using Api.Utilities.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.Configure<HotelSearchWeights>(builder.Configuration.GetSection("HotelSearchWeights"));
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>(); 
});

var app = builder.Build();

app.ConfigureExceptionHandler(app.Logger);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();