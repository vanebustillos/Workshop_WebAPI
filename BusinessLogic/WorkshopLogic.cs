using System;
using System.Collections.Generic;
using System.Linq;
using Database;

namespace BusinessLogic
{
    public class WorkshopLogic : IWorkshopLogic
    {
        private IWorkshopTableDB _workshopDB;
        public List<Workshop> allWorkshop;

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
            
            if(ValidateInput(workshop)){
                Workshop input = ConvDTOtoDB(workshop);
                input.Id = GenerateId(input);
                _workshopDB.Create(input);  
                return workshop;
            }
            else {
                return null;
            }
        }

        public void Put(WorkshopDTO workshopToUpdate, string id)
        {
            UpdateLocalDB();

            foreach (Workshop workshop in allWorkshop)
            {
                if (workshop.Id == id)
                {  
                    if(ValidateInput(workshopToUpdate)){
                        workshop.Name = workshopToUpdate.Name;
                        workshop.Status = workshopToUpdate.Status;
                        
                        Workshop input = ConvDTOtoDB(workshopToUpdate);
                        _workshopDB.Update(input);
                        break;
                    }
                } 
            }
        }
        public void Delete(string id)
        {
            UpdateLocalDB();
            foreach (Workshop workshop in allWorkshop)
            {
                if (workshop.Id == id)
                {
                    _workshopDB.Delete(workshop);
                    allWorkshop.Remove(workshop);
                    break;
                }
            }
        }

        public void CancellWorkshop(string id)
        {
            UpdateLocalDB();
            foreach (Workshop workshop in allWorkshop)
            {
                if (workshop.Id == id)
                {
                    if (!workshop.Status.Equals("Cancelled"))
                    {
                        workshop.Status = "Cancelled";
                         _workshopDB.Update(workshop);
                        break;
                    }
                }
            }
        }
        public void PostponeWorkshop(string id)
        {
            UpdateLocalDB();
            foreach (Workshop workshop in allWorkshop)
            {
                if (workshop.Id == id)
                {
                    if (!workshop.Status.Equals("Postponed"))
                    {
                        workshop.Status = "Postponed";
                         _workshopDB.Update(workshop);
                        break;
                    }
                }
            }
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

        public void UpdateLocalDB()
        {
            allWorkshop= _workshopDB.GetAll();
        }

        public Workshop ConvDTOtoDB(WorkshopDTO oldWorkshop) //Converts a DTOWorkshop to a DB Workshop
        {
            Workshop validWorkshop = new Workshop();
            if (!oldWorkshop.Id.Equals("Workshop-0")){
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

        public bool ValidateInput(WorkshopDTO workshop){
            if (workshop.Name == null || workshop.Name == "")
            {
                return false;
            }
            if (workshop.Status == null || workshop.Status == "")
            {
                return false;
            }
            return true;
        }
    }
}