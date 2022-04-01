using SilviqDancheva_2101321099.Entities;
using SilviqDancheva_2101321099.ViewModels.Shared;
using System.Collections.Generic;

namespace SilviqDancheva_2101321099.ViewModels.JobAds
{
    public class IndexVM
    {
        public List<JobAd> Items { get; set; }

        public JobAd JobAd { get; set; }
        public List<User> Users { get; set; }

        public PagerVM Pager { get; set; }
        public FilterVM Filter { get; set; }
    }
}