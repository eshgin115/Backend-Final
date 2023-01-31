using First_For_Mvc_Project.Database.Models;

namespace First_For_Mvc_Project.Services.Concretes
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
