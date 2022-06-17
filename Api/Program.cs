using Api.Models;
using Api.Services;


// DotEnv config
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<KindSocialDatabaseSettings>(builder.Configuration.GetSection("KindSocialDatabase"));
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<PostsService>();
builder.Services.AddSingleton<CommentsService>();
builder.Services.AddSingleton<MessagesService>();
builder.Services.AddControllers();
builder.Services.AddCors( o => {
  o.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
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

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
