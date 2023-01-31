namespace First_For_Mvc_Project.Exceptions
{
    public class IdentityCookieException : ApplicationException
    {
        public IdentityCookieException(string? message)
           : base(message)
        {

        }
    }
}
