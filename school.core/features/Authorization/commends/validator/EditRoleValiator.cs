using FluentValidation;
using school.core.features.Authorization.commends.models;
using School.Service.Abstract;

namespace school.core.features.Authorization.commends.validator
{
    public class EditRoleValiator : AbstractValidator<EditRolecommend>
    {

        private readonly IAuthorize authorizationService;
        public EditRoleValiator(IAuthorize _authorizationService)
        {
            ApplayRule();

            authorizationService = _authorizationService;


        }


        public void ApplayRule()
        {

            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role Name should not empaty")
                    .NotNull().WithMessage("Name must should not be null");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Role Id should not empaty")
                    .NotNull().WithMessage("Role Id must should not be null");

        }



    }
}
