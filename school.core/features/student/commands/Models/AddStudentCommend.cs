using MediatR;
using school.core.Base;

namespace school.core.features.student.commands.Models
{
    public class AddStudentCommend : IRequest<Response<string>>
    {

        // [Required(ErrorMessage ="Name is required y safafa")]
        public string Name { get; set; }
        //[Required]
        public string Address { get; set; }

        public string Phone { get; set; }
        public int DepartmentId { get; set; }



    }
}
