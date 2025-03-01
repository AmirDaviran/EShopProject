﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Utilities.Convertors.Date
{
    public static class DateExtensions
    {
        public static string ToShamsi1(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);

            return $"{year}/{month.ToString("00")}/{day.ToString("00")}";
        }
    }
}
