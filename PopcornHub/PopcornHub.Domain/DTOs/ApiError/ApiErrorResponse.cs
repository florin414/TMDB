using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.ApiError;

public class ApiErrorResponse : EntityDto
{
    public int StatusCode { get; set; }
    public string Error { get; set; }

    public ApiErrorResponse(int statusCode, string error)
    {
        StatusCode = statusCode;
        Error = error;
    }
}