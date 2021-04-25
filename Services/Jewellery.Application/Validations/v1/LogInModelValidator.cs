namespace Jewellery.Application.Validations.v1
{
    using FluentValidation;
    using Jewellery.Application.Models.v1;

    public class LogInModelValidator : AbstractValidator<LogInModel>
    {
        public LogInModelValidator()
        {
            _ = RuleFor(x => x.UserId).NotNull().NotEmpty();
            _ = RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
