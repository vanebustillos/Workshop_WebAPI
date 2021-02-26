using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IWorkshopLogic
    {
        public List<WorkshopDTO> Get();
        public WorkshopDTO Post(WorkshopDTO workshop);
        public WorkshopDTO Put(WorkshopDTO workshopToUpdate, string id);
        public WorkshopDTO Delete(string id);
        public WorkshopDTO Cancel(string id);
        public WorkshopDTO Postpone(string id);
    }
}