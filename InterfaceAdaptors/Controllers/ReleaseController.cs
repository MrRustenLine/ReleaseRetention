using Microsoft.AspNetCore.Mvc;
using ReleaseRetention.Entities;
using ReleaseRetention.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReleaseRetention.InterfaceAdaptors.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReleaseRetention.Interface_Adaptors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseController : ControllerBase
    {
        // GET api/<ReleasesController>/5
        [HttpGet("{nReleases}")]
        public async Task<ActionResult<IEnumerable<Release>>> GetReleases(int nReleases)
        {
            BusinessManager mgr = new BusinessManager(DataFileManager.Releases, DataFileManager.Deployments, DataFileManager.Environments, DataFileManager.Projects);
            //return releases to be retained
            return mgr.RetainReleases(nReleases);
        }

        //// POST api/<ReleasesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ReleasesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReleasesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
