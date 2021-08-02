using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Extensions
{
    public static class ExtensionMethods
    {
        public static DateTime GetFromDateTime(this DateTime dateFrom)
        {
            return new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 0, 0, 0);
        }

        public static DateTime GetToDateTime(this DateTime dateTo)
        {
            return new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 59, 59);
        }


    }
}