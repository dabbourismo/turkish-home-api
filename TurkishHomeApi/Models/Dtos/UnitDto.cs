using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Models.Dtos
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LevelName { get; set; }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
    }
}