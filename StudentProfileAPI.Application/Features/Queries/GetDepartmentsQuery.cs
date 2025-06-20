using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetDepartmentsQuery
    {
        private readonly IDepartmentService _departmentService;

        public GetDepartmentsQuery(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IEnumerable<Department>> ExecuteAsync()
        {
            return await _departmentService.GetAllDepartmentsAsync();
        }
    }
}