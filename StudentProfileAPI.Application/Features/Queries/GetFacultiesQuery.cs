using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Queries
{
    public class GetFacultiesQuery
    {
        private readonly IFacultyService _facultyService;

        public GetFacultiesQuery(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task<IEnumerable<Faculty>> ExecuteAsync()
        {
            return await _facultyService.GetAllFacultiesAsync();
        }
    }
}