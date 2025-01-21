using FluentValidation;
using school.core.features.Authorization.commends.models;
using School.Service.Abstract;

namespace school.core.features.Authorization.commends.validator
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommend>
    {
        private readonly IAuthorize authorizationService;
        public AddRoleValidator(IAuthorize _authorizationService)
        {
            authorizationService = _authorizationService;
            ApplayRule();
            ApplyCustomValidationsRules();


        }


        public void ApplayRule()
        {

            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role Name should not empaty")
                    .NotNull().WithMessage("Name must should not be null");
        }
        public async Task ApplyCustomValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (key, cancellationToken) =>
                    !await authorizationService.IsRoleExistByName(key))
                .WithMessage("Role already exists");
        }
    }
}
