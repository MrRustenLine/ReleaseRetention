using ReleaseRetention.Entities;
using ReleaseRetention.InterfaceAdaptors.Managers;

namespace ReleaseRetention.UseCases
{
    public class BusinessManager
    {
        List<Release> _releases;
        List<Deployment> _deployments;

        public BusinessManager(List<Release> releases, List<Deployment> deployments)
        {
            _releases = releases;
            _deployments = deployments;
        }

        public List<Release>RetainReleases(int number)
        {
            List<Release> releases = new List<Release>();
            return releases;
        }
    }
}
