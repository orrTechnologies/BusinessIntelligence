using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessInsights.Models
{
    public class FacebookAreaChartViewModel
    {
        public DateTime Day { get; set; }
        public int PositivePostCount { get; set; }
        public int NegativePostCount { get; set; }
        public int NeutralPostCount { get; set; }
    }

    public class FacebookDonutChartViewModel
    {
        public int PositivePostCount { get; set; }
        public int NegativePostCount { get; set; }
        public int NeutralPostCount { get; set; }
    }
}