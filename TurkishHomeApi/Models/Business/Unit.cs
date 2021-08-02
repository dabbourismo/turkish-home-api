using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Models.Business
{
    public class Unit
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}