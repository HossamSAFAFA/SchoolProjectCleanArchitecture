using Microsoft.EntityFrameworkCore;
using school.Data.Entites;
using school.infrstrcture.Abstract;
using school.infrstrcture.Baseinfrstcture;
using school.infrstrcture.Data;

namespace school.infrstrcture.Repositary
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        DbSet<Student> _student;
        public StudentRepository(ApplicationDbContext _context) : base(_context)
        {
            _student = _context.students;
        }
        public async Task<List<Student>> GetAllStudent()
        {


            return await _student.Include(o => o.Department).ToListAsync();

        }


    }
}
