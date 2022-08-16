using Microsoft.Extensions.Configuration;
using ReleaseRetention.Entities;

namespace ReleaseRetention.InterfaceAdaptors.Managers
{
    public static class DataFileManager
    {
        public static List<Release>Releases { get; set; }
        public static List<Deployment> Deployments { get; set; }
        //public string DeploymentsJSON { get; set; }
        //public string EnvironmentsJSON { get; set; }
        //public string ProjectsJSON { get; set; }
        //public string ReleasesJSON { get; set; }

    }
}
