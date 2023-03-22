namespace Frontend.DTO.Response.Common
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string? InnerException { get; set; }
        public string? ErrorType { get; set; }
    }
}
