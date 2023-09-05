using FluentValidation;
using TaskManagement.API.Request;
using TaskManagement.Utility.RegexHelper;

namespace TaskManagement.API.Infrastructure.Validator.User
{
    public class UserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FirstName).
                NotEmpty().
                NotNull().
                Matches(RegexHelper.REGEX_NAME).
                WithMessage("TM023");

            RuleFor(x => x.LastName).
                NotEmpty().
                NotNull().
                Matches(RegexHelper.REGEX_NAME).
                WithMessage("TM024");

            RuleFor(x => x.EmailId).
                NotEmpty().
                NotNull().
                EmailAddress().
                WithMessage("TM025");

            RuleFor(x => x.MobileNo).
                NotEmpty().
                NotNull().
                Matches(RegexHelper.REGEX_MOBILE_NUMBER).
                WithMessage("TM026");

            RuleFor(x => x.DateOfBirth).
                NotEmpty().
                NotNull().
                WithMessage("TM027");

            RuleFor(x => x.Address).
                NotEmpty().
                NotNull().
                WithMessage("TM028");

            RuleFor(x => x.City).
                NotEmpty().
                NotNull().
                WithMessage("TM029");

            RuleFor(x => x.State).
                NotEmpty().
                NotNull().
                WithMessage("TM030");

            RuleFor(x => x.Country).
                NotEmpty().
                NotNull().
                WithMessage("TM031");

            RuleFor(x => x.CountryCode).
                NotEmpty().
                NotNull().
                Matches(RegexHelper.REGEX_COUNTRY_CODE).
                WithMessage("TM032");

            RuleFor(x => x.ZipCode).
                NotEmpty().
                NotNull().
                WithMessage("TM033");
        }
    }
}