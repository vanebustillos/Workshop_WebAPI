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
    }
}