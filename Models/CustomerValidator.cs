using FluentValidation;

namespace MyFirstProject.Models
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name must be at least 3 letters long");
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid (must contain @ symbol).");
            RuleFor(x => x.Mobile).NotEmpty();
            RuleFor(x => x.Mobile).MaximumLength(10).WithMessage("The phone number must contain a maximum of 10 digits");
        }
    }
}