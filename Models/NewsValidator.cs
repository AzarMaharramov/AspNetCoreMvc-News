using FluentValidation;

namespace MyFirstProject.Models
{
    public class NewsValidator : AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(x => x.Title).MaximumLength(50).MinimumLength(3).WithMessage("Title should be between 5-50 characters");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content must not be empty");
            RuleFor(x => x.Content).MaximumLength(50).WithMessage("Content length must be at least 50 characters");
        }
    }
}