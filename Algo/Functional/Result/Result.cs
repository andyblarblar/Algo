using System;
using System.Security.Cryptography;

namespace Algo.Functional.Result
{
    /// <summary>
    /// An implementation of the Result monad. Consume using <see cref="ResultExtensions"/>.
    /// </summary>
    /// <remarks>Result is an immutable type, and will always be in one of two states: Success or Error. This state
    /// can be determined using IsError. To get the held value of a Result, call either UnwrapSuccess or UnwrapError respectively.
    /// Either of these methods will throw if the Result is in the opposite state, be sure to check IsError before unwrapping.
    /// You can also use callbacks to handle states via OnError and OnSuccess.
    /// </remarks>
    /// <example>
    /// <code>
    /// //creating results
    /// var hi = Success&lt;string,Exception&gt;("hi");
    /// 
    /// //binding results
    /// var err = hi
    /// .FlatMap(s =&gt; Success&lt;string, Exception&gt;("yay! no errors!"))
    /// .FlatMap((s) =&gt; Error&lt;string, Exception&gt;(new Exception("oops! all errors!")))
    /// .FlatMap(s =&gt; Success&lt;string, Exception&gt;(":[")); //never runs because previous bind returns error 
    /// 
    /// //matching with callbacks
    /// err.OnSuccess(s =&gt; Console.WriteLine($"This will never happen! {s}"))
    /// .OnError(e =&gt; Console.WriteLine($"This will! {e}"));
    /// 
    /// //same as above without callbacks
    /// if (!err.IsError)
    /// {
    /// var s = err.UnwrapSuccess(); //will throw if error
    /// Console.WriteLine($"This will never happen! {s}");
    /// }
    /// else
    /// {
    /// var e = err.UnwrapError(); //will throw if success
    /// Console.WriteLine($"this will! {e}");
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="TData">The type of the success data</typeparam>
    /// <typeparam name="TError">The type of the error data</typeparam>
    public readonly struct Result<TData, TError>
    {
        /// <summary>
        /// The success state.
        /// </summary>
        private readonly TData _success;
        
        /// <summary>
        /// The possible error state.
        /// </summary>
        private readonly TError _error;

        /// <summary>
        /// If this Result is in the Error state or Success state
        /// </summary>
        public readonly bool IsError;

        /// <summary>
        /// Creates a result in the success state
        /// </summary>
        public Result(TData data)
        {
            IsError = false;
            _success = data;
            _error = default!; 
        }

        /// <summary>
        /// Creates a result in the error state
        /// </summary>
        public Result(TError error)
        {
            _error = error;
            IsError = true;
            _success = default!;
        }

        /// <summary>
        /// Unwraps the success data
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if the result contains an error</exception>
        public TData UnwrapSuccess()
        {
            return !IsError ? _success : throw new InvalidOperationException("Attempted to unwrap an error as success");
        }
        
        /// <summary>
        /// Unwraps the error data
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if the result contains a success</exception>
        public TError UnwrapError()
        {
            return IsError ? _error : throw new InvalidOperationException("Attempted to unwrap a success as error");
        }

        /// <summary>
        /// Executes the passed lambda if this result is in the success state, else does nothing.
        /// </summary>
        public Result<UData, TError> SelectMany<UData>(Func<TData, Result<UData, TError>> f)
        {
            return !IsError
                ? f(_success)
                : new Result<UData, TError>(_error);
        }
        
        /// <summary>
        /// Executes the passed lambda if this result is in the success state, else does nothing.
        /// </summary>
        public Result<UData, TError> FlatMap<UData>(Func<TData, Result<UData, TError>> f)
        {
            return SelectMany(f);
        }


        public delegate void ErrorHandle(TError error);
        public delegate void SuccessHandle(TData data);

        /// <summary>
        /// Eagerly invokes the callback if in the error state.
        /// </summary>
        public Result<TData,TError> OnError(ErrorHandle handle)
        {
            if (IsError) handle(_error);
            return this;
        }

        /// <summary>
        /// Eagerly invokes the callback if in the success state.
        /// </summary>
        public Result<TData, TError> OnSuccess(SuccessHandle handle)
        {
            if (!IsError) handle(_success);
            return this;
        }

        /// <summary>
        /// Unwraps a success case or returns the passed alternate. 
        /// </summary>
        public TData UnwrapSuccessOr(TData alternate)
        {
            return IsError
                ? alternate
                : _success;
        }
        
        /// <summary>
        /// Unwraps a error case or returns the passed alternate. 
        /// </summary>
        public TError UnwrapErrorOr(TError alternate)
        {
            return !IsError
                ? alternate
                : _error;
        }

    }

    /// <summary>
    /// Result type for functions that have no meaningful return value. see <see cref="Result{TData,TError}"/> for more docs.
    /// </summary>
    /// <remarks>Due to Result being a struct, the param-less constructor acts as the success case constructor.</remarks>
    /// <typeparam name="TError"></typeparam>
    public readonly struct Result<TError>
    {
        private readonly TError _error;

        public readonly bool IsError;

        public Result(TError err)
        {
            IsError = true;
            _error = err;
        }

        /// <summary>
        /// returns the internal error
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if there is no error</exception>
        public TError Unwrap()
        {
            return IsError ? _error : throw new InvalidOperationException("cannot unwrap if there is no error");
        }

        /// <summary>
        /// Executes the passed lambda if this result is in the success state, else does nothing.
        /// </summary>
        public Result<TError> SelectMany(Func<Result<TError>> f)
        {
            return !IsError
                ? f()
                : new Result<TError>(_error);
        }

        /// <summary>
        /// Executes the passed lambda if this result is in the success state, else does nothing.
        /// </summary>
        public Result<TError> FlatMap(Func<Result<TError>> f)
        {
            return SelectMany(f);
        }

        public delegate void ErrorHandle(TError error);

        /// <summary>
        /// Eagerly invokes the callback if in the error state.
        /// </summary>
        public Result<TError> OnError(ErrorHandle handle)
        {
            if (IsError) handle(_error);
            return this;
        }

        /// <summary>
        /// Eagerly invokes the callback if in the success state.
        /// </summary>
        public Result<TError> OnSuccess(Action handle)
        {
            if(!IsError) handle.Invoke();
            return this;
        }


    }

    /// <summary>
    /// Factory methods for creating <see cref="Result{TData,TError}"/> without redundant generics.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Creates a new Result in the Success state
        /// </summary>
        public static Result<TData, TError> Success<TData,TError>(TData data) => new Result<TData, TError>(data);

        /// <summary>
        /// Creates a new Result in the Error state
        /// </summary>
        public static Result<TData, TError> Error<TData, TError>(TError error) => new Result<TData, TError>(error);
        
        /// <summary>
        /// Creates a new Result in the Success state
        /// </summary>
        public static Result<TError> Success<TError>() => new Result<TError>();

        /// <summary>
        /// Creates a new Result in the Error state
        /// </summary>
        public static Result<TError> Error<TError>(TError error) => new Result<TError>(error);

    }



}
