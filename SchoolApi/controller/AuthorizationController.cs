using Microsoft.AspNetCore.Mvc;
using school.core.features.Authorization.commends.models;
using school.core.features.Authorization.Querys.models;
using SchoolApi.Base;

namespace SchoolApi.controller
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    public class AuthorizationController : AppController
    {
        [HttpPost(school.Data.AppMetaData.Router.Authorize.create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommend command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(school.Data.AppMetaData.Router.Authorize.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRolecommend command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(school.Data.AppMetaData.Router.Authorize.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteRoleCommend(id);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(school.Data.AppMetaData.Router.Authorize.GetList)]
        public async Task<IActionResult> GetListRole()
        {
            var command = new GetRoleListQuery();
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(school.Data.AppMetaData.Router.Authorize.GetRole)]
        public async Task<IActionResult> GetRolebId(int id)
        {
            var command = new GetRolebyId(id);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(school.Data.AppMetaData.Router.Authorize.GetRoleManageUserRole)]
        public async Task<IActionResult> ManageUserRole(int id)
        {
            var command = new ManageUserRoleQuery(id);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(school.Data.AppMetaData.Router.Authorize.UpdateUserRole)]
        public async Task<IActionResult> UpdateUserRple(UpdateRoleCommend command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(school.Data.AppMetaData.Router.Authorize.GetRoleManageClaimsRole)]
        public async Task<IActionResult> ManageUserClaims(int id)
        {
            var command = new ManageUserClaimQuery(id);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(school.Data.AppMetaData.Router.Authorize.UpdateCliamsRole)]
        public async Task<IActionResult> UpdateUserCliams(UpdateClaimsCommend command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
