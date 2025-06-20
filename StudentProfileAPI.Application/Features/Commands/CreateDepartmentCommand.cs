using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class CreateDepartmentCommand
    {
        private readonly IDepartmentService _departmentService;

        public CreateDepartmentCommand(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task ExecuteAsync(Department department)
        {
            await _departmentService.AddDepartmentAsync(department);
        }
    }
}