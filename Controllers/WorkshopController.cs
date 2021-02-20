using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using workshop_web_api.BusinessLogic;

namespace workshop_web_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private IWorkshopLogic _workshopLogic; //To be added
        private IConfiguration _configuration;

        public WorkshopController(IWorkshopLogic workshopLogic, IConfiguration configuration)
        {
            _workshopLogic = workshopLogic;
            _configuration = configuration;
        }

        // GET: api/workshops
        [HttpGet]
        [Route("workshops")]
        public IEnumerable<WorkshopDTO> GetAll()
        {
            return _workshopLogic.Get();
        }

        // POST: api/workshops
        [HttpPost]
        [Route("workshops")]
        public WorkshopDTO Post([FromBody] WorkshopDTO workshop)
        {
            _workshopLogic.Post(workshop);

            var dbServer = _configuration.GetSection("Database").GetSection("ServerName");
            workshop.Name = $"{workshop.Name} data from {dbServer.Value}";

            return workshop;
        }

        // PUT: api/workshops/Workshop-4
        [HttpPut]
        [Route("workshops/{id}")]
        public void Put([FromBody]WorkshopDTO workshop, string id)
        {
            _workshopLogic.Put(workshop, id);
        }

        // DELETE: api/workshops/Workshop-4
        [HttpDelete]
         [Route("workshops/{id}")]
        public void Delete(string id)
        {
             _workshopLogic.Delete(id);
        }
    }
}