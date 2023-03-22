namespace Backend.Core.Common.ErrorHandlers
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string? InnerException { get; set; }
        public string? ErrorType { get; set; }
    }
}
