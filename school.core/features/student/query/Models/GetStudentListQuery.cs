using MediatR;
using school.core.Base;
using school.core.features.student.query.Results;
using school.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.core.features.student.query.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentResult>>>
    {

    }
}
