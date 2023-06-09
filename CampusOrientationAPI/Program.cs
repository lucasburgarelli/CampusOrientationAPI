using CampusOrientationAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cnString = builder.Configuration.GetConnectionString("postgres");
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<CampusOrientationDBContext>(options =>
    options.UseNpgsql(cnString));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.WithMethods("*");
    c.WithHeaders("*");
    c.WithOrigins("*");
});
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
