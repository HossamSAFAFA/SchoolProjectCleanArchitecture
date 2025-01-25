using MediatR;
using school.core.Base;
using school.Data.requesr;

namespace school.core.features.Authorization.commends.models
{
    public class UpdateClaimsCommend : UpdateClaimsRequest, IRequest<Response<string>>
    {




    }
}
