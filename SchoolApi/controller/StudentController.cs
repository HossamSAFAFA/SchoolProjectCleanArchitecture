using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school.core.features.student.commands.Models;
using school.core.features.student.query.Models;
using SchoolApi.Base;



namespace SchoolApi.controller
{

    [ApiController]
    // [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    //  [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    //  [Authorize("Bearer")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

    public class StudentController : AppController
    {
        private IMediator _Mediator;

        public StudentController(IMediator m)
        {
            _Mediator = m;
        }

        [HttpGet(school.Data.AppMetaData.Router.studentRouting.List)]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await Mediator.Send(new GetStudentListQuery());
            return Ok(response);


        }
        [HttpGet(school.Data.AppMetaData.Router.studentRouting.GetById)]
        public async Task<IActionResult> GetStudent(int id)
        {
            var response = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(response);


        }
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Create Student")]
        [HttpPost(school.Data.AppMetaData.Router.studentRouting.AddStudent)]
        public async Task<IActionResult> Created(AddStudentCommend commend)
        {
            var response = await Mediator.Send(commend);
            return NewResult(response);


        }
        [HttpPut(school.Data.AppMetaData.Router.studentRouting.EditStudent)]
        public async Task<IActionResult> Edit(EditStudentCommend commend)
        {
            var response = await _Mediator.Send(commend);
            return NewResult(response);
        }

        [HttpDelete(school.Data.AppMetaData.Router.studentRouting.DeleteStudent)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteStudent(id));
            return NewResult(response);


        }

    }


}
