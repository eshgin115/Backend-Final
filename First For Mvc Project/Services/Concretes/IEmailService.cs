using First_For_Mvc_Project.Contracts.Email;

namespace First_For_Mvc_Project.Services.Concretes
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
