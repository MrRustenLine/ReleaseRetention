using ReleaseRetention.InterfaceAdaptors.Managers;
using ReleaseRetention.UseCases;
using ReleaseRetention.Entities;

namespace TestReleaseRetention
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            DataFileManager.EnvironmentsFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "Environments.json";
            DataFileManager.ProjectsFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "Projects.json";
        }

        [Test]
        public void Test0a()
        {
            DataFileManager.ReleasesFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "Releases.json";
            DataFileManager.DeploymentsFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "Deployments.json";
            DataFileManager.LoadData();
            
            Assert.AreEqual(DataFileManager.Deployments.Count, 10);
            Assert.AreEqual(DataFileManager.Environments.Count, 2);
            Assert.AreEqual(DataFileManager.Projects.Count, 2);
            Assert.AreEqual(DataFileManager.Releases.Count, 8);
        }

        [Test]
        public void Test0b()
        {
            BusinessManager mgr = new BusinessManager(DataFileManager.Releases, DataFileManager.Deployments, DataFileManager.Environments, DataFileManager.Projects);
            
            Assert.IsNotNull(mgr);
        }

        [Test]
        public void Test1a()
        {
            DataFileManager.ReleasesFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "ReleasesTest1a.json";
            DataFileManager.DeploymentsFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "DeploymentsTest1a.json";
            DataFileManager.LoadData();
            BusinessManager mgr = new BusinessManager(DataFileManager.Releases, DataFileManager.Deployments, DataFileManager.Environments, DataFileManager.Projects);
            List<Release> releasesRetained = mgr.RetainReleases(1);
            Assert.AreEqual(1, releasesRetained.Count);
            Assert.That(releasesRetained[0].Id == "Release-1");
        }

        [Test]
        public void Test1b()
        {
            DataFileManager.ReleasesFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "ReleasesTest1b.json";
            DataFileManager.DeploymentsFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "DeploymentsTest1b.json";
            DataFileManager.LoadData();
            BusinessManager mgr = new BusinessManager(DataFileManager.Releases, DataFileManager.Deployments, DataFileManager.Environments, DataFileManager.Projects);
            List<Release> releasesRetained = mgr.RetainReleases(1);

            Assert.AreEqual(1, releasesRetained.Count);
            Assert.That(releasesRetained[0].Id == "Release-1");
        }

        [Test]
        public void Test2a()
        {
            DataFileManager.ReleasesFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "ReleasesTest2a.json";
            DataFileManager.DeploymentsFile = @"C:\Users\Rusten\source\repos\ReleaseRetention\InterfaceAdaptors\Models\" + "DeploymentsTest2a.json";
            DataFileManager.LoadData();
            BusinessManager mgr = new BusinessManager(DataFileManager.Releases, DataFileManager.Deployments, DataFileManager.Environments, DataFileManager.Projects);
            List<Release> releasesRetained = mgr.RetainReleases(1);

            Assert.AreEqual(2, releasesRetained.Count);
            Assert.That(releasesRetained[0].Id == "Release-2");
            Assert.That(releasesRetained[1].Id == "Release-1");
        }
    }
}