using StudentProfileAPI.Domain.Models;
using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class CreateFacultyCommand
    {
        private readonly IFacultyService _facultyService;

        public CreateFacultyCommand(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task ExecuteAsync(Faculty faculty)
        {
            await _facultyService.AddFacultyAsync(faculty);
        }
    }
}