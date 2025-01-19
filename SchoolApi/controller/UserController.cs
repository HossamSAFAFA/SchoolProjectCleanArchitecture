using MediatR;
using Microsoft.AspNetCore.Mvc;
using school.core.features.user.commend.models;
using school.core.features.user.Query.models;
using SchoolApi.Base;

namespace SchoolApi.controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : AppController
    {
        private IMediator _Mediator;

        public UserController(IMediator m)
        {
            _Mediator = m;
        }

        [HttpPost(school.Data.AppMetaData.Router.UserRouting.AddUser)]
        public async Task<IActionResult> Create([FromBody] AddUserCommend Commend)
        {
            var response = await _Mediator.Send(Commend);
            return NewResult(response);
        }
        [HttpPut(school.Data.AppMetaData.Router.UserRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommend Commend)
        {
            var response = await _Mediator.Send(Commend);
            return NewResult(response);
        }

        [HttpGet(school.Data.AppMetaData.Router.UserRouting.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            GetUserList getUserListResponse = new GetUserList();
            var response = await _Mediator.Send(getUserListResponse);
            return NewResult(response);
        }
        [HttpGet(school.Data.AppMetaData.Router.UserRouting.GetById)]
        public async Task<IActionResult> GetUserById(int id)
        {
            GetUseById getUseById = new GetUseById(id);
            var response = await _Mediator.Send(getUseById);
            return NewResult(response);
        }
        [HttpDelete(school.Data.AppMetaData.Router.UserRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteUserCommend deleteUserCommend = new DeleteUserCommend(id);
            var response = await _Mediator.Send(deleteUserCommend);
            return NewResult(response);
        }
        [HttpPut(school.Data.AppMetaData.Router.UserRouting.changepassord)]
        public async Task<IActionResult> changepassord(ChangePasswordCommend commend)
        {

            var response = await _Mediator.Send(commend);
            return NewResult(response);
        }




    }
}
