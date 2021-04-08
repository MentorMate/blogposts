using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidationExample.ExtensionsValidation.Results
{
    /// <summary>A static result class.</summary>
    public static class Result
    {
        /// <summary>Create an Result from data.</summary>
        /// <typeparam name="TResult">The type of the result data.</typeparam>
        public static Result<TResult> Create<TResult>(TResult data) =>
            new Result<TResult>(data);

        /// <summary>Create an Result with single object.</summary>
        /// <typeparam name="TResult">The type of the single result data.</typeparam>
        public static Result<TResult> CreateResultWithError<TResult>(ResultCompleteTypes status, params string[] messages) =>
            new Result<TResult>(default, status, messages);

        /// <summary>Convert entity result to model result.</summary>
        /// <typeparam name="TEntity">The entity object.</typeparam>
        /// <typeparam name="TModel">The model object.</typeparam>
        public static Result<TModel> Map<TEntity, TModel>(this Result<TEntity> entity, Func<TEntity, TModel> converter) =>
            entity.Status == ResultCompleteTypes.Success ?
            new Result<TModel>(converter(entity.Data)) :
            new Result<TModel>(default, entity.Status, entity.Messages);

        /// <summary>Execute a asynchronius operation.</summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <typeparam name="TSource">The source type.</typeparam>
        public static async Task<Result<TResult>> MapAsync<TResult, TSource>(this Result<TSource> result, Func<TSource, Task<TResult>> pipelineAction)
        {
            if (result.Status == ResultCompleteTypes.Success)
            {
                var data = await pipelineAction(result.Data);
                return new Result<TResult>(data);
            }

            return new Result<TResult>(default, result.Status, result.Messages);
        }

        /// <summary>Convert an result entity to model.</summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        public static async Task<Result<TModel>> MapAsync<TEntity, TModel>(this Task<Result<TEntity>> entityTask, Func<TEntity, TModel> converter) =>
            await MapAsync(await entityTask, entity => Task.FromResult(converter(entity)));

        /// <summary>Convert an result entity to model.</summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        public static async Task<Result<TModel>> MapAsync<TEntity, TModel>(this Task<Result<TEntity>> entityTask, Func<TEntity, Task<TModel>> converterAsync) =>
            await MapAsync(await entityTask, converterAsync);

        /// <summary>Convert entity list result to model list result.</summary>
        /// <typeparam name="TEntity">The entity object.</typeparam>
        /// <typeparam name="TModel">The model object.</typeparam>
        public static Result<IReadOnlyList<TModel>> MapList<TEntity, TModel>(this Result<IReadOnlyList<TEntity>> entity, Func<TEntity, TModel> converter) =>
            entity.Map(it => (IReadOnlyList<TModel>)it.Select(converter).ToList());

        /// <summary>Validates the specified condition.</summary>
        public static Result<bool> Validate(bool condition, ResultCompleteTypes status, string message) =>
            condition ? new Result<bool>(true) : CreateResultWithError<bool>(status, message);

        /// <summary>Validates the specified condition.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        public static Result<TResult> Validate<TResult>(this Result<TResult> result, bool condition, ResultCompleteTypes status, string message) =>
            condition ? result : CreateResultWithError<TResult>(status, CombineArray(result?.Messages, message));

        /// <summary>Validates the specified condition.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        public static Result<TResult> Validate<TResult>(
            this Result<TResult> result,
            Predicate<TResult> predicate,
            ResultCompleteTypes status,
            string message) =>
            result.IsSuccessfulStatus() ? Validate(result, predicate(result.Data), status, message) : result;

        /// <summary>Validates the specified condition.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        public static async Task<Result<TResult>> ValidateAsync<TResult>(
            this Task<Result<TResult>> entityTask,
            Predicate<TResult> predicate,
            ResultCompleteTypes status,
            string message) =>
            Validate(await entityTask, predicate, status, message);

        private static string[] CombineArray(IEnumerable<string> messages, string message)
        {
            var messageArray = new[] { message };
            return messages?.Concat(messageArray).ToArray() ?? messageArray;
        }
    }
}
