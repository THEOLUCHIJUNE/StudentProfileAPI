using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class DeleteDepartmentCommand
    {
        private readonly IDepartmentService _departmentService;

        public DeleteDepartmentCommand(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task ExecuteAsync(int departmentId)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId);
        }
    }
}