using MediatR;
using school.core.Base;

namespace school.core.features.student.commands.Models
{
    public class EditStudentCommend : IRequest<Response<string>>
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public int DepartmentId { get; set; }

    }
}
