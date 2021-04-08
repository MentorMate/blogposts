namespace ValidationExample.ValidationPattern.Validation
{
    /// <summary>The validation error information.</summary>
    public class ValidationError
	{
        /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
        public ValidationError(string code, string message)
		{
			Code = code;
			Message = message;
		}

        /// <summary>Gets the error code.</summary>
        public string Code { get; private set; }

        /// <summary>Gets or sets the message.</summary>
        public string Message { get; set; }
	}
}
