using System.Collections.Generic;

namespace ValidationExample.ExtensionsValidation.Results
{
    /// <summary>The result of an operation.</summary>
    /// <typeparam name="TResult">The type of the result data.</typeparam>
    public class Result<TResult>
    {
        /// <summary>Initializes a new instance of the <see cref="Result{TResult}"/> class.</summary>
        public Result(TResult data, ResultCompleteTypes status, ICollection<string> messages)
        {
            Data = data;
            Status = status;
            Messages = messages;
        }

        /// <summary>Initializes a new instance of the <see cref="Result{TResult}"/> class.</summary>
        public Result(TResult data)
            : this(data, ResultCompleteTypes.Success, null)
        {
        }

        /// <summary>Gets the result.</summary>
        public TResult Data { get; private set; }

        /// <summary>Gets the status.</summary>
        public ResultCompleteTypes Status { get; private set; }

        /// <summary>Gets the message.</summary>
        public ICollection<string> Messages { get; private set; }

        /// <summary>Determines whether [is successful status].</summary>
        public bool IsSuccessfulStatus() =>
            Status == ResultCompleteTypes.Success;
    }
}
