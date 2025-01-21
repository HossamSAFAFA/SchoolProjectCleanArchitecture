using AutoMapper;
using MediatR;
using school.core.Base;
using school.core.features.Authorization.Querys.models;
using school.core.features.Authorization.Querys.Result;
using School.Service.Abstract;

namespace school.core.features.Authorization.Querys.Handlers
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRoleListQuery, Response<List<GetRoleResult>>>, IRequestHandler<GetRolebyId, Response<GetRoleResult>>
    {
        private readonly IAuthorize authorizationService;
        private readonly IMapper mapper;
        public RoleQueryHandler(IAuthorize _authorizationService, IMapper _mapper)
        {
            authorizationService = _authorizationService;
            mapper = _mapper;
        }


        public async Task<Response<List<GetRoleResult>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var role = await authorizationService.GetListRole();
            var result = mapper.Map<List<GetRoleResult>>(role);

            return Success(result);
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
