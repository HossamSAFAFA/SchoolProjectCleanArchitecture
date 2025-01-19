using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using school.core.Base;
using school.core.features.student.commands.Models;
using school.core.Resourse;
using school.Data.Entites;
using School.Service.Abstract;

namespace school.core.features.student.commands.Handlers
{
    public class StudentCommendHandelr : ResponseHandler, IRequestHandler<AddStudentCommend, Response<string>>, IRequestHandler<EditStudentCommend, Response<string>>, IRequestHandler<DeleteStudent, Response<string>>
    {
        private readonly IStuidentservice stuidentservice;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<sharedResourse> stringLocalizer;
        #region Fields
        #endregion
        #region Constractor
        public StudentCommendHandelr(IStuidentservice _stuidentservice, IMapper _mapper, IStringLocalizer<sharedResourse> _stringLocalizer) : base(_stringLocalizer)
        {
            stuidentservice = _stuidentservice;
            mapper = _mapper;
            stringLocalizer = _stringLocalizer;
        }


        #endregion
        #region HandelFunction
        #endregion
        async Task<Response<string>> IRequestHandler<AddStudentCommend, Response<string>>.Handle(AddStudentCommend request, CancellationToken cancellationToken)
        {
            var student = mapper.Map<Student>(request);
            var Result = await stuidentservice.AddAsync(student);
            if (Result == "Exist")
            {
                return unprocessableEntity<string>("the name is exist");
            }
            else if (Result == "Student added successfully")
            {
                return Created<string>(stringLocalizer[sharedResourseKey.Created]);
            }
            else
            {
                return BadRequest<string>();
            }
        }
        public async Task<Response<string>> Handle(EditStudentCommend request, CancellationToken cancellationToken)
        {
            var student = await stuidentservice.GetStudentById(request.Id);

            if (student == null)
                return NotFound<String>("Student Not Found");
            else
            {
                var studentt = mapper.Map(request, student);
                var result = await stuidentservice.EditAsync(studentt);
                if (result == "Success")
                    return Created<string>(stringLocalizer[sharedResourseKey.Created]);
                else
                    return BadRequest<string>();
            }


        }

        public async Task<Response<string>> Handle(DeleteStudent request, CancellationToken cancellationToken)
        {
            var student = await stuidentservice.GetStudentById(request.id);

            if (student == null)
                return NotFound<String>(stringLocalizer[sharedResourseKey.NotFound]);
            else
            {
                var result = await stuidentservice.DeleteAsyn(request.id);
                if (result == "Delete")
                    return Deleted<string>();
                else
                    return BadRequest<string>();

            }

        }
    }
}
