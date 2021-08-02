using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TurkishHomeApi.Models.Enums;

namespace TurkishHomeApi.Models.Business
{
    public class Exam
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public EnumExamType ExamType { get; set; }

        public DateTime ApperanceDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Url { get; set; }
    }
}