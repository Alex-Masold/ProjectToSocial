using ProjectToSocial.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Role admin = new() {Id=Convert.ToInt32(Guid.NewGuid()), Name="Admin"};
Role teamLider = new() {Id=Convert.ToInt32(Guid.NewGuid()), Name="Team Lider"};
Role user = new() {Id=Convert.ToInt32(Guid.NewGuid()), Name="user"};

List<User> users = new List<User> 
{ 
    new() { Id=Convert.ToInt32(Guid.NewGuid()), FirstName="User1", LastName="1Resu", Family="Usering", Email="user1@gmail.com", Pessword="123", Role=user, RoleId=user.Id},
    new() { Id=Convert.ToInt32(Guid.NewGuid()), FirstName="User1", LastName="1Resu", Family="Usering", Email="user1@gmail.com", Pessword="123", Role=user, RoleId=user.Id},
    new() { Id=Convert.ToInt32(Guid.NewGuid()), FirstName="User1", LastName="1Resu", Family="Usering", Email="user1@gmail.com", Pessword="123", Role=user, RoleId=user.Id},
    new() { Id=Convert.ToInt32(Guid.NewGuid()), FirstName="User1", LastName="1Resu", Family="Usering", Email="user1@gmail.com", Pessword="123", Role=user, RoleId=user.Id},
    new() { Id=Convert.ToInt32(Guid.NewGuid()), FirstName="User1", LastName="1Resu", Family="Usering", Email="user1@gmail.com", Pessword="123", Role=user, RoleId=user.Id},
};

app.MapGet("/user/{id}", () =>
{

});

app.MapFallbackToFile("/index.html");

app.Run();
