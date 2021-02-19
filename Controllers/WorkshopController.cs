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
            Console.WriteLine("from post => " + workshop.Id + " - " + workshop.Name + " - " + workshop.Status);
            _workshopLogic.Post(workshop);

            var dbServer = _configuration.GetSection("Database").GetSection("ServerName");
            workshop.Name = $"{workshop.Name} data from {dbServer.Value}";
            
            return workshop;
        }
    }
}