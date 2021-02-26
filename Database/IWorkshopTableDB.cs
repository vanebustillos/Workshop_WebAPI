using System;
using System.Collections.Generic;

namespace Database
{
    public interface IWorkshopTableDB
    {
        Workshop Create(Workshop Workshop);
        void Delete(Workshop Workshop);
        List<Workshop> GetAll();
        void Update(Workshop Workshop);
    }
}