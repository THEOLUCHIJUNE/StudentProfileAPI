using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class UpdateFacultyCommand
    {
        private readonly IFacultyService _facultyService;

        public UpdateFacultyCommand(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task ExecuteAsync(Faculty faculty)
        {
            await _facultyService.UpdateFacultyAsync(faculty);
        }
    }
}