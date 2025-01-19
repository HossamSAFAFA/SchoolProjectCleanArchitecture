using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using school.Data.Entites;
using school.Data.Entites.identity;

namespace school.infrstrcture.Data
{
    //public class ApplicationDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }
        public DbSet<User> users { set; get; }
        public DbSet<UserRefreshToken> userRefreshTokens { set; get; }

        public DbSet<Department> departments { set; get; }
        public DbSet<Student> students { set; get; }
        public DbSet<DepartmetSubject> departmetsubjects { set; get; }
        public DbSet<Subjects> subjects { set; get; }
        public DbSet<StudentSubject> studentsubject { set; get; }



    }
}
