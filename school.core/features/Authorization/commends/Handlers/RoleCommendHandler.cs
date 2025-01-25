using MediatR;
using school.core.Base;
using school.core.features.Authorization.commends.models;
using school.Data.requesr;
using School.Service.Abstract;

namespace school.core.features.Authorization.commends.Handlers
{
    public class RoleCommendHandler : ResponseHandler, IRequestHandler<AddRoleCommend, Response<string>>, IRequestHandler<EditRolecommend, Response<string>>, IRequestHandler<DeleteRoleCommend, Response<string>>, IRequestHandler<UpdateRoleCommend, Response<string>>
    {
        private readonly IAuthorize authorizationService;
        public RoleCommendHandler(IAuthorize _authorizationService)
        {
            authorizationService = _authorizationService;
        }



        public async Task<Response<string>> Handle(DeleteRoleCommend request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.DeleteRole(request.Id);
            if (result == "Not Found") return NotFound<string>("Role Not found");
            else if (result == "Success") return Success("Delte Success");
            else if (result == "Used") return BadRequest<string>("Role Is Used");
            else return BadRequest<string>(result);

        }

        public async Task<Response<string>> Handle(EditRolecommend request, CancellationToken cancellationToken)
        {
            EditRole e = new EditRole();
            e.Id = request.Id;
            e.RoleName = request.RoleName;
            //   EditRole e = (EditRole)request;
            var result = await authorizationService.EditRole(e);
            if (result == "Not Found") return NotFound<string>("Role Not found");
            else if (result == "Success") return Success("Update Success");
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateRoleCommend request, CancellationToken cancellationToken)
        {

            var result = await authorizationService.UpdateUserRole(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(result);
                case "FailedToRemoveOldRoles": return BadRequest<string>(result);
                case "FailedToAddNewRoles": return BadRequest<string>(result);
                case "FailedToUpdateUserRoles": return BadRequest<string>(result);
            }
            return Success<string>(result);
        }

        async Task<Response<string>> IRequestHandler<AddRoleCommend, Response<string>>.Handle(AddRoleCommend request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success")
            {
                return Success("");
            }
            else
            {
                return BadRequest<string>("Faild");
            }
        }
    }
}
