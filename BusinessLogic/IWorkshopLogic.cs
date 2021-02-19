using System;
using System.Collections.Generic;
using workshop_web_api.BusinessLogic;

namespace workshop_web_api.BusinessLogic
{
    public interface IWorkshopLogic
    {
        public List<WorkshopDTO> Get();
    }
}