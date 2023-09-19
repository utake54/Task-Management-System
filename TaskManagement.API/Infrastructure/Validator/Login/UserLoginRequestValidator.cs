using FluentValidation;
using TaskManagement.API.Request;
using TaskManagement.Utility;

namespace TaskManagement.API.Infrastructure.Validator.Login
{
    public class UserLoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.EmailOrMobile)
                .NotEmpty()
                .NotNull()
                .WithErrorCode(ContentLoader.ReturnMessage("TM055"));

            RuleFor(x => x.Password)
               .NotEmpty()
               .NotNull()
               .WithErrorCode(ContentLoader.ReturnMessage("TM056"));

        }
    }



    public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPassswordRequest>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(x => x.EmailOrMobile)
                .NotEmpty()
                .NotNull()
                .WithErrorCode(ContentLoader.ReturnMessage("TM055"));

            RuleFor(x => x.DateOfBirth)
               .NotEmpty()
               .NotNull()
               .WithErrorCode(ContentLoader.ReturnMessage("TM059"));

        }
    }


    public class PasswordReseValidator : AbstractValidator<PasswordResetRequest>
    {
        public PasswordReseValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .WithErrorCode(ContentLoader.ReturnMessage("TM056"));

            RuleFor(x => x.ConfirmPassword)
               .NotEmpty()
               .NotNull().WithMessage(ContentLoader.ReturnMessage("TM058"))
               .Equal(x => x.Password)
               .WithErrorCode(ContentLoader.ReturnMessage("TM007"));

        }
    }
}
