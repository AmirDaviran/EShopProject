using System.Security.Claims;
using System.Security.Principal;

namespace EShop.Application.Utilities.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        #region Get FirstName From Claims
        public static string GetFirstName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
        }
        #endregion

        #region Get LastName From Claims
        public static string GetLastName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
        }
        #endregion

        #region Get UserID From Claims
        public static int GetUserID(this ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Parse the string to int or return 0/default value if not found or parsing fails  
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        public static int GetUserID(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUserID();
        }
        #endregion

        #region Get PhoneNumber From Claims
        public static string GetPhoneNumber(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "Phone-Number")?.Value;
        }
        #endregion

    }
}
