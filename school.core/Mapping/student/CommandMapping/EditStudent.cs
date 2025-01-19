using school.core.features.student.commands.Models;
using school.Data.Entites;

namespace school.core.Mapping.student
{
    public partial class StudentProfile
    {

        public void EditStudienMaping()
        {
            CreateMap<EditStudentCommend, Student>().ForMember(des => des.DID, o => o.MapFrom(s => s.DepartmentId))
                .ForMember(des => des.StudID, o => o.MapFrom(s => s.Id));

        }
    }
}
