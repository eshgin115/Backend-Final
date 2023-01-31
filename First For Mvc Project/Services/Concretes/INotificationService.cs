namespace Pronia.Services.Concretes
{
    public interface INotificationService
    {
        Task SenOrderCreatedToAdmin(string trackingCode);
    }
}
