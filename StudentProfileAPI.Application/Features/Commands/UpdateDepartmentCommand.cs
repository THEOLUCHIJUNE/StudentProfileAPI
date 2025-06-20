using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class UpdateDepartmentCommand
    {
        private readonly IDepartmentService _departmentService;

        public UpdateDepartmentCommand(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task ExecuteAsync(Department department)
        {
            await _departmentService.UpdateDepartmentAsync(department);
        }
    }
}