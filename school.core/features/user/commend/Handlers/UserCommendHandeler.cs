using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using school.core.Base;
using school.core.features.user.commend.models;
using school.Data.Entites.identity;

namespace school.core.features.user.commend.Handlers
{
    public class UserCommendHandeler : ResponseHandler, IRequestHandler<AddUserCommend, Response<string>>,
        IRequestHandler<UpdateUserCommend, Response<string>>,
        IRequestHandler<DeleteUserCommend, Response<string>>, IRequestHandler<ChangePasswordCommend, Response<string>>
    {

        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserCommendHandeler(IMapper _mapper, UserManager<User> _userManager)
        {

            mapper = _mapper;
            userManager = _userManager;

        }

        public async Task<Response<string>> Handle(UpdateUserCommend request, CancellationToken cancellationToken)
        {

            var User = await userManager.FindByIdAsync(request.id.ToString());
            if (User == null) return NotFound<string>("Not found");
            else
            {
                var NewUser = mapper.Map(request, User);
                var result = await userManager.UpdateAsync(NewUser);
                if (!result.Succeeded)
                {
                    return BadRequest<string>("bad request");
                }
                else
                {
                    return Success("Update");
                }
            }
        }

        public async Task<Response<string>> Handle(DeleteUserCommend request, CancellationToken cancellationToken)
        {
            var User = await userManager.FindByIdAsync(request.id.ToString());
            if (User == null) return NotFound<string>("Not found");
            else
            {
                var result = await userManager.DeleteAsync(User);
                if (!result.Succeeded)
                {
                    return BadRequest<string>("bad request");
                }
                else
                {
                    return Success("Deleted");
                }

            }
        }

        public async Task<Response<string>> Handle(ChangePasswordCommend request, CancellationToken cancellationToken)
        {
            var User = await userManager.FindByIdAsync(request.id.ToString());
            if (User == null) return NotFound<string>("Not found");
            else
            {
                var result = await userManager.ChangePasswordAsync(User, request.currentPassword, request.newPassword);

                if (!result.Succeeded)
                {
                    return BadRequest<string>(result.Errors.FirstOrDefault().Description);
                }
                else
                {
                    return Success("Password Changed");
                }

            }
        }

        async Task<Response<string>> IRequestHandler<AddUserCommend, Response<string>>.Handle(AddUserCommend request, CancellationToken cancellationToken)
        {
            var UserEmail = await userManager.FindByEmailAsync(request.Email);
            if (UserEmail != null)
                return BadRequest<string>("Email is Esxit");
            var UserUser = await userManager.FindByNameAsync(request.Email);
            if (UserUser != null)
                return BadRequest<string>("User is Esxit");
            var UserIdentity = mapper.Map<User>(request);
            var CreateResult = await userManager.CreateAsync(UserIdentity, request.password);
            if (!CreateResult.Succeeded)
            {
                return BadRequest<string>(CreateResult.Errors.FirstOrDefault().Description);
            }
            else
            {
                return Created<string>("");
            }
        }
    }
}
