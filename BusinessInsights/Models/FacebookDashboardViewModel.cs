using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessInsights.Models
{
    public class FacebookDashboardViewModel
    {
        public FacebookProfileViewModel Page { get; set; }
        public IEnumerable<FacebookPostAnalysed> Posts { get; set; }
        public IEnumerable<FacebookAreaChartViewModel> AreaChartDataViewModel { get; set; }
        public FacebookDonutChartViewModel DonoutChartDataViewModel { get; set; }

    }
}