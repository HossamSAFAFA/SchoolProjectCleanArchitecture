using school.Data.Entites;
using school.infrstrcture.Baseinfrstcture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.infrstrcture.Abstract
{
    public interface IStudentRepository: IGenericRepositoryAsync<Student>
    {

        public Task<List<Student>> GetAllStudent();


     }
}
