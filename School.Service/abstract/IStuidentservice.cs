using school.Data.Entites;

namespace School.Service.Abstract
{
    public interface IStuidentservice
    {


        public Task<List<Student>> GetStudentListAsync();
        public Task<Student> GetStudentById(int id);
        public Task<string> DeleteAsyn(int id);

        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);



        //public bool IsExist(string? name);
        //  public Task<bool> IsExitExculedself(string name);

    }


}

