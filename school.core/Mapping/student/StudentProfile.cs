using AutoMapper;

namespace school.core.Mapping.student
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {

            GetStudientListMaping();
            GetStudienMaping();
            AddStudienMaping();
            EditStudienMaping();
        }

    }
}
