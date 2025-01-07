using FluentValidation.Results;

namespace CarRentService.DAL.Extensions;

public static class ValidationExtensions
{
    public static string GetValidationErrors(this ValidationResult validationResult)
    {
        var errors = string.Empty;
        foreach (var error in validationResult.Errors)
        {
            errors += $"{error.ErrorMessage}\n";
        }

        return errors;
    }
}