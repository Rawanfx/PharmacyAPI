using projectAPI.Core.DTO;
using projectAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.ServiceCotract
{
    public interface IAccountServices
    {
        Task<AuthenticationRespose> RegisterAsync(RegisterDTO registerDto);
        Task<AuthenticationRespose> LoginAsync(LoginDto loginDto);
        Task<OperationResult> AddRoleAsync(string role);
        Task<OperationResult> AssignUserRole(UserModelRoleDTO model);
    }
}
