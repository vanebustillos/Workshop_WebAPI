using System;
using System.Collections.Generic;
using workshop_web_api.BusinessLogic;

namespace workshop_web_api.BusinessLogic
{
    public interface IWorkshopLogic
    {
        public List<WorkshopDTO> Get();
        public WorkshopDTO Post(WorkshopDTO workshop);
        public void Put(WorkshopDTO workshopToUpdate, string id);
        public void Delete(string id);
        public void CancellWorkshop(string id);
        public void PostponeWorkshop(string id);
    }
}