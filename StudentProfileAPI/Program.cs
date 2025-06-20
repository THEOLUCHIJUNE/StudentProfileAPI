using StudentProfileAPI.Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;
using StudentProfileAPI.Application.Interfaces; 
using StudentProfileAPI.Infrastructure.Persistence; 
using StudentProfileAPI.Application.Features.Queries; 
using StudentProfileAPI.Application.Features.Commands; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext 
builder.Services.AddDbContext<StudentProfileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services with Dependency Injection
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();

// Register Use Cases (Queries and Commands) as services
// STUDENT Commands and Queries
builder.Services.AddScoped<GetStudentsQuery>();
builder.Services.AddScoped<GetStudentByIdQuery>();
builder.Services.AddScoped<GetStudentByMatricNumberQuery>();
builder.Services.AddScoped<GetStudentsByStateOfOriginQuery>();
builder.Services.AddScoped<CreateStudentCommand>();
builder.Services.AddScoped<UpdateStudentCommand>();
builder.Services.AddScoped<DeleteStudentCommand>();

// DEPARTMENT Commands and Queries
builder.Services.AddScoped<GetDepartmentsQuery>();
builder.Services.AddScoped<GetDepartmentByIdQuery>();
builder.Services.AddScoped<CreateDepartmentCommand>();
builder.Services.AddScoped<UpdateDepartmentCommand>();
builder.Services.AddScoped<DeleteDepartmentCommand>();

// FACULTY Commands and Queries
builder.Services.AddScoped<GetFacultiesQuery>();
builder.Services.AddScoped<GetFacultyByIdQuery>();
builder.Services.AddScoped<CreateFacultyCommand>();
builder.Services.AddScoped<UpdateFacultyCommand>();
builder.Services.AddScoped<DeleteFacultyCommand>();

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