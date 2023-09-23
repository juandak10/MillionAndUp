using System;

namespace Domain.References
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? ExceptionType { get; set; }
        public string? StackTrace { get; set; }
    }
}
