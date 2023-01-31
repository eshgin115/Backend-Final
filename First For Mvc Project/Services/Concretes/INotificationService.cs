namespace First_For_Mvc_Project.Services.Concretes
{
    public interface INotificationService
    {
        Task SenOrderCreatedToAdmin(string trackingCode);
    }
}
