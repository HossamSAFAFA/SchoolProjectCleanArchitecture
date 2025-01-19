using FluentValidation;
using school.core.features.student.commands.Models;
using School.Service.Abstract;

namespace school.core.features.student.commands.validation
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommend>
    {

        #region field
        private readonly IStuidentservice stuidentservice;
        #endregion
        #region constarctor
        public EditStudentValidator(IStuidentservice _stuidentservice)
        {
            stuidentservice = _stuidentservice;
            ApplayFalidator();
            ApplayCustomFalidator();
        }



        #endregion
        #region Action
        public void ApplayFalidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not empaty")
                .NotNull().WithMessage("Name must should not be null")
                .MaximumLength(10).WithMessage("Max lenghth must be 10");



            RuleFor(x => x.Address).NotEmpty().WithMessage("Address should not empaty")
                .NotNull().WithMessage("Address must should not be null")
                .MaximumLength(10).WithMessage("Max lenghth must be 10");




        }


        public void ApplayCustomFalidator()
        {



        }

        #endregion
    }
}