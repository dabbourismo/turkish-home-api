using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Models.Business
{
    public class Material
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LevelId { get; set; }
        public Level Level { get; set; }
    }
}