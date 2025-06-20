using StudentProfileAPI.Application.Interfaces;

namespace StudentProfileAPI.Application.Features.Commands
{
    public class DeleteFacultyCommand
    {
        private readonly IFacultyService _facultyService;

        public DeleteFacultyCommand(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task ExecuteAsync(int facultyId)
        {
            await _facultyService.DeleteFacultyAsync(facultyId);
        }
    }
}