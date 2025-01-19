using FluentValidation;
using school.core.features.user.commend.models;
using School.Service.Abstract;

namespace school.core.features.user.commend.validation
{
    public class ChangePasswordvalidation : AbstractValidator<ChangePasswordCommend>
    {



        public ChangePasswordvalidation(IStuidentservice _stuidentservice)
        {

            ApplayFalidator();

        }




        public void ApplayFalidator()
        {
            RuleFor(x => x.currentPassword).NotEmpty().WithMessage("Name should not empaty")
                .NotNull().WithMessage("Name must should not be null");

            RuleFor(x => x.newPassword).NotEmpty().WithMessage("Name should not empaty")
                .NotNull().WithMessage("Name must should not be null");

            RuleFor(x => x.confirmPassword).Matches(X => X.confirmPassword).WithMessage("ConfirmPassword Should be equal password");





            RuleFor(x => x.id).NotEmpty().WithMessage("Address should not empaty")
                .NotNull().WithMessage("Address must should not be null");







        }



    }
}


