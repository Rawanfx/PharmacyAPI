using projectAPI.Core.Domain.IdentityEntities;
using projectAPI.Core.DTO;

namespace projectAPI.Core.ServiceCotract
{
    public interface IJwtServices
    {
       Task<AuthenticationRespose> CreateTokenAsync(ApplicationUser user);
    }
}
