namespace ConferenceRoomBooking.Bll.Common.Responces
{
    public class Result
    {
        public ResultState State { get; protected set; }

        public Exception? Exception { get; protected set; }

        public Result()
        {
            State = ResultState.Success;
        }

        public Result(Exception exception)
        {
            State = ResultState.Failure;
            Exception = exception;
        }

        public TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> failure)
        {
            return State switch
            {
                ResultState.Success => success(),
                ResultState.Failure => failure(Exception!),
                _ => throw new InvalidOperationException()
            };
        }

        public TResult MatchSuccess<TResult>(Func<TResult> success)
        {
            return State switch
            {
                ResultState.Success => success(),
                _ => throw new InvalidOperationException()
            };
        }

        public TResult MatchFailure<TResult>(Func<Exception, TResult> failure)
        {
            return State switch
            {
                ResultState.Failure => failure(Exception!),
                _ => throw new InvalidOperationException()
            };
        }

        public void Match(Action success, Action<Exception> failure)
        {
            if (State == ResultState.Success)
            {
                success();
            }
            else if (State == ResultState.Failure)
            {
                failure(Exception!);
            }
        }

        public void MatchSuccess(Action success)
        {
            if (State == ResultState.Success)
            {
                success();
            }
        }

        public void MatchFailure(Action<Exception> failure)
        {
            if (State == ResultState.Failure)
            {
                failure(Exception!);
            }
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; protected set; }

        public Result(T value) : base()
        {
            Value = value;
        }

        public Result(Exception exception) : base(exception) { }

        public TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure)
        {
            return State switch
            {
                ResultState.Success => success(Value!),
                ResultState.Failure => failure(Exception!),
                _ => throw new InvalidOperationException()
            };
        }

        public TResult MatchSuccess<TResult>(Func<T, TResult> success)
        {
            return State switch
            {
                ResultState.Success => success(Value!),
                _ => throw new InvalidOperationException()
            };
        }

        public void Match(Action<T> success, Action<Exception> failure)
        {
            if (State == ResultState.Success)
            {
                success(Value!);
            }
            else if (State == ResultState.Failure)
            {
                failure(Exception!);
            }
        }

        public void MatchSuccess(Action<T> success)
        {
            if (State == ResultState.Success)
            {
                success(Value!);
            }
        }
    }

    public enum ResultState
    {
        Success,
        Failure
    }
}
