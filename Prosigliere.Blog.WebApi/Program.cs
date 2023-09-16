using Prosigliere.Blog;
using Prosigliere.Blog.Fakes;
using Prosigliere.Blog.WebApi.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddProsigliereBlog();

if (builder.Configuration["Storage"] == "Fake")
    builder.Services.AddProsigliereBlogFakeRepositories();
else
    builder.Services.AddProsigliereBlogEntityFrameworkRepositories(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();