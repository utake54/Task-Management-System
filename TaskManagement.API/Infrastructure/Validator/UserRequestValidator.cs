using FluentValidation;
using TaskManagement.API.Request;

namespace TaskManagement.API.Infrastructure.Validator
{
    public class UserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("User name required.");
        }
    }
}
