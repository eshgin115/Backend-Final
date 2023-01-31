using Pronia.Contracts.Email;

namespace Pronia.Services.Concretes
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
