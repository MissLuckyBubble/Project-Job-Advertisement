using SilviqDancheva_2101321099.Entities;
using System.Collections.Generic;

namespace SilviqDancheva_2101321099.ViewModels.JobAds
{
    public class ApplayVM
    {
        public int UserId { get; set; }
        public int JobAdId { get; set; }

        public List<UserToJobAd> Applays { get; set; }
        public List<JobAd> JobAds { get; set; }
    }
}