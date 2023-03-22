namespace Backend.Core.Common.ErrorHandlers
{
    public class Result
    {
        public List<Error> Errors { get; set; }
        public bool IsError => Errors != null && Errors.Any();
    }

    public class Results<T> : Result
    {
        public T ? Response { get; set; }
        public string ? WarningMessage { get; set; }
    }
}
