namespace First_For_Mvc_Project.Database.Models.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
