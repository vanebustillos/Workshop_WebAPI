using System;
using System.Collections.Generic;
using System.Linq;
using Database;

namespace BusinessLogic
{
    public class WorkshopLogic : IWorkshopLogic
    {
        private IWorkshopTableDB _workshopDB;
        private List<Workshop> allWorkshop;
        private const string _unvalidInitialWorkshop = "Workshop-0";
        private const string _workshopIdPrefix = "Workshop";
        private const string _firstWorkshop = "Workshop-1";
        private const string _postponed = "Postponed";
        private const string _cancelled = "Cancelled";

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
            WorkshopDTO addedWorkshop = new WorkshopDTO();

            UpdateLocalDB();
            
            if(ValidateInput(workshop)){
                Workshop input = ConvDTOtoDB(workshop);
                input.Id = GenerateId();
                _workshopDB.Create(input);
                addedWorkshop =  workshop;
            }

            return addedWorkshop;
        }

        public WorkshopDTO Put(WorkshopDTO workshopToUpdate, string workshopId)
        {
            WorkshopDTO updatedWorkshop = new WorkshopDTO();
            Workshop updatedWorkshopDB = new Workshop();

            UpdateLocalDB();

            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  workshop.Name = workshopToUpdate.Name;
                  workshop.Status = workshopToUpdate.Status;

                  updatedWorkshopDB = ConvDTOtoDB(workshopToUpdate);
                  _workshopDB.Update(updatedWorkshopDB);
                  updatedWorkshop = ConvDBtoDTO(updatedWorkshopDB);
                  updatedWorkshop.Id = workshop.Id;
                  return workshop;
              })
              .ToList();

            return updatedWorkshop;
        }
        public WorkshopDTO Delete(string workshopId)
        {
            WorkshopDTO deletedWorkshop = new WorkshopDTO();
            Workshop deletedFromDB = new Workshop();

            UpdateLocalDB();
            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  deletedFromDB = _workshopDB.Delete(workshop);
                  allWorkshop.Remove(workshop);
                  deletedWorkshop = ConvDBtoDTO(deletedFromDB);
                  return workshop;
              })
              .ToList();

            return deletedWorkshop;
        }

        public WorkshopDTO Cancel(string workshopId)
        {
            WorkshopDTO cancelledWorkshop = new WorkshopDTO();
            Workshop updatedWorkshop = new Workshop();

            UpdateLocalDB();
            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  workshop.Status = _cancelled;
                  updatedWorkshop = _workshopDB.Update(workshop);
                  cancelledWorkshop = ConvDBtoDTO(updatedWorkshop);
                  return workshop;
              })
              .ToList();
   
            return cancelledWorkshop;
        }

        public WorkshopDTO Postpone(string workshopId)
        {
            WorkshopDTO posponedWorkshop = new WorkshopDTO();
            Workshop updatedWorkshop = new Workshop();

            UpdateLocalDB();

            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  workshop.Status = _postponed;
                  updatedWorkshop = _workshopDB.Update(workshop);
                  posponedWorkshop = ConvDBtoDTO(updatedWorkshop);
                  return workshop;
              })
              .ToList();

            return posponedWorkshop;
        }

        private string GenerateId()
        {
            string id;

            if (allWorkshop.Count == 0)
            {
                id = _firstWorkshop;
            }
            else
            {
                Workshop lastWorkshop = allWorkshop.Last();
                string[] fracment = lastWorkshop.Id.Split("-");
                int lastId = Int32.Parse(fracment[1]) + 1;
                id = $"{ _workshopIdPrefix }{ lastId }";
            }

            return id;
        }

        private void UpdateLocalDB()
        {
            allWorkshop= _workshopDB.GetAll();
        }

        private Workshop ConvDTOtoDB(WorkshopDTO oldWorkshop) //Converts a DTOWorkshop to a DB Workshop
        {
            Workshop validWorkshop = new Workshop();
            if (!oldWorkshop.Id.Equals(_unvalidInitialWorkshop)){
                validWorkshop.Id = oldWorkshop.Id;
            }
            validWorkshop.Name = oldWorkshop.Name;
            validWorkshop.Status = oldWorkshop.Status;

            return validWorkshop;
        }

        private WorkshopDTO ConvDBtoDTO(Workshop oldWorkshop) //Converts a DB Workshop to a DTOWorkshop
        {
            WorkshopDTO validWorkshop = new WorkshopDTO
            {
                Id = oldWorkshop.Id,
                Name = oldWorkshop.Name,
                Status = oldWorkshop.Status
            };

            return validWorkshop;
        }

        private bool ValidateInput(WorkshopDTO workshop)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(workshop.Name) && !string.IsNullOrEmpty(workshop.Status))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}