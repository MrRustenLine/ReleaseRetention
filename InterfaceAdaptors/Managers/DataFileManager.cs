using Microsoft.Extensions.Configuration;
using ReleaseRetention.Entities;
using Newtonsoft.Json;

namespace ReleaseRetention.InterfaceAdaptors.Managers
{
    public static class DataFileManager
    {
        public static string ReleasesFile { get; set; }
        public static string DeploymentsFile { get; set; }
        //public string EnvironmentsJSON { get; set; }
        //public string ProjectsJSON { get; set; }

        //public static List<Release>Releases { get; set; }
        //public static List<Deployment> Deployments { get; set; }
        public static void ReadConfig()
        {

        }
        public static List<Release> GetReleases()
        {
            string jsonString = Read(ReleasesFile);
            return JsonConvert.DeserializeObject<List<Release>>(jsonString);
        }

        public static List<Deployment> GetDeployments()
        {
            string jsonString = Read(DeploymentsFile);
            return JsonConvert.DeserializeObject<List<Deployment>>(jsonString);
        }

        public static string Read(string fileName)
        {
            //fileName = full path including extension
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }
            else
            {
                throw new Exception(fileName + " does not exist.");
            }
        }
    }
}
