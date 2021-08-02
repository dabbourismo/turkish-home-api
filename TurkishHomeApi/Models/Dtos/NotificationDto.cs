using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Models.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ApperanceDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LevelId { get; set; }
        public string LevelName { get; set; }
    }
}