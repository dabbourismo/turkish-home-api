
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using TurkishHomeApi.Helpers;

namespace TurkishHomeApi.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPagenationHeader(this HttpResponse response
            ,int currentPage,int itemsPerPage,int totalItems,int totalPages)
        {
            var pagenationHeader = new PagenationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagenation", JsonSerializer.Serialize(pagenationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagenation");
        }
    }
}