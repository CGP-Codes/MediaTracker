using MediaTracker.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connection, option => {
        option.EnableRetryOnFailure(); 
    })
); // Or specific error numbers to retry


var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

    if (!dbContext.Database.CanConnect()) 
    {
        throw new NotImplementedException("Can't connect to DB");
    }
}

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

app.MapFallbackToFile("/index.html");

app.Run();








/*
using (var context = new LibraryDbContext()) 
{
    try
    {
        context.Database.EnsureCreated();

        var srs1 = new Series() { Name = "Longhorn/Chee", Description = "American Indian Mystery", IsCompleted = true, Length = 20 };
        var bk1 = new Book() { AuthorFirst = "Tony", AuthorLast = "Hillerman", ISBN = "1234", Name = "Sacred Clown", PublicationDate = "January 2010", Publisher = "Random House" };

        context.Books.Add(bk1);

        context.SaveChanges();

        foreach (var s in context.Books)
        {
            Console.WriteLine($"Title: {s.Name}, Author First Name: {s.AuthorFirst}, Author Last Name: {s.AuthorLast}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}


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

app.MapFallbackToFile("/index.html");

app.Run();
*/