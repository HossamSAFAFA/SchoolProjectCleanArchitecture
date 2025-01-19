using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using school.core.Base;
using school.core.features.student.query.Models;
using school.core.features.student.query.Results;
using school.core.Resourse;
using School.Service.Abstract;

namespace school.core.features.student.query.Handlers
{
    public class StudentQueryHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentResult>>>, IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudent>>
    {
        #region field
        private readonly IStuidentservice stuidentservice;
        private readonly IMapper Mapper;
        private readonly IStringLocalizer<sharedResourse> stringLocalizer;

        #endregion
        #region constractor
        public StudentQueryHandler(IStuidentservice _stuidentservice, IMapper mapper, IStringLocalizer<sharedResourse> _stringLocalizer) : base(_stringLocalizer)
        {
            stuidentservice = _stuidentservice;
            Mapper = mapper;
            stringLocalizer = _stringLocalizer;
        }


        #endregion

        #region
        async Task<Response<List<GetStudentResult>>> IRequestHandler<GetStudentListQuery, Response<List<GetStudentResult>>>.Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var student = await stuidentservice.GetStudentListAsync();
            var studentResults = Mapper.Map<List<GetStudentResult>>(student);
            return Success(studentResults);
        }
        public async Task<Response<GetSingleStudent>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await stuidentservice.GetStudentById(request.id);
            var single = Mapper.Map<GetSingleStudent>(student);
            if (single == null) return NotFound<GetSingleStudent>(stringLocalizer[sharedResourseKey.NotFound
                ]);
            return Success(single);
        }
        #endregion


    }
}
