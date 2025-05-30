

namespace ConstrainService.Application.DTOs.Response
{
    public class APIResponse<T>
    {
        public required T Data { get; set; }
        public bool Success { get; set; } = true;
    }
}