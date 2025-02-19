using EShop.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.Account
{
    public class User : BaseEntity
    {
        #region Properties


        public string? FirstName { get; set; }


        public string? LastName { get; set; }


        public string? PhoneNumber { get; set; }


        public string? Email { get; set; }


        public string Password { get; set; } = string.Empty;


        public string? Avatar { get; set; }


        public string? ActiveCode { get; set; }


        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; } = false;

        #endregion

        #region Relations

        #endregion



    }
}
