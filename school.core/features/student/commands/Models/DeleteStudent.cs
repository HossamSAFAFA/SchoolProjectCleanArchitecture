using MediatR;
using school.core.Base;

namespace school.core.features.student.commands.Models
{
    public class DeleteStudent : IRequest<Response<string>>
    {
        public int id { get; set; }
        public DeleteStudent(int id)
        {
            this.id = id;
        }



    }
}
