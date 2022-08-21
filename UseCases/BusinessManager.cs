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

        public List<Release>RetainReleases(int number)
        {
            var ReleasesDeployments = from r in _releases
                                      join d in _deployments on r.Id equals d.ReleaseId
                                      orderby r.Id, d.DeployedAt descending
                                      select new
                                      {
                                          r.Id,
                                          r.ProjectId,
                                          r.Version,
                                          r.Created,
                                          d.DeployedAt
                                      };
            //var list = (from t in ctn.Items
            //            where t.DeliverySelection == true && t.Delivery.SentForDelivery == null
            //            orderby t.Delivery.SubmissionDate
            //            select t).Take(5);
            //foreach (var releasesResult in ReleasesDeployments)
            //{
            //    OrdersCountByUser ocbu = new OrdersCountByUser();
            //    ocbu.name = orderGroup.Key;
            //    ocbu.count = orderGroup.Count();
            //    ordersCountByUser.Add(ocbu);
            //}

            return _releases;
        }
    }
}
