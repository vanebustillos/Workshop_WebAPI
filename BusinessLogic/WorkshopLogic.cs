using System;
using System.Collections.Generic;
using System.Linq;
using workshop_web_api.BusinessLogic;
using workshop_web_api.Database;

namespace workshop_web_api.BusinessLogic
{
    public class WorkshopLogic : IWorkshopLogic
    {
        private IWorkshopTableDB _workshopDB; // DB of Workshop
        public List<Workshop> allWorkshop; //Data of DB

        public WorkshopLogic(IWorkshopTableDB workshopDB)
        {
            _workshopDB = workshopDB;
            allWorkshop = _workshopDB.GetAll();
        }

        public List<WorkshopDTO> Get() { 
            UpdateLocalDB();
            List<WorkshopDTO> workshopList = new List<WorkshopDTO>();
            foreach (Workshop workshop in allWorkshop)
            {
                workshopList.Add(ConvDBtoDTO(workshop));
            }
            return workshopList;
        }

        public WorkshopDTO Post(WorkshopDTO workshop)
        {
            UpdateLocalDB();
            Workshop input = ConvDTOtoDB(workshop);
            input.Id = GenerateId(input);
            _workshopDB.Create(input);
            return workshop;
        }

        private string GenerateId(Workshop input)
        {
            if (allWorkshop.Count == 0)
            {
                return "Workshop-1";
            }
            else
            {
                Workshop lastWorkshop = allWorkshop.Last();
                string[] fracment = lastWorkshop.Id.Split("-");
                int lastId = Int32.Parse(fracment[1]) + 1;
                return "Workshop-" + lastId;
            }
        }

        public void UpdateLocalDB() //Updates the local list of elements used for the operations
        {
            allWorkshop= _workshopDB.GetAll();
        }

        public Workshop ConvDTOtoDB(WorkshopDTO oldWorkshop) //Converts a DTOWorkshop to a DB Workshop
        {
            Workshop validWorkshop = new Workshop();
            if (oldWorkshop.Id != "Workshop-0"){
                validWorkshop.Id = oldWorkshop.Id;
            }
            validWorkshop.Name = oldWorkshop.Name;
            validWorkshop.Status = oldWorkshop.Status;
            return validWorkshop;
        }

        public WorkshopDTO ConvDBtoDTO(Workshop oldWorkshop) //Converts a DB Workshop to a DTOWorkshop
        {
            WorkshopDTO validWorkshop = new WorkshopDTO();
             
            validWorkshop.Id = oldWorkshop.Id;
            validWorkshop.Name = oldWorkshop.Name;
            validWorkshop.Status = oldWorkshop.Status;
            return validWorkshop;
        }
    }
}