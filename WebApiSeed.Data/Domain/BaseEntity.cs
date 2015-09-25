namespace WebApiSeed.Data.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}