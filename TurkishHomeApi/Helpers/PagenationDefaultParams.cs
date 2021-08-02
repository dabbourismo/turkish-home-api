using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkishHomeApi.Helpers
{
    public class PagenationDefaultParams
    {       
        public int PageNumber { get; set; } = 1;

        private const int MaxPageSize = 50;

        private int _PageSize;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = (value > MaxPageSize) ? MaxPageSize : value;  }
        }


    }
}