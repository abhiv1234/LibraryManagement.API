using LibraryManagement.API.Services;
using LibraryManagement.API.Data;
using LibraryManagement.API.Middlewares;
using LibraryManagement.API.Repositories.Implementations;
using LibraryManagement.API.Repositories.Interfaces;
using LibraryManagement.API.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//=================================
// 1. Register Services
//=================================

// Add Controllers
builder.Services.AddControllers();

//Register Business Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberService, MemberService>();

//Register Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

// Register DbContext
// Code

// Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//=================================
// 2. Build App
//=================================

var app = builder.Build();

//=================================
// 3. Configure Middleware Pipeline
//=================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handling Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();