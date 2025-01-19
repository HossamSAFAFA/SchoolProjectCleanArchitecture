using AutoMapper;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school.core.Base;
using school.core.features.user.Query.models;
using school.core.features.user.Query.Result;
using school.Data.Entites.identity;

namespace school.core.features.user.Query.Handlers
{
    public class UserQueryHandeler : ResponseHandler, IRequestHandler<GetUserList, Response<List<GetUserListResponse>>>, IRequestHandler<GetUseById, Response<GetUserByid>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public UserQueryHandeler(IMapper _mapper, UserManager<User> _userManager)
        {

            mapper = _mapper;
            userManager = _userManager;

        }


        public async Task<Response<GetUserByid>> Handle(GetUseById request, CancellationToken cancellationToken)
        {
            var User = await userManager.FindByIdAsync(request.id.ToString());
            if (User == null) return NotFound<GetUserByid>("Not found");
            var UserResponse = mapper.Map<GetUserByid>(User);
            return Success(UserResponse);
        }

        async Task<Base.Response<List<GetUserListResponse>>> IRequestHandler<GetUserList, Base.Response<List<GetUserListResponse>>>.Handle(GetUserList request, CancellationToken cancellationToken)
        {
            var User = await userManager.Users.ToListAsync();


            var UserResponse = mapper.Map<List<GetUserListResponse>>(User);

            return Success(UserResponse);
        }


    }
}

