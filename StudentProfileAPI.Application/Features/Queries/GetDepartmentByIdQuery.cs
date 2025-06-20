using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetDepartmentByIdQuery
    {
        private readonly IDepartmentService _departmentService;

        public GetDepartmentByIdQuery(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<Department?> ExecuteAsync(int departmentId)
        {
            return await _departmentService.GetDepartmentByIdAsync(departmentId);
        }
    }
}