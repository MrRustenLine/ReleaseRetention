using ReleaseRetention.Entities;
using ReleaseRetention.InterfaceAdaptors.Managers;

namespace ReleaseRetention.UseCases
{
    public class BusinessManager
    {
        List<Release> _releases;
        List<Deployment> _deployments;
        List<ReleaseRetention.Entities.Environment> _environments;
        List<Project> _projects;


        public BusinessManager(List<Release> releases, List<Deployment> deployments, List<ReleaseRetention.Entities.Environment> environments, List<Project> projects)
        {
            _releases = releases;
            _deployments = deployments;
            _environments = environments;
            _projects = projects;
        }

        public List<Release>RetainReleases(int number)
        {
            //
            return _releases;
        }
    }
}
