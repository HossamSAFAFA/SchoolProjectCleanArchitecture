using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using school.core.Base;
using school.core.features.Authorization.Querys.models;
using school.Data.Entites.identity;
using school.Data.result;
using School.Service.Abstract;

namespace school.core.features.Authorization.Querys.Handlers
{
    public class ClaimsQueryHandeler : ResponseHandler, IRequestHandler<ManageUserClaimQuery, Response<ManageUserClaimResult>>
    {
        private readonly IAuthorize authorizationService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public ClaimsQueryHandeler(IAuthorize _authorizationService, IMapper _mapper, UserManager<User> _userManager)
        {
            authorizationService = _authorizationService;
            mapper = _mapper;
            userManager = _userManager;

        }
        public async Task<Response<ManageUserClaimResult>> Handle(ManageUserClaimQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return NotFound<ManageUserClaimResult>("User Not Found");
            }
            else
            {
                var result = await authorizationService.GetMangeUserClaimsData(user);
                return Success(result);
            }
        }
    }
}
