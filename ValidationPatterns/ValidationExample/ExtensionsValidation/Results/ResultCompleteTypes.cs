namespace ValidationExample.ExtensionsValidation.Results
{
    /// <summary>The operation status types.</summary>
    public enum ResultCompleteTypes
    {
        /// <summary>The success status</summary>
        Success,

        /// <summary>One or more object where no found</summary>
        NotFound,

        /// <summary>When one or more argument was invalid.</summary>
        InvalidArgument,

        /// <summary>When the operation failed.</summary>
        OperationFailed,
    }
}