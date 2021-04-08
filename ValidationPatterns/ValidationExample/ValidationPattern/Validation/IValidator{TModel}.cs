using System;

namespace ValidationExample.ValidationPattern.Validation
{
    /// <summary>The validator class.</summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IValidator<TModel>
    {
        /// <summary>Adds a validation rule.</summary>
        IValidator<TModel> AddRule<TRule>()
            where TRule : IValidationRule<TModel>, new();

        /// <summary>Adds a validation rule.</summary>
        IValidator<TModel> AddRule(Type ruleType);

        /// <summary>Adds a validation rule.</summary>
        IValidator<TModel> AddRule(IValidationRule<TModel> rule);

        /// <summary>Adds a validation rule.</summary>
        IValidator<TModel> AddRules<TProp>(Func<TModel, TProp> propertySelector, params Type[] validators);

        /// <summary>Adds a validation rule.</summary>
        IValidator<TModel> AddRule<TProp>(Func<TModel, TProp> propertySelector, IValidationRule<TProp> rule);

        /// <summary>Validates the specified model.</summary>
        ValidationResult Validate(TModel model);
    }
}
