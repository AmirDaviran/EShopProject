using System.ComponentModel.DataAnnotations;


namespace EShop.Domain.Entities.BaseEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

