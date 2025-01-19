using Microsoft.AspNetCore.Mvc;
using school.core.features.Authentication.commends.models;
using school.core.features.Authentication.Querys.models;
using SchoolApi.Base;

namespace SchoolApi.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppController
    {

        [HttpPost(school.Data.AppMetaData.Router.Authentication.create)]
        public async Task<IActionResult> Created(SignInCommend commend)
        {
            var response = await Mediator.Send(commend);
            return NewResult(response);


        }


        [HttpPost(school.Data.AppMetaData.Router.Authentication.RefrashToken)]
        public async Task<IActionResult> RefrashToken(RefrashTokenCommend commend)
        {
            var response = await Mediator.Send(commend);
            return NewResult(response);


        }
        [HttpGet(school.Data.AppMetaData.Router.Authentication.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

    }
}
