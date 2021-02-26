using System;
using System.Collections.Generic;

namespace Database
{
    public interface IWorkshopTableDB
    {
        Workshop Create(Workshop workshop);
        Workshop Delete(Workshop workshop);
        List<Workshop> GetAll();
        Workshop Update(Workshop workshop);
    }
}