namespace First_For_Mvc_Project.Contracts.Order
{
    public enum Status
    {
        Created = 0,
        Confirmed = 1,
        Rejected = 2,
        Submitted = 4,
        Completed = 8,
    }
    public static class StatusCodeExtensions
    {
        public static string GetShortNameWithStatus(this Status status)
        {
            switch (status)
            {
                case 0:
                    return "Created";
                case Status.Confirmed:
                    return "Confirmed";
                case Status.Rejected:
                    return "Rejected";
                case Status.Submitted:
                    return "Submitted";
                case Status.Completed:
                    return "Completed";



                default:
                    throw new Exception("Status  not found");
            }

        }
        public static string GetNotification(this Status status,string firstName, string lastName,string order_number)
        {
            switch (status)
            {
                case Status.Created:
                    return  $" Hörmətli {firstName} {lastName}, sizin {order_number} nömrəli sifariş təsdiqləndi.";
           
                case Status.Confirmed:
                    return $"Hörmətli {firstName} {lastName}, sizin {order_number} nömrəli sifariş təsdiqlənmədi.";

                case Status.Submitted:
                    return $"Hörmətli {firstName} {lastName}, sizin {order_number} nömrəli sifariş göndərildi, kuryer sizinlə əlaqə saxlayacaq.";
                case Status.Completed:
                    return $"Hörmətli {firstName} {lastName}, sizin {order_number} nömrəli sifariş kuryer tərəfindən təhvil verildi.";



                default:
                    throw new Exception("Status  not found");
            }

        }
    }
}
