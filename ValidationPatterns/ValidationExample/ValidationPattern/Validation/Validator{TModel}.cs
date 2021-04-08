using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationExample.ValidationPattern.Validation
{
    /// <summary>The validator service.</summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class Validator<TModel> : IValidator<TModel>
	{
		private readonly Func<Type, object> _activator;
		private readonly List<Func<TModel, ValidationError>> _validators = new List<Func<TModel, ValidationError>>();

        /// <summary>Initializes a new instance of the <see cref="Validator{TModel}"/> class.</summary>
        public Validator()
			: this(Activator.CreateInstance)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="Validator{TModel}"/> class.</summary>
		protected Validator(Func<Type, object> activator)
		{
			_activator = activator;
		}

        /// <inheritdoc/>
        public IValidator<TModel> AddRule<TRule>()
			where TRule : IValidationRule<TModel>, new() =>
			AddRule(typeof(TRule));

		/// <inheritdoc/>
		public IValidator<TModel> AddRule(Type ruleType) =>
			AddRule(CreateRuleInstance<TModel>(ruleType));

		/// <inheritdoc/>
		public IValidator<TModel> AddRule<TProp>(Func<TModel, TProp> propertySelector, IValidationRule<TProp> rule)
		{
			AddRuleInternal((TModel model) => rule.Validate(propertySelector(model)), rule.Error);
			return this;
		}

		/// <inheritdoc/>
		public IValidator<TModel> AddRule<TRule, TProp>(Func<TModel, TProp> propertySelector)
			where TRule : IValidationRule<TProp>, new() =>
            AddRule(propertySelector, CreateRuleInstance<TProp>(typeof(TRule)));

		/// <inheritdoc/>
		public IValidator<TModel> AddRule(IValidationRule<TModel> rule)
		{
			AddRuleInternal(rule.Validate, rule.Error);
			return this;
		}

		/// <inheritdoc/>
		public IValidator<TModel> AddRules<TProp>(Func<TModel, TProp> propertySelector, params Type[] rules)
        {
			foreach (var ruleType in rules)
			{
                AddRule(propertySelector, CreateRuleInstance<TProp>(ruleType));
			}

			return this;
        }

		/// <inheritdoc/>
		public ValidationResult Validate(TModel model) =>
			new ValidationResult(
				_validators
					.Select(validate => validate(model))
					.FirstOrDefault(error => error != null));

		protected void AddRuleInternal(Func<TModel, bool> validator, ValidationError result) =>
			_validators.Add((TModel model) => validator(model) ? null : result);

		private IValidationRule<TData> CreateRuleInstance<TData>(Type ruleType) =>
			(IValidationRule<TData>)_activator(ruleType);
	}
}
