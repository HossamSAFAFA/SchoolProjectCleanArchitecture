using MediatR;
using school.core.Base;
using school.core.features.Authorization.commends.models;
using School.Service.Abstract;

namespace school.core.features.Authorization.commends.Handlers
{
    public class CliamsCommendHandler : ResponseHandler, IRequestHandler<UpdateClaimsCommend, Response<string>>
    {

        private readonly IAuthorize authorizationService;
        public CliamsCommendHandler(IAuthorize _authorizationService)
        {
            authorizationService = _authorizationService;
        }

        public async Task<Response<string>> Handle(UpdateClaimsCommend request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.UpdateUserCliam(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(result);
                case "FailedToRemoveOldCliams": return BadRequest<string>(result);
                case "FailedToAddNewCliams": return BadRequest<string>(result);
                case "FailedToUpdateUserCliams": return BadRequest<string>(result);
            }
            return Success<string>(result);
        }
    }
}
