using Microsoft.EntityFrameworkCore;
using school.Data.Entites;
using school.infrstrcture.Abstract;
using School.Service.Abstract;

namespace School.Service.implmentation
{
    public class StudentService : IStuidentservice

    {
        #region Fields
        private readonly IStudentRepository studentRepository;
        #endregion

        #region constractor
        public StudentService(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }
        #endregion

        #region Function Handel
        public async Task<List<Student>> GetStudentListAsync()
        {
            return await studentRepository.GetAllStudent();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await studentRepository.GetTableNoTracking().Include(x => x.Department).Where(x => x.StudID == id).FirstOrDefaultAsync();
            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            var studentResult = await studentRepository            //null
                 .GetTableNoTracking()
                 .Where(x => x.Name == student.Name)
                 .FirstOrDefaultAsync();
            if (studentResult != null) return "Exist";
            await studentRepository.AddAsync(student);
            return "Student added successfully";

        }

        public async Task<string> EditAsync(Student student)
        {
            try
            {

                await studentRepository.UpdateAsync(student);
                return "Success";
            }
            catch
            {
                return "Faild";
            }
        }
        public async Task<string> DeleteAsyn(int id)
        {

            var studentResult = await studentRepository.GetByIdAsync(id);

            //var trans = studentRepository.BeginTransaction;
            try
            {
                await studentRepository.DeleteAsync(studentResult);
                //  studentRepository.Commit();
                return "Delete";

            }
            catch
            {
                //studentRepository.RollBack();

                return "Faild";
            }

        }


        //   public bool IsExist(string student)
        //   {
        //       var studentResult = studentRepository
        //            .GetTableNoTracking()
        //            .Where(x => x.Name == student)
        //            .FirstOrDefaultAsync();
        //       if (studentResult != null) return false;
        //       return true;
        //   }

        //    public async Task<bool> IsExitExculedself(string name)
        //    {
        //        //   var studentResult = await studentRepository
        //        //        .GetTableNoTracking()
        //        //        .Where(x => x.Name == student && x.DID != 1)
        //        //        .FirstOrDefaultAsync();
        //        //   if (studentResult != null) return true;
        //        return false;
        //    }



        #endregion
    }
}
