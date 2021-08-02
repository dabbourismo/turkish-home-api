using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Models.Dtos
{
    public class MaterialDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LevelId { get; set; }
        public string LevelName { get; set; }
    }
}