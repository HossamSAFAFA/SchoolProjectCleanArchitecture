using FluentValidation;
using school.core.features.Authorization.commends.models;
using School.Service.Abstract;

namespace school.core.features.Authorization.commends.validator
{
    public class DeleteValidator : AbstractValidator<DeleteRoleCommend>
    {
        private readonly IAuthorize authorizationService;
        public DeleteValidator(IAuthorize _authorizationService)
        {
            authorizationService = _authorizationService;
            ApplayRule();



        }


        public void ApplayRule()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("ID should not empaty")
                    .NotNull().WithMessage("Id must should not be null");
        }

    }
}
