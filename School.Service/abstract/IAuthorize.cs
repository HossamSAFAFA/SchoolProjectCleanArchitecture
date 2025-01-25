using Microsoft.AspNetCore.Identity;
using school.Data.Entites.identity;
using school.Data.requesr;
using school.Data.result;

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
        public Task<ManageUserResult> GetMangeUserRolesData(User user);
        public Task<ManageUserClaimResult> GetMangeUserClaimsData(User user);
        public Task<string> UpdateUserRole(UpdateUseeRoleRequest UpdateUseeRoleRequest);
        public Task<string> UpdateUserCliam(UpdateClaimsRequest UpdateUseCliamsRequest);

    }

}
