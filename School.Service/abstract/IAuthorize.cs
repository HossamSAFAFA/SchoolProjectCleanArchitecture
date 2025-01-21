using Microsoft.AspNetCore.Identity;
using school.Data.Dto;

namespace School.Service.Abstract
{
    public interface IAuthorize
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRole(EditRole editRole);
        public Task<string> DeleteRole(int roleId);
        public Task<List<IdentityRole<int>>> GetListRole();
        public Task<IdentityRole<int>> GetRoleById(int id);


    }

}
