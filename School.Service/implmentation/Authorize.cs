using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school.Data.Entites.identity;
using school.Data.Helper;
using school.Data.requesr;
using school.Data.result;
using school.infrstrcture.Data;
using School.Service.Abstract;
using System.Security.Claims;

namespace School.Service.implmentation
{
    public class Authorize : IAuthorize
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;
        public Authorize(RoleManager<IdentityRole<int>> _roleManager, UserManager<User> _userManager, ApplicationDbContext _context)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            context = _context;
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

        public async Task<ManageUserResult> GetMangeUserRolesData(User user)
        {
            //throw new NotImplementedException();
            var UserRoles = await userManager.GetRolesAsync(user);

            var Roles = await roleManager.Roles.ToListAsync();

            ManageUserResult manageUserResult = new ManageUserResult();
            List<Role> roles = new List<Role>();
            manageUserResult.UserId = user.Id;
            //var x= await userManager.IsInRoleAsync()
            foreach (var r in Roles)
            {
                Role roleresult = new Role();
                if (await userManager.IsInRoleAsync(user, r.Name))
                {
                    roleresult.HasRole = true;

                }
                else
                {
                    roleresult.HasRole = false;
                }
                roleresult.id = r.Id;
                roleresult.name = r.Name;

                roles.Add(roleresult);

            }
            manageUserResult.Roles = roles;
            return manageUserResult;

        }

        public async Task<string> UpdateUserRole(UpdateUseeRoleRequest UpdateUseeRoleRequest)
        {
            var transact = context.Database.BeginTransaction();
            try
            {
                var user = await userManager.FindByIdAsync(UpdateUseeRoleRequest.UserId.ToString());
                if (user == null)
                    return "UserIsNull";


                var userrole = await userManager.GetRolesAsync(user);

                var removeResult = await userManager.RemoveFromRolesAsync(user, userrole);
                if (!removeResult.Succeeded)
                {
                    return "FailedToRemoveOldRoles";
                }
                var role = UpdateUseeRoleRequest.Roles.Where(x => x.HasRole == true).Select(x => x.name);
                var result = await userManager.AddToRolesAsync(user, role);
                if (!result.Succeeded)
                    return "FailedToAddNewRoles";
                transact.Commit();
                return "Success";


            }
            catch (Exception e)
            {
                transact.Rollback();
                return "FailedToUpdateUserRoles";
            }
        }

        public async Task<ManageUserClaimResult> GetMangeUserClaimsData(User user)
        {
            var UserClaims = await userManager.GetClaimsAsync(user);
            var respons = new ManageUserClaimResult();
            List<UserClaim> userClaims = new List<UserClaim>();
            respons.UserId = user.Id;

            foreach (var cliam in ClaimStore.cliams)
            {
                UserClaim userClaim = new UserClaim();
                userClaim.name = cliam.Type;
                if (UserClaims.Any(x => x.Type == cliam.Type))
                {
                    userClaim.value = true;
                }
                else
                {
                    userClaim.value = false;

                }

                userClaims.Add(userClaim);

            }
            respons.userclaims = userClaims;
            return respons;
        }

        public async Task<string> UpdateUserCliam(UpdateClaimsRequest UpdateUseCliamsRequest)
        {
            var transact = context.Database.BeginTransaction();
            try
            {
                var user = await userManager.FindByIdAsync(UpdateUseCliamsRequest.UserId.ToString());
                if (user == null)
                    return "UserIsNull";
                var userCliam = await userManager.GetClaimsAsync(user);
                var RemoveCliam = await userManager.RemoveClaimsAsync(user, userCliam);
                if (!RemoveCliam.Succeeded)
                {
                    return "FailedToRemoveOldCliams";
                }
                var cliams = UpdateUseCliamsRequest.userclaims.Where(x => x.value == true).Select(x => new Claim(x.name, x.value.ToString()));
                var result = await userManager.AddClaimsAsync(user, cliams);
                if (!result.Succeeded)
                    return "FailedToAddNewCliams";
                transact.Commit();
                return "Success";
            }
            catch (Exception e)
            {
                transact.Rollback();
                return "FailedToUpdateUserCliams";
            }
        }
    }
}

