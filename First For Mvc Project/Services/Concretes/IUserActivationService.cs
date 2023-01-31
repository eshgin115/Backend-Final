using Pronia.Database.Models;

namespace Pronia.Services.Concretes
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
