using school.core.features.student.query.Results;
using school.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.core.Mapping.student
{
    public partial class StudentProfile
    {
        public void GetStudienMaping()
        {
            CreateMap<Student, GetSingleStudent>().ForMember(des => des.nameOfdept, o => o.MapFrom(s => s.Department.DName));

        }
    }
}
