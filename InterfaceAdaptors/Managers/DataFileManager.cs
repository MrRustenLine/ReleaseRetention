using Microsoft.Extensions.Configuration;
using ReleaseRetention.Entities;
using Newtonsoft.Json;

namespace ReleaseRetention.InterfaceAdaptors.Managers
{
    public static class DataFileManager
    {
        public static string ReleasesFile { get; set; }
        public static string DeploymentsFile { get; set; }
        public static string EnvironmentsFile { get; set; }
        public static string ProjectsFile { get; set; }

        private static List<Release> releases;
        private static List<Deployment> deployments;
        private static List<ReleaseRetention.Entities.Environment> environments;
        private static List<Project> projects;

        public static List<Release>Releases { 
            get
            {
                return releases == null ? null : releases;
            }
        }

        public static List<ReleaseRetention.Entities.Environment> Environments
        {
            get
            {
                return environments == null ? null : environments;
            }
        }

        public static List<Project> Projects
        {
            get
            {
                return projects == null ? null : projects;
            }
        }

        public static List<Deployment> Deployments
        {
            get
            {
                return deployments == null ? null : deployments;
            }
        }

        public static void LoadData()
        {
            string jsonString = Read(ReleasesFile);
            releases = JsonConvert.DeserializeObject<List<Release>>(jsonString);

            jsonString = Read(DeploymentsFile);
            deployments = JsonConvert.DeserializeObject<List<Deployment>>(jsonString);

            jsonString = Read(EnvironmentsFile);
            environments = JsonConvert.DeserializeObject<List<ReleaseRetention.Entities.Environment>>(jsonString);

            jsonString = Read(ProjectsFile);
            projects = JsonConvert.DeserializeObject<List<Project>>(jsonString);

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
