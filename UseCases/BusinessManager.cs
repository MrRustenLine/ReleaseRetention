using ReleaseRetention.Entities;
using ReleaseRetention.InterfaceAdaptors.Managers;
using System.Collections.Generic;
using System.Linq;

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

        public List<Release>RetainReleases(int nReleases)
        {
            var ds = from r in _releases
                      join d in _deployments on r.Id equals d.ReleaseId
                      join p in _projects on r.ProjectId equals p.Id
                      join e in _environments on d.EnvironmentId equals e.Id
                      select new
                      {
                          d.ReleaseId,
                          r.ProjectId,
                          r.Version,
                          r.Created,
                          DeploymentId = d.Id,
                          d.EnvironmentId,
                          d.DeployedAt,
                          EnvironmentName = e.Name,
                          ProjectName = p.Name,
                      };
            List<Release> results = new List<Release>();
            foreach(Project project in _projects)
            {
                foreach(ReleaseRetention.Entities.Environment environment in _environments)
                {
                    var shortList = (from r in ds
                                where string.Equals(r.ProjectId, project.Id) && string.Equals(r.EnvironmentId, environment.Id)
                                orderby r.DeployedAt descending
                                select r).Take(nReleases);
                    foreach(var r in shortList)
                    {
                        Release release = new Release();
                        release.ProjectId = project.Id;
                        release.Id = r.ReleaseId;
                        release.Version = r.Version;
                        release.Created = r.Created;
                        results.Add(release);
                        //"Log why a release should be kept" `Release-1` kept because it was the most recently deployed to `Environment-2`
                    }
                }
            }
            return results;
        }
    }
}
