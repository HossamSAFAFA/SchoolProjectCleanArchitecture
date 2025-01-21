using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school.Data.Dto;
using school.Data.Entites.identity;
using School.Service.Abstract;

namespace School.Service.implmentation
{
    public class Authorize : IAuthorize
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly UserManager<User> userManager;
        public Authorize(RoleManager<IdentityRole<int>> _roleManager, UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;

        }


        async Task<string> IAuthorize.AddRoleAsync(string roleName)
        {
            var idrntityRole = new IdentityRole<int>();
            idrntityRole.Name = roleName;
            var result = await roleManager.CreateAsync(idrntityRole);
            if (result.Succeeded)
            {
                return "Success";
            }
            else
            {
                return "Faild";
            }

        }
        public async Task<bool> IsRoleExistByName(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null) return false;
            return true;
            //return await roleManager.RoleExistsAsync(roleName);
        }

        public async Task<string> EditRole(EditRole editRole)
        {
            var role = await roleManager.FindByIdAsync(editRole.Id.ToString());

            if (role == null)
                return "Role Not Found";

            role.Name = editRole.RoleName;
            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return "Success";

            // تحسين قراءة الأخطاء
            var errorMessages = result.Errors.Select(e => e.Description);
            return string.Join(" | ", errorMessages);


        }

        public async Task<string> DeleteRole(int roleId)
        {
            var Role = await roleManager.FindByIdAsync(roleId.ToString());
            if (Role == null) return "Not Found";
            else
            {
                var users = await userManager.GetUsersInRoleAsync(Role.Name);
                if (users.Count > 0) return "Used";
                var result = await roleManager.DeleteAsync(Role);
                if (result.Succeeded)
                    return "Success";

                // تحسين قراءة الأخطاء
                var errorMessages = result.Errors.Select(e => e.Description);
                return string.Join(" | ", errorMessages);
            }
        }

        async Task<List<IdentityRole<int>>> IAuthorize.GetListRole()
        {
            var role = await roleManager.Roles.ToListAsync();
            return role;
        }

        async Task<IdentityRole<int>> IAuthorize.GetRoleById(int id)
        {
            var Role = await roleManager.FindByIdAsync(id.ToString());
            return Role;

        }
    }
}
