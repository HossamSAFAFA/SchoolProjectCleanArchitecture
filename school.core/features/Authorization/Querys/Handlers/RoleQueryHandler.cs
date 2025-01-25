using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using school.core.Base;
using school.core.features.Authorization.Querys.models;
using school.core.features.Authorization.Querys.Result;
using school.Data.Entites.identity;
using school.Data.result;
using School.Service.Abstract;

namespace school.core.features.Authorization.Querys.Handlers
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRoleListQuery, Response<List<GetRoleResult>>>, IRequestHandler<GetRolebyId, Response<GetRoleResult>>, IRequestHandler<ManageUserRoleQuery, Response<ManageUserResult>>
    {
        private readonly IAuthorize authorizationService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public RoleQueryHandler(IAuthorize _authorizationService, IMapper _mapper, UserManager<User> _userManager)
        {
            authorizationService = _authorizationService;
            mapper = _mapper;
            userManager = _userManager;

        }


        public async Task<Response<List<GetRoleResult>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var role = await authorizationService.GetListRole();
            var result = mapper.Map<List<GetRoleResult>>(role);

            return Success(result);
        }

        public async Task<Response<ManageUserResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return NotFound<ManageUserResult>("User Not Found");
            }
            else
            {
                var result = await authorizationService.GetMangeUserRolesData(user);
                return Success(result);
            }

        }

        async Task<Response<GetRoleResult>> IRequestHandler<GetRolebyId, Response<GetRoleResult>>.Handle(GetRolebyId request, CancellationToken cancellationToken)
        {
            var role = await authorizationService.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleResult>("Role Not found");
            var result = mapper.Map<GetRoleResult>(role);

            return Success(result);
        }
    }
}
