using TwitterAPI.Database;
using TwitterAPI.Entities;
using TwitterAPI.PostsManager;

var builder = WebApplication.CreateBuilder(args);
var usersList = new List<Users>();
var postsList = new List<Posts>();

// Add services to the container.
builder.Services.AddSingleton<UserDatabase>();
builder.Services.AddSingleton<PostsDatabase>();
builder.Services.AddSingleton(new PostsManager(usersList, postsList));

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
