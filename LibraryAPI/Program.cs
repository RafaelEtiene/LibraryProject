using LibraryAPI.Repositories;
using LibraryAPI.Repositories.Interfaces;
using LibraryAPI.Services;
using LibraryAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ILoanService, LoanService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<ILoanRepository, LoanRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IClientService, ClientService>();

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
