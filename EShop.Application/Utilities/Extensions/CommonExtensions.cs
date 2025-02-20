using EShop.Domain.Enums.TicketEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Utilities.Extensions
{
    public static class CommonExtentions
    {
        public static string GetEnumName(this System.Enum dataEnum)
        {
            var enumDisplayName = dataEnum.GetType().GetMember(dataEnum.ToString()).FirstOrDefault();

            if (enumDisplayName != null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();
            }

            return "";
        }

        public static string GetBadgeClass(this TicketStatus status)
        {
            return status switch
            {
                TicketStatus.Pending => "bg-warning",
                TicketStatus.Answered => "bg-success",
                TicketStatus.Closed => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}
