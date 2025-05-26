using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
public class AuthResponseDto
{
    public required string Token { get; set; }
    public required string Role { get; set; }
    public bool IsSuccess { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}


}
