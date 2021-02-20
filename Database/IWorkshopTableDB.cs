using System;
using System.Collections.Generic;
using workshop_web_api.Database;

namespace workshop_web_api.Database
{
    public interface IWorkshopTableDB
    {
        Workshop Create(Workshop Workshop);
        void Delete(Workshop Workshop);
        List<Workshop> GetAll();
        void Update(Workshop Workshop);
    }
}