using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BusinessLogic;

namespace workshop_web_api.Controllers
{
    [Route("api/workshops")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private IWorkshopLogic _workshopLogic;
        private IConfiguration _configuration;

        public WorkshopController(IWorkshopLogic workshopLogic, IConfiguration configuration)
        {
            _workshopLogic = workshopLogic;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<WorkshopDTO> GetAll()
        {
            return _workshopLogic.Get();
        }

        [HttpPost]
        public WorkshopDTO Post([FromBody] WorkshopDTO workshop)
        {
            return _workshopLogic.Post(workshop);
        }

        [HttpPut]
        [Route("{workshopId}")]
        public WorkshopDTO Put([FromBody]WorkshopDTO workshop, string id)
        {
            return _workshopLogic.Put(workshop, id);
        }

        [HttpDelete]
         [Route("{workshopId}")]
        public WorkshopDTO Delete(string id)
        {
             return _workshopLogic.Delete(id);
        }

        [HttpPost]
        [Route("{workshopId}/cancel")]
        public WorkshopDTO Cancell(string id)
        {
            return _workshopLogic.Cancel(id);
        }

        [HttpPost]
        [Route("{workshopId}/postpone")]
        public WorkshopDTO Postpone(string id)
        {
            return _workshopLogic.Postpone(id);
        }
    }
}