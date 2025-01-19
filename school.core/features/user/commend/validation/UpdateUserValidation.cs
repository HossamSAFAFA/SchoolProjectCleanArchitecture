using FluentValidation;
using school.core.features.user.commend.models;
using School.Service.Abstract;

namespace school.core.features.user.commend.validation
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserCommend>
    {



        public UpdateUserValidation(IStuidentservice _stuidentservice)
        {

            ApplayFalidator();

        }




        public void ApplayFalidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Name should not empaty")
                .NotNull().WithMessage("Name must should not be null");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Name should not empaty")
                .NotNull().WithMessage("Name must should not be null");







            RuleFor(x => x.Address).NotEmpty().WithMessage("Address should not empaty")
                .NotNull().WithMessage("Address must should not be null");








        }



    }
}


