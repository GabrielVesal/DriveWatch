using FluentValidation.Results;

namespace Application.Validators.Error
{
    public static class ValidationFailureExtension
    {
        public static IList<CustomValidatorFailure> ToCustomValidatorFailures(this IEnumerable<ValidationFailure> failures)
        {
            return failures.Select(f => new CustomValidatorFailure(f.PropertyName, f.ErrorMessage)).ToList();
        }
    }
}
