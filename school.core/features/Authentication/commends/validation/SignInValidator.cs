using FluentValidation;
using school.core.features.Authentication.commends.models;

namespace school.core.features.Authentication.commends.validation
{
    public class SignInValidator : AbstractValidator<SignInCommend>
    {

        public SignInValidator()
        {
            ApplayValidationRule();
        }
        void ApplayValidationRule()
        {

            RuleFor(x => x.Username).NotEmpty().WithMessage("User should not be null").NotNull().WithMessage("user shold not be null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("User should not be null").NotNull().WithMessage("user shold not be null");

        }
    }
}
