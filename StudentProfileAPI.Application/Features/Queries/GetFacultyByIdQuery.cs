using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetFacultyByIdQuery
    {
        private readonly IFacultyService _facultyService;

        public GetFacultyByIdQuery(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task<Faculty?> ExecuteAsync(int facultyId)
        {
            return await _facultyService.GetFacultyByIdAsync(facultyId);
        }
    }
}