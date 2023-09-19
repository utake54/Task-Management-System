using FluentValidation;
using TaskManagement.API.Request;
using TaskManagement.Utility;

namespace TaskManagement.API.Infrastructure.Validator.Category
{
    public class CategoryValidator : AbstractValidator<AddCategoryRequest>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Category)
                .NotEmpty()
                .NotNull()
                .WithErrorCode(ContentLoader.ReturnMessage("TM053"));
        }
    }

    public class CategoryUpdateValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(ContentLoader.ReturnMessage("TM054"));

            RuleFor(x => x.Category)
                .NotEmpty()
                .NotNull()
                .WithErrorCode(ContentLoader.ReturnMessage("TM053"));
        }
    }

    public class CategoryDeleteValidator : AbstractValidator<DeleteCategoryRequest>
    {
        public CategoryDeleteValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(ContentLoader.ReturnMessage("TM054"));
        }
    }

    public class CategoryGetValidator : AbstractValidator<GetByIdCategoryRequest>
    {
        public CategoryGetValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(ContentLoader.ReturnMessage("TM054"));
        }
    }
}
