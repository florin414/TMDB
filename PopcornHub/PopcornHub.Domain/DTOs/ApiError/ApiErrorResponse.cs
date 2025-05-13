using PopcornHub.Domain.Common;

namespace PopcornHub.Domain.DTOs.ApiError;

public class ApiErrorResponse
{
    public int StatusCode { get; init; }
    
    public string Error { get; init; }
}