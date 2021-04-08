namespace ValidationExample.ValidationPattern.Validation
{
    /// <summary>An validation rule.</summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IValidationRule<TModel>
	{
        /// <summary>Gets the invalid error.</summary>
        ValidationError Error { get; }

        /// <summary>Validates the specified model.</summary>
        bool Validate(TModel model);
	}
}
