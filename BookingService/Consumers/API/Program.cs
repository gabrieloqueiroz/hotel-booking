using API.Configuration;

string baseUrl = "hotel-booking";
string serviceName = "HotelBooking";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterDependencies(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();


var app = builder.Build();
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwaggerConfiguration(builder.Environment, baseUrl, serviceName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
