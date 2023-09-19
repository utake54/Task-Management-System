using FluentValidation;
using TaskManagement.API.Request;
using TaskManagement.Utility;

namespace TaskManagement.API.Infrastructure.Validator.Task
{
    public class AddTaskRequestValidation : AbstractValidator<AddTaskRequest>
    {
        public AddTaskRequestValidation()
        {
            RuleFor(x => x.Title).
                NotEmpty().
                NotNull().
                WithMessage(ContentLoader.ReturnMessage("TM039"));

            RuleFor(x => x.Description).
               NotEmpty().
               NotNull().
               WithMessage(ContentLoader.ReturnMessage("TM040"));

            RuleFor(x => x.Priority).
               NotEmpty().
               NotNull().
               WithMessage(ContentLoader.ReturnMessage("TM041"));

            RuleFor(x => x.DueDate).
               NotEmpty().
               NotNull().
               NotNull().WithMessage(ContentLoader.ReturnMessage("TM042")).
               GreaterThan(DateTime.Now).
               WithMessage(ContentLoader.ReturnMessage("TM043"));

        }
    }

    public class UpdateTaskValidator:AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Id).
                NotEmpty().
                NotNull().
                WithMessage(ContentLoader.ReturnMessage("TM044"));

            RuleFor(x => x.Title).
                NotEmpty().
                NotNull().
                WithMessage(ContentLoader.ReturnMessage("TM039"));

            RuleFor(x => x.Description).
               NotEmpty().
               NotNull().
               WithMessage(ContentLoader.ReturnMessage("TM040"));

            RuleFor(x => x.Priority).
               NotEmpty().
               NotNull().
               WithMessage(ContentLoader.ReturnMessage("TM041"));

            RuleFor(x => x.DueDate).
               NotEmpty().
               NotNull().
               NotNull().WithMessage(ContentLoader.ReturnMessage("TM042")).
               GreaterThan(DateTime.Now).
               WithMessage(ContentLoader.ReturnMessage("TM043"));
        }
    }
}
