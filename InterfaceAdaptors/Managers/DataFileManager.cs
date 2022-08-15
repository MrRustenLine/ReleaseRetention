namespace ReleaseRetention.InterfaceAdaptors.Managers
{
    public class DataFileManager: IDataFileManager
    {
        public string DeploymentsJSON { get; set; }
        public string EnvironmentsJSON { get; set; }
        public string ProjectsJSON { get; set; }
        public string ReleasesJSON { get; set; }

    }
}
